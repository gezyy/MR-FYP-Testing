using UnityEngine;
using System.Collections.Generic;


public class ExampleTarget : MonoBehaviour
{
    // 记录DataManager物体
    public AthenaAudioController athenaAudioController;
    public AircraftMovement aircraftMovement;
    public GameObject hint2;
    public GameObject hint3;

    // 当小球与标签为"Bullet"的物体发生碰撞时启用对应子物体并禁用自己
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(athenaAudioController.PlayVoiceSequence(new int[] { 2, 3 }));
            // 等待播放完成后调用飞行器移动到指定位置
            List<int> targetPositions = new List<int> { 0, 2 }; // 定义位置索引0和2
            aircraftMovement.MoveThroughMultiplePositions(targetPositions);  // 调用飞行器移动

            hint2.SetActive(false);
            hint3.SetActive(true);
            // 禁用当前小球物体
            gameObject.SetActive(false);

        }
    }
}