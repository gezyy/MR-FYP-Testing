using UnityEngine;

public class GazeFocusDetector : MonoBehaviour
{
    // 可设置的被注视的目标对象
    public Transform targetObject;

    // 用于调整视野角度，控制注视的精度（角度范围）
    [SerializeField]
    private float focusFov = 20f;

    // 用于判断注视距离的阈值
    [SerializeField]
    private float distanceThreshold = 0.6f;

    // 判断是否注视的缓冲计数
    private int focusCount = 0;
    private int focusBuffer = 30;

    // 当前是否正在注视目标
    private bool isCurrentlyFocused = false;

    // 提供给外部访问的接口，返回当前是否正在注视
    public bool IsTargetInFocus => isCurrentlyFocused;

    // 内部方法：检测目标是否在视野范围内
    void FixedUpdate()
    {
        if (targetObject != null)
        {
            bool focus = HasFocus();
            if (isCurrentlyFocused != focus)
            {
                isCurrentlyFocused = focus;
                // 这里你可以调用一个事件或通知来告知其他脚本注视状态发生了变化
            }
        }
    }

    // 判断目标是否在注视范围内
    private bool HasFocus()
    {
        // 获取目标位置与摄像头的方向向量
        Vector3 targetDir = targetObject.position - Camera.main.transform.position;
        targetDir.y = 0;  // 忽略垂直方向的影响
        float distance = targetDir.sqrMagnitude;

        // 判断视角与距离是否符合要求
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;  // 忽略垂直方向的影响
        float angle = Vector3.Angle(targetDir, forward);

        if (angle < focusFov || distance < distanceThreshold)
        {
            focusCount++;
        }
        else
        {
            focusCount = 0;
            return false;
        }

        if (focusCount >= focusBuffer)
        {
            focusCount = focusBuffer;
            return true;
        }
        return isCurrentlyFocused;
    }
}

