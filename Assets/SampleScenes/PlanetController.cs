using UnityEngine;

public class PlanetController: MonoBehaviour
{
    // 每秒减少的量
    public float decreaseSpeed = 2f;

    // 控制行星是否运动
    private bool isMoving = true;

    void Update()
    {
        // 如果isMoving为true，行星按照脚本移动
        if (isMoving)
        {
            transform.position -= new Vector3(decreaseSpeed * Time.deltaTime, 0f, decreaseSpeed * Time.deltaTime);
        }
    }

    // 方法用于停止行星运动
    public void StopMovement()
    {
        isMoving = false;
    }

    // 方法用于启动行星运动
    public void StartMovement()
    {
        isMoving = true;
    }
}

