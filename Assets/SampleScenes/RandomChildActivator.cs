using System.Collections;
using UnityEngine;

public class RandomChildActivator : MonoBehaviour
{
    // 用于存储所有需要管理的物体
    public GameObject[] objects;
    public GameObject hint1;
    public AthenaAudioController1 athenaAudioController1;
    // 每隔多少秒启用下一个物体
    public float interval = 5f;

    private int currentIndex = 0;

    void Start()
    {
        // 启动协程来按顺序启用物体
        StartCoroutine(EnableObjectsInOrder());
    }
    void Update()
    {
        // 检查物体列表是否为空或无效
        if (objects == null || objects.Length == 0)
        {

                hint1.SetActive(true);
            athenaAudioController1.PlayVoiceClip(1);

        }
    }
        // 协程实现按顺序启用物体
        private IEnumerator EnableObjectsInOrder()
    {
        while (true)
        {
            // 检查当前索引是否小于物体列表的长度
            if (currentIndex < objects.Length)
            {
                // 启用当前索引的物体
                objects[currentIndex].SetActive(true);
                currentIndex++; // 移动到下一个物体

                // 等待指定的时间间隔再启用下一个物体
                yield return new WaitForSeconds(interval);
            }
            else
            {
                // 如果所有物体都已启用，停止协程
                yield break;
            }
        }
    }
}
