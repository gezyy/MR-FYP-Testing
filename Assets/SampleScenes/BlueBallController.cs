using UnityEngine;

public class BlueBallController : MonoBehaviour
{
    // 目标物体的Transform
    public Transform targetTransform;
    // 移动速度
    public float moveSpeed = 5f;

    void Update()
    {
        // 如果目标物体存在，持续向目标位置移动
        if (targetTransform != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // 计算当前位置到目标物体位置的方向
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        // 更新位置
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 如果非常接近目标位置，销毁小球
        if (Vector3.Distance(transform.position, targetTransform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果与子弹发生碰撞，摧毁小球
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}


