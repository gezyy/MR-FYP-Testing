using UnityEngine;

public class Axis_rotator : MonoBehaviour
{
    public float Yspeed = 0;
    public float Xspeed = 0;
    public float Zspeed = 0;

    private float xf;
    private float yf;
    private float zf;

    // 引用注视检测脚本
    public GazeFocusDetector gazeFocusDetector;

    void Update()
    {
        // 检查是否正在注视目标
        if (gazeFocusDetector != null && gazeFocusDetector.IsTargetInFocus)
        {
            // 如果正在注视，执行旋转
            yf += Yspeed * Time.deltaTime;
            xf += Xspeed * Time.deltaTime;
            zf += Zspeed * Time.deltaTime;

            transform.localEulerAngles = new Vector3(xf, yf, zf);
        }
        else
        {
            // 如果没有注视，则保持当前角度，不进行旋转
        }
    }
}

