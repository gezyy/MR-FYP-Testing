using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // 目标物体的Transform
    public Transform spaceship;  // 飞船的Transform
    public float sensitivity = 1.0f; // 灵敏度，可以调整相机的反应速度
    public float minYRotation = 5f;  // Y轴旋转的最小值
    public float maxYRotation = 75f; // Y轴旋转的最大值
    public float spaceshipRotationSpeed = 3f; // 飞船旋转的平滑速度
    public float alignmentThreshold = 2f; // 飞船和相机的Y轴旋转差值阈值
    public float targetXMin = -89f; // 目标物体X轴旋转的最小值
    public float targetXMax = 89f;  // 目标物体X轴旋转的最大值

    private float lastXRotation;  // 上一次目标物体的X轴旋转值
    private bool isCameraYLimited = true;  // 相机的Y轴旋转是否受限制

    void Start()
    {
        if (target != null)
        {
            // 获取目标物体的初始X轴旋转值
            lastXRotation = target.eulerAngles.x;
        }
        else
        {
            Debug.LogWarning("Target is not assigned!");
        }

        if (spaceship == null)
        {
            Debug.LogWarning("Spaceship is not assigned!");
        }
    }

    void Update()
    {
        if (target != null && spaceship != null)
        {
            // 获取目标物体当前的X轴旋转值
            float currentXRotation = target.eulerAngles.x;

            // 获取当前相机的Y轴旋转值
            float currentYRotation = transform.eulerAngles.y;

            // 获取飞船的当前Y轴旋转值
            float spaceshipYRotation = spaceship.eulerAngles.y;

            // 计算目标物体X轴旋转变化量
            float deltaX = currentXRotation - lastXRotation;

            // 检查飞船和相机的Y轴旋转差值是否在阈值范围内
            float rotationDifference = Mathf.Abs(Mathf.DeltaAngle(spaceshipYRotation, currentYRotation));

            if (rotationDifference <= alignmentThreshold)
            {
                // 如果旋转差值小于阈值，取消相机的Y轴旋转限制
                isCameraYLimited = false;
            }
            else
            {
                // 否则启用相机的Y轴旋转限制
                isCameraYLimited = true;
            }

            // 处理旋转变化
            if (deltaX != 0)
            {
                float newYRotation = currentYRotation - deltaX * sensitivity;

                // 如果相机的Y轴旋转受限制，则应用限制
                if (isCameraYLimited)
                {
                    newYRotation = Mathf.Clamp(newYRotation, minYRotation, maxYRotation);
                }

                // 设置相机的新旋转
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, newYRotation, transform.eulerAngles.z);

                // 使飞船的Y轴旋转平滑过渡到相机的旋转Y值
                spaceship.rotation = Quaternion.Lerp(spaceship.rotation, Quaternion.Euler(spaceship.rotation.eulerAngles.x, newYRotation, spaceship.rotation.eulerAngles.z), Time.deltaTime * spaceshipRotationSpeed);
            }

            // 如果目标物体的X轴旋转达到最小或最大限制，自动调整相机的Y轴旋转
            if (currentXRotation <= targetXMin || currentXRotation >= targetXMax)
            {
                // 如果目标物体的X轴旋转达到了限制，自动调整相机的Y轴旋转
                if (currentXRotation <= targetXMin && currentYRotation > minYRotation)
                {
                    // 目标物体的X旋转达到最小，减小相机的Y旋转
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + sensitivity * Time.deltaTime * 10, transform.eulerAngles.z);
                }
                else if (currentXRotation >= targetXMax && currentYRotation < maxYRotation)
                {
                    // 目标物体的X旋转达到最大，增大相机的Y旋转
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - sensitivity * Time.deltaTime * 10, transform.eulerAngles.z);
                }
            }

            // 更新最后的X轴旋转值
            lastXRotation = currentXRotation;
        }
    }
}

