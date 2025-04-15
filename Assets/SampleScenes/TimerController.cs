using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    // 计时器变量
    private int timer = 0;

    // 物体A
    public GameObject hint3;
    public GameObject hint4;
    public GameObject ImageProcess;
    public GameObject FeatureExtract;

    // 按钮列表
    public Button[] buttons;

    void Start()
    {

        // 为每个按钮添加点击事件
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // 按钮点击事件
    void OnButtonClick()
    {
        // 增加计时器
        timer++;

        // 判断计时器是否达到4
        if (timer >= 4)
        {
            ImageProcess.SetActive(false);
            FeatureExtract.SetActive(true);
            hint3.SetActive(false);
            // 启用物体A
            if (hint4 != null)
            {
                hint4.SetActive(true);
            }
        }
    }
}
