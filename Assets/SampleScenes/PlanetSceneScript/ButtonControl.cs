using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonControl : MonoBehaviour
{
    public List<Button> buttons; // 存储按钮的列表
    public GameObject data1; // temperature
    public GameObject data2; // humidity
    public GameObject data3; // atmosphere
    public GameObject error; // 物品B
    public GameObject noise; // 物品C
    public GameObject tempText;
    public GameObject tempButtons;
    public GameObject tempChart;
    public GameObject HumText;
    public GameObject HumButtons;
    public GameObject HumChart;
    public GameObject AtoText;
    public GameObject AtoButtons;
    public GameObject AtoChart;
    public GameObject defaultChart;
    // 在 Inspector 中定义一个公开按钮来触发检查操作
    public Button checkButton;
    public GameObject GenerateButton;

    private int clickCount = 0;  // 用于跟踪checkButton被点击的次数

    void Start()
    {
        // 给检查按钮添加点击事件
        checkButton.onClick.AddListener(OnCheckButtonClicked);
    }

    void OnCheckButtonClicked()
    {
        clickCount++;  // 每次点击，递增计数器
        Debug.Log("Button clicked " + clickCount + " times");

        // 根据点击次数执行不同的操作
        if (clickCount == 1)
        {
            // 第一次点击时
            EnableItem(data1);
            tempButtons.SetActive(false);
            Debug.Log("Disabling tempChart");
            tempChart.SetActive(false);
            Debug.Log("tempChart active: " + tempChart.activeSelf);
            tempText.SetActive(false);
            HumButtons.SetActive(true);
            HumChart.SetActive(true);
            HumText.SetActive(true);
        }
        else if (clickCount == 2)
        {
            // 第二次点击时
            EnableItem(data2);
            HumButtons.SetActive(false);
            HumChart.SetActive(false);
            HumText.SetActive(false);
            AtoButtons.SetActive(true);
            AtoChart.SetActive(true);
            AtoText.SetActive(true);
        }
        else if (clickCount == 3)
        {
            // 第三次点击时
            EnableItem(data3);
            AtoButtons.SetActive(false);
            AtoChart.SetActive(false);
            AtoText.SetActive(false);
            defaultChart.SetActive(true);
            GenerateButton.SetActive(false);
        }
    }

    // 启用指定物品并禁用其他物品
    void EnableItem(GameObject itemToEnable)
    {
        // 禁用所有物品
        data1.SetActive(false);
        data2.SetActive(false);
        data3.SetActive(false);
        noise.SetActive(false);
        error.SetActive(false);

        // 启用指定的物品
        itemToEnable.SetActive(true);
    }
}
