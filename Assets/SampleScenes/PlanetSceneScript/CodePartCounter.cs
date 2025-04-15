using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePartCounter : MonoBehaviour
{
    public int counter = 0;  // 计数器，初始值为0
    public GameObject hint9;  // 需要启用的物体A
    public GameObject hint8;
    public GameObject predictChart;
    public GameObject defaultChart;
    public GameObject FlowChart;
    public GameObject error;
    public GameObject codePart;
    public GameObject predictCHART;
    public GameObject defaultCHART;
    public AircraftMovement aircraftMovement;
    void Update()
    {
        // 如果计数器大于等于5，则启用物体A
        if (counter >= 5)
        {
            hint9.SetActive(true);
            hint8.SetActive(false);
            predictChart.SetActive(true);
            predictCHART.SetActive(true);
            defaultChart.SetActive(false);
            FlowChart.SetActive(false);
            error.SetActive(false);
            codePart.SetActive(false);
            defaultCHART.SetActive(false);
            aircraftMovement.MoveToPositionByIndex(4);
        }
    }

    // 其他脚本可以通过调用此方法来增加计数器的值
    public void IncrementCounter()
    {
        counter++;
    }
}
