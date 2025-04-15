using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlanetController : MonoBehaviour
{
    public GameObject land1;
    public GameObject land2;
    public Transform pos1;
    public float acceleration = 2f;
    private bool isMoving = false;
    private float speed = 0f;
    private bool hasLanded = false;  // 新增标志位
    public Transform room;
    public GameObject effect;

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship") && !hasLanded)
        {
            hasLanded = true;
            land1.SetActive(true);
            land2.SetActive(true);
            effect.SetActive(true);
            // 获取飞船的Rigidbody组件，准备控制物体移动
            Rigidbody spaceshipRigidbody = other.GetComponent<Rigidbody>();
            if (spaceshipRigidbody != null)
            {
                // 将飞船的速度设置为零，让它暂时停下来
                spaceshipRigidbody.velocity = Vector3.zero;
                spaceshipRigidbody.angularVelocity = Vector3.zero; // 防止旋转

                // 开始平滑加速
                StartCoroutine(MoveSpaceship(spaceshipRigidbody, other.transform));
                StartCoroutine(MoveRoom());
            }
        }
    }
    private IEnumerator MoveSpaceship(Rigidbody spaceshipRigidbody, Transform spaceshipTransform)
    {
        // 逐渐加速，直到到达目标位置
        while (Vector3.Distance(spaceshipTransform.position, pos1.position) > 0.1f)
        {
            // 平滑加速
            speed += acceleration * Time.deltaTime;
            Vector3 direction = (pos1.position - spaceshipTransform.position).normalized;
            spaceshipRigidbody.MovePosition(spaceshipTransform.position + direction * speed * Time.deltaTime);
            yield return null;
        }

        // 到达目标位置后，将速度归零
        spaceshipRigidbody.velocity = Vector3.zero;
        spaceshipTransform.position = pos1.position;
    }
    // 新增的房间移动协程
    private IEnumerator MoveRoom()
    {
        float roomSpeed = 0f;  // 房间的移动速度
        Vector3 roomStartPos = room.position;  // 房间的初始位置

        // 逐渐加速，直到房间到达目标位置
        while (Vector3.Distance(room.position, pos1.position) > 0.1f)
        {
            // 平滑加速
            roomSpeed += acceleration * Time.deltaTime;
            Vector3 direction = (pos1.position - room.position).normalized;
            room.position = room.position + direction * roomSpeed * Time.deltaTime;
            yield return null;
        }

        // 到达目标位置后，将速度归零
        room.position = pos1.position;
    }
}

