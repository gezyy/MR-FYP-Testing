using UnityEngine;

public class BulletController: MonoBehaviour
{
    // 子弹存活时间（秒）
    public float lifeTime = 10f;

    private void Start()
    {
        // 在指定时间后自动销毁子弹
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞到目标小球，销毁自己
        if (collision.gameObject.CompareTag("BlueBall"))
        {
            Destroy(gameObject);
        }
    }
}
