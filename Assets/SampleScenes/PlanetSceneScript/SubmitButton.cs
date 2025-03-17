using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI库

public class SubmitButton : MonoBehaviour
{
    // 在Inspector面板中拖拽物体A和按钮
    public GameObject hint7;
    public GameObject hint8;// 物体A
    public GameObject Expression;
    public Button button;       // 按钮
    public GameObject defaultChart;
    public GameObject defaultConsole;
    public GameObject codeChart;
    public GameObject codeConsole;
    public GameObject codePartObject;

    void Start()
    {

        // 确保按钮有点击事件监听
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);  // 添加点击监听
        }
    }

    // 按钮点击事件的回调函数
    void OnButtonClicked()
    {
        hint7.SetActive(false);
        Expression.SetActive(false);
        defaultChart.SetActive(false);
        defaultConsole.SetActive(false);
        codeChart.SetActive(true);
        codeConsole.SetActive(true);
        if (hint8 != null)
        {
            hint8.SetActive(true);  // 启用物体A
        }
        codePartObject.SetActive(true);
    }
}
