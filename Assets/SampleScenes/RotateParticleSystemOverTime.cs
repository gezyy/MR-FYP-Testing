using UnityEngine;

public class RotateParticleSystemYOverTime : MonoBehaviour
{
    public float startYAngle = 200f;     // 初始Y轴角度
    public float endYAngle = 125f;       // 目标Y轴角度
    public float rotationDuration = 7f;  // 旋转的持续时间

    private float elapsedTime = 0f;      // 已经过去的时间

    void Update()
    {
        // 每帧计算已经过去的时间
        elapsedTime += Time.deltaTime;

        // 计算旋转的比例，确保它不会超过1
        float t = Mathf.Clamp01(elapsedTime / rotationDuration);

        // 使用Lerp平滑插值计算当前Y轴角度
        float currentYAngle = Mathf.Lerp(startYAngle, endYAngle, t);

        // 获取当前的旋转角度
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // 更新Y轴旋转值，保持其他轴不变
        transform.rotation = Quaternion.Euler(currentRotation.x, currentYAngle, currentRotation.z);
    }
}
