using UnityEngine;

public class BlueBallController : MonoBehaviour
{

    void Update()
    {

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


