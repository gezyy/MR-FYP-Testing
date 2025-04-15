using UnityEngine;

public class RotateYAxis: MonoBehaviour
{
    // 旋转速度，单位为角度/秒
    public float rotationSpeed = 50f;

    // Update方法在每帧调用
    void Update()
    {
        // 计算旋转的角度
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // 应用旋转，只改变Y轴的值
        transform.Rotate(0, rotationAngle, 0, Space.Self);
    }
}

