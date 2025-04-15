using UnityEngine;

public class RedBallController: MonoBehaviour
{
    [SerializeField] private Transform leftControllerPivot; // 左手控制器的位置
    [SerializeField] private Transform targetPosition;      // 小球靠近的目标位置
    [SerializeField] private float detectionRange = 2f;     // 检测范围
    [SerializeField] private float moveSpeed = 5f;          // 小球移动速度
    private RightControllerGun gunController; // 引用子弹管理脚本

    private bool isMoving = false; // 小球是否正在移动

    private void Start()
    {
        // 获取场景中 RightControllerGun 脚本
        gunController = FindObjectOfType<RightControllerGun>();

        if (gunController == null)
        {
            Debug.LogError("未找到 RightControllerGun 脚本，请确保场景中有该对象！");
        }
    }

    private void Update()
    {
        // 检测小球与左手控制器的距离
        float distanceToController = Vector3.Distance(transform.position, leftControllerPivot.position);

        // 检查是否在范围内且按下握紧按钮
        if (distanceToController <= detectionRange && OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            // 开始移动小球
            isMoving = true;
        }
        else
        {
            // 停止移动小球
            isMoving = false;
        }

        // 如果小球正在移动
        if (isMoving)
        {
            // 检查目标位置是否被赋值
            if (targetPosition != null)
            {
                // 计算方向向量并移动小球
                Vector3 direction = (targetPosition.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                // 如果小球到达目标位置，摧毁小球
                if (Vector3.Distance(transform.position, targetPosition.position) <= 0.1f)
                {
                    DestroyBallAndReload(); // 摧毁小球并装填子弹
                }
            }
        }
    }
    private void DestroyBallAndReload()
    {
        Destroy(gameObject); // 摧毁小球
        if (gunController != null)
        {
            gunController.Reload(1); // 调用 Reload 方法，增加 1 发子弹
            Debug.Log("小球销毁，子弹+1");
        }
    }
}

