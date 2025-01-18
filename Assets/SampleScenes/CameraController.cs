using UnityEngine;
using UnityEngine.UI;  // 用于处理按钮事件
using System.Collections;

public class CameraController: MonoBehaviour
{
    public Transform target;  // 方向盘（target）
    public Transform room;  // 房间的Transform
    public Transform spaceship;  // 飞船的Transform

    public float sensitivity = 1.0f;  // 灵敏度，控制旋转速度
    public float maxRotationSpeed = 30f;  // 最大旋转速度
    public float maxAngle = 92.928f;  // 方向盘旋转x的最大值
    public float minAngle = -87.072f;  // 方向盘旋转x的最小值
    public float neutralAngle = 2.928f;  // 方向盘旋转x的中立值
    public float accelerationTime = 1f;  // 从平滑到最大旋转速度的过渡时间
    public float maxMoveSpeed = 5f;  // 飞船和房间的最大前进速度
    public float speedChangeDuration = 2f;  // 移动速度变化的持续时间（平滑过渡的时间）

    private float targetRoomRotationY;  // 房间目标的Y轴旋转
    private float targetSpaceshipRotationY;  // 飞船目标的Y轴旋转
    private float rotationSpeed;  // 当前旋转速度

    private bool isMoving = false;  // 是否正在移动的标志
    private float currentMoveSpeed = 0f;  // 当前的移动速度（用于平滑过渡）


    void Start()
    {
        if (target == null || room == null || spaceship == null)
        {
            Debug.LogWarning("Please assign all required transforms.");
        }

        // 初始化房间和飞船的目标旋转值
        targetRoomRotationY = room.rotation.eulerAngles.y;
        targetSpaceshipRotationY = spaceship.rotation.eulerAngles.y;

    }

    void Update()
    {
        if (target != null && room != null && spaceship != null)
        {
            // 获取方向盘的当前x旋转角度
            float targetXRotation = target.rotation.eulerAngles.x;

            // 计算旋转的增量
            float deltaRotation = targetXRotation - neutralAngle;

            // 如果方向盘x旋转超过临界值，则以最大速度旋转
            if (targetXRotation >= neutralAngle && targetXRotation <= maxAngle)
            {
                // 当目标旋转增加时，房间和飞船向左转
                rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, (targetXRotation - neutralAngle) / (maxAngle - neutralAngle));
            }
            else if (targetXRotation <= neutralAngle && targetXRotation >= minAngle)
            {
                // 当目标旋转减小时，房间和飞船向右转
                rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, (neutralAngle - targetXRotation) / (neutralAngle - minAngle));
            }
            else
            {
                // 如果超过最大值或最小值，固定最大速度
                rotationSpeed = maxRotationSpeed;
            }

            // 平滑房间和飞船的旋转
            targetRoomRotationY = Mathf.LerpAngle(room.rotation.eulerAngles.y, room.rotation.eulerAngles.y + deltaRotation, Time.deltaTime * accelerationTime);
            targetSpaceshipRotationY = Mathf.LerpAngle(spaceship.rotation.eulerAngles.y, spaceship.rotation.eulerAngles.y + deltaRotation, Time.deltaTime * accelerationTime);

            // 最终设置房间和飞船的旋转
            room.rotation = Quaternion.Euler(room.rotation.eulerAngles.x, targetRoomRotationY, room.rotation.eulerAngles.z);
            spaceship.rotation = Quaternion.Euler(spaceship.rotation.eulerAngles.x, targetSpaceshipRotationY, spaceship.rotation.eulerAngles.z);

            // 如果正在移动，飞船和房间沿着z轴（正前方）持续移动
            if (isMoving)
            {
                MoveForward();
            }
        }
    }

    // 飞船和房间的持续前进（带有平滑过渡的速度）
    private void MoveForward()
    {
        // 基于物体的局部z轴方向来移动
        Vector3 moveDirection = new Vector3(0, 0, currentMoveSpeed * Time.deltaTime);

        // 沿着房间和飞船的局部z轴（即前方方向）进行平移
        room.Translate(moveDirection, Space.Self);  // 使用局部空间进行移动
        spaceship.Translate(moveDirection, Space.Self);  // 使用局部空间进行移动
    }

    // 控制飞船和房间停止或恢复移动
    public void ToggleMovement()
    {
        // 根据当前状态判断调用停止或启动方法
        if (isMoving)
        {
            StopMovement();
        }
        else
        {
            StartMovement();
        }
    }

    // 启动过程中的平滑加速
    private void StartMovement()
    {
        StartCoroutine(SmoothStart());
        isMoving = true;  // 启动移动
    }

    // 停止过程中的平滑减速
    private void StopMovement()
    {
        StartCoroutine(SmoothStop());
        isMoving = false;  // 停止移动
    }

    // 启动过程中的平滑加速
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

        currentMoveSpeed = maxMoveSpeed;  // 确保最终达到最大速度
    }

    // 停止过程中的平滑减速
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

        currentMoveSpeed = 0f;  // 确保最终停止
    }
}
