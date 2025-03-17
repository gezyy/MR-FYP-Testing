using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliderManager : MonoBehaviour
{
    public Slider slider1;  // 第一个Slider
    public Slider slider2;  // 第二个Slider
    public Slider slider3;  // 第三个Slider
    public GameObject objectA;  // 物体A
    public List<GameObject> objectsList;  // 物体列表
    public GameObject hint9;
    public GameObject hint10;
    public GameObject particle;
    public GameObject donut;

    private float lastValue1 = 0f;
    private float lastValue2 = 0f;
    private float lastValue3 = 0f;

    private bool isObjectEnabled = false;  // 标记物体是否已经启用

    void Start()
    {
        // 默认启用objectsList[0]
        if (objectsList.Count > 0)
        {
            objectsList[0].SetActive(true);
        }

        // 初始化三个滑块的值
        lastValue1 = slider1.value;
        lastValue2 = slider2.value;
        lastValue3 = slider3.value;
    }

    void Update()
    {
        // 获取三个slider的当前值
        float value1 = slider1.value;
        float value2 = slider2.value;
        float value3 = slider3.value;

        // 判断三个slider的值是否为 (0.5, 0.5, 1)
        if (objectsList.Count > 3 && objectsList[3].activeSelf)
        {
            // 启用物体A
            objectA.SetActive(true);
            hint9.SetActive(false);
            hint10.SetActive(true);
            particle.SetActive(true);
            donut.SetActive(true);
            DisableOtherObjects();  // 禁用物体列表中的其他物体
            isObjectEnabled = true; // 标记物体已启用
        }
        else
        {
            // 只有当三个slider的值发生变化时才启用随机物体
            if (value1 != lastValue1 || value2 != lastValue2 || value3 != lastValue3)
            {
                // 启用随机选择的物体
                EnableRandomObject();
                isObjectEnabled = true;  // 标记物体已启用
            }
        }

        // 更新滑块的值
        lastValue1 = value1;
        lastValue2 = value2;
        lastValue3 = value3;
    }

    // 随机启用物体列表中的一个物体
    private void EnableRandomObject()
    {
        // 禁用所有物体
        DisableOtherObjects();

        // 如果物体列表不为空，则从列表中随机选择一个物体并启用
        if (objectsList.Count > 0)
        {
            int randomIndex = Random.Range(0, objectsList.Count);
            objectsList[randomIndex].SetActive(true);
        }
    }

    // 禁用物体列表中的所有物体
    private void DisableOtherObjects()
    {
        foreach (var obj in objectsList)
        {
            obj.SetActive(false);
        }
    }
}
