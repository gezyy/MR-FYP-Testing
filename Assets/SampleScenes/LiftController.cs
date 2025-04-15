using UnityEngine;

public class LiftController: MonoBehaviour
{
    public Transform target;  // 控制杆的Transform
    public Transform room;  // 房间的Transform
    public Transform spaceship;  // 飞船的Transform

    // 控制杆、飞船、房间升降时的临界值
    public float neutralLiftAngle = 52.367f;  // 控制杆的中立角度
    public float maxLiftAngle = -21.241f;  // 飞船最大升起角度
    public float maxLiftAngleR = -25f;  // 房间最大升起角度

    public float maxDescendAngle = 28.644f;  // 飞船最大下降角度
    public float maxDescendAngleR = 25f;  // 房间最大下降角度
    public float maxControlLeverAngle = -31.759f;  // 控制杆最大下降角度

    // 房间位置的变化范围
    public float roomMaxHeight = 5f;  // 房间最大高度
    public float roomMinHeight = -3f;  // 房间最小高度
    public float roomNeutralHeight = 1f;  // 房间初始高度

    // 平滑过渡速度
    public float transitionSpeed = 0.5f;

    private float targetRoomRotationX;  // 房间目标的X轴旋转
    private float targetSpaceshipRotationX;  // 飞船目标的X轴旋转
    private float targetRoomPositionY;  // 房间目标的Y轴位置

    void Start()
    {
        if (target == null || room == null || spaceship == null)
        {
            Debug.LogWarning("Please assign all required transforms.");
        }

        // 初始化飞船和房间的目标旋转和位置
        targetRoomRotationX = room.rotation.eulerAngles.x;
        targetSpaceshipRotationX = spaceship.rotation.eulerAngles.x;
        targetRoomPositionY = room.position.y;
    }

    void Update()
    {
        if (target != null && room != null && spaceship != null)
        {
            // 获取控制杆当前的y旋转角度
            float targetXRotation = target.rotation.eulerAngles.x;

            // 打印targetYRotation值用于调试
            //Debug.Log("Control Lever Rotation (X): " + targetXRotation);

            // 控制杆的旋转角度与中立角度的偏差
            float deltaRotation = targetXRotation - neutralLiftAngle;

            // 打印deltaRotation值用于调试
            //Debug.Log("Delta Rotation: " + deltaRotation);

            // 升起状态（控制杆旋转小于中立值）
            if (targetXRotation > neutralLiftAngle)
            {
                // 计算飞船和房间的旋转
                targetSpaceshipRotationX = Mathf.Lerp(maxLiftAngle, 3.703f, Mathf.InverseLerp(113.216f, neutralLiftAngle, targetXRotation));
                targetRoomRotationX = Mathf.Lerp(maxLiftAngleR, 0f, Mathf.InverseLerp(113.216f, neutralLiftAngle, targetXRotation));

                // 计算房间的高度
                //targetRoomPositionY = Mathf.Lerp(roomMinHeight, roomMaxHeight, Mathf.InverseLerp(113.216f, neutralLiftAngle, targetXRotation));
            }
            // 降落状态（控制杆旋转大于中立值）
            else if (targetXRotation < neutralLiftAngle)
            {
                // 计算飞船和房间的旋转
                targetSpaceshipRotationX = Mathf.Lerp(3.703f, maxDescendAngle, Mathf.InverseLerp(neutralLiftAngle, maxControlLeverAngle, targetXRotation));
                targetRoomRotationX = Mathf.Lerp(0f, maxDescendAngleR, Mathf.InverseLerp(neutralLiftAngle, maxControlLeverAngle, targetXRotation));

                // 计算房间的高度
                //targetRoomPositionY = Mathf.Lerp(roomNeutralHeight, roomMinHeight, Mathf.InverseLerp(neutralLiftAngle, maxControlLeverAngle, targetXRotation));
            }

            // 控制杆超过临界值时，保持在最大或最小角度
            if (targetXRotation >= 113.216f)
            {
                targetSpaceshipRotationX = maxLiftAngle;
                targetRoomRotationX = maxLiftAngleR;
                //targetRoomPositionY = roomMaxHeight;
            }
            else if (targetXRotation <= maxControlLeverAngle)
            {
                targetSpaceshipRotationX = maxDescendAngle;
                targetRoomRotationX = maxDescendAngleR;
                //targetRoomPositionY = roomMinHeight;
            }

            // 打印目标飞船和房间的旋转和位置
            //Debug.Log("Spaceship Target Rotation (X): " + targetSpaceshipRotationX);
            //Debug.Log("Room Target Rotation (X): " + targetRoomRotationX);
            //Debug.Log("Room Target Position (Y): " + targetRoomPositionY);

            // 使用 Slerp 平滑旋转
            spaceship.rotation = Quaternion.Slerp(spaceship.rotation, Quaternion.Euler(targetSpaceshipRotationX, spaceship.rotation.eulerAngles.y, spaceship.rotation.eulerAngles.z), Time.deltaTime * transitionSpeed);
            room.rotation = Quaternion.Slerp(room.rotation, Quaternion.Euler(targetRoomRotationX, room.rotation.eulerAngles.y, room.rotation.eulerAngles.z), Time.deltaTime * transitionSpeed);

            // 平滑房间的Y轴位置
            //room.position = new Vector3(room.position.x, Mathf.Lerp(room.position.y, targetRoomPositionY, Time.deltaTime * transitionSpeed), room.position.z);
        }
    }

}
