using UnityEngine;
using UnityEngine.UI;  // Used for handling button events
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Steering wheel (target)
    public Transform room;  // Room's Transform
    public Transform spaceship;  // Spaceship's Transform

    public float sensitivity = 1.0f;  // Sensitivity, controls rotation speed
    public float maxRotationSpeed = 30f;  // Maximum rotation speed
    public float maxAngle = 92.928f;  // Maximum x rotation angle of the steering wheel
    public float minAngle = -87.072f;  // Minimum x rotation angle of the steering wheel
    public float neutralAngle = 2.928f;  // Neutral x rotation angle of the steering wheel
    public float accelerationTime = 1f;  // Transition time from smooth to max rotation speed
    public float maxMoveSpeed = 5f;  // Maximum forward speed of the spaceship and room
    public float speedChangeDuration = 2f;  // Duration for smooth speed transition (time to change speed)

    private float targetRoomRotationY;  // Target Y-axis rotation of the room
    private float targetSpaceshipRotationY;  // Target Y-axis rotation of the spaceship
    private float rotationSpeed;  // Current rotation speed

    private bool isMoving = false;  // Flag to indicate if it is moving
    private float currentMoveSpeed = 0f;  // Current move speed (used for smooth transition)

    void Start()
    {
        if (target == null || room == null || spaceship == null)
        {
            Debug.LogWarning("Please assign all required transforms.");
        }

        // Initialize the target rotation values for the room and spaceship
        targetRoomRotationY = room.rotation.eulerAngles.y;
        targetSpaceshipRotationY = spaceship.rotation.eulerAngles.y;

    }

    void Update()
    {
        if (target != null && room != null && spaceship != null)
        {
            // Get the current x rotation angle of the steering wheel
            float targetXRotation = target.rotation.eulerAngles.x;

            // Calculate the rotation delta
            float deltaRotation = targetXRotation - neutralAngle;

            // If the steering wheel's x rotation exceeds the critical value, rotate at max speed
            if (targetXRotation >= neutralAngle && targetXRotation <= maxAngle)
            {
                // When the target rotation increases, the room and spaceship turn to the left
                rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, (targetXRotation - neutralAngle) / (maxAngle - neutralAngle));
            }
            else if (targetXRotation <= neutralAngle && targetXRotation >= minAngle)
            {
                // When the target rotation decreases, the room and spaceship turn to the right
                rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, (neutralAngle - targetXRotation) / (neutralAngle - minAngle));
            }
            else
            {
                // If it exceeds the maximum or minimum value, use the maximum speed
                rotationSpeed = maxRotationSpeed;
            }

            // Smoothly rotate the room and spaceship
            targetRoomRotationY = Mathf.LerpAngle(room.rotation.eulerAngles.y, room.rotation.eulerAngles.y + deltaRotation, Time.deltaTime * accelerationTime);
            targetSpaceshipRotationY = Mathf.LerpAngle(spaceship.rotation.eulerAngles.y, spaceship.rotation.eulerAngles.y + deltaRotation, Time.deltaTime * accelerationTime);

            // Apply the final rotations to the room and spaceship
            room.rotation = Quaternion.Euler(room.rotation.eulerAngles.x, targetRoomRotationY, room.rotation.eulerAngles.z);
            spaceship.rotation = Quaternion.Euler(spaceship.rotation.eulerAngles.x, targetSpaceshipRotationY, spaceship.rotation.eulerAngles.z);

            // If moving, the spaceship and room continue to move forward along the z-axis
            if (isMoving)
            {
                MoveForward();
            }
        }
    }

    // Continuous forward movement of the spaceship and room (with smooth speed transition)
    private void MoveForward()
    {
        // Move based on the object's local z-axis direction
        Vector3 moveDirection = new Vector3(0, 0, currentMoveSpeed * Time.deltaTime);

        // Translate the room and spaceship along their local z-axis (i.e., forward direction)
        room.Translate(moveDirection, Space.Self);  // Move in local space
        spaceship.Translate(moveDirection, Space.Self);  // Move in local space
    }

    // Control the spaceship and room to stop or resume movement
    public void ToggleMovement()
    {
        // Check the current state and either stop or start the movement
        if (isMoving)
        {
            StopMovement();
        }
        else
        {
            StartMovement();
        }
    }

    // Smooth acceleration during the start of movement
    private void StartMovement()
    {
        StartCoroutine(SmoothStart());
        isMoving = true;  // Start moving
    }

    // Smooth deceleration during the stop of movement
    public void StopMovement()
    {
        StartCoroutine(SmoothStop());
        isMoving = false;  // Stop moving
    }

    // Smooth acceleration during the start of movement
    private IEnumerator SmoothStart()
    {
        float startTime = Time.time;
        float initialSpeed = currentMoveSpeed;

        while (currentMoveSpeed < maxMoveSpeed)
        {
            float t = (Time.time - startTime) / speedChangeDuration;
            currentMoveSpeed = Mathf.Lerp(initialSpeed, maxMoveSpeed, t);
            yield return null;
        }

        currentMoveSpeed = maxMoveSpeed;  // Ensure the final speed reaches max speed
    }

    // Smooth deceleration during the stop of movement
    private IEnumerator SmoothStop()
    {
        float startTime = Time.time;
        float initialSpeed = currentMoveSpeed;

        while (currentMoveSpeed > 0f)
        {
            float t = (Time.time - startTime) / speedChangeDuration;
            currentMoveSpeed = Mathf.Lerp(initialSpeed, 0f, t);
            yield return null;
        }

        currentMoveSpeed = 0f;  // Ensure the final speed is zero
    }
}
