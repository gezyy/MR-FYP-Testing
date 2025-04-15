using UnityEngine;

public class Stone : MonoBehaviour
{
    // --- Config ---
    public float moveSpeed = 3f;   // Stone 向飞船移动的速度
    private Vector3 spaceshipPosition; // 存储飞船的位置

    // 设置飞船的位置
    public void SetSpaceshipPosition(Vector3 spaceshipPos)
    {
        spaceshipPosition = spaceshipPos;
    }

    private void Update()
    {
        // 如果 spaceshipPosition 没有被设置，说明 Stone 还没有目标位置
        if (spaceshipPosition == Vector3.zero) return;

        // 向飞船的位置移动
        MoveStone(spaceshipPosition); // 修改为传递位置参数
    }

    // 移动 Stone 物体，接受一个飞船位置参数
    public void MoveStone(Vector3 spaceshipPos)
    {
        // 计算朝飞船的方向
        Vector3 direction = (spaceshipPos - transform.position).normalized;
        // 按照一定速度朝飞船移动
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞物体的标签是否为 Rocket 或 Spaceship
        if (collision.gameObject.CompareTag("Rocket") || collision.gameObject.CompareTag("Spaceship"))
        {
            // 销毁自己
            Destroy(gameObject);
        }
    }
}
