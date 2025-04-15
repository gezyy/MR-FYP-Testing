using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour
{
    public Button button; // 按钮组件
    public Color greenColor = Color.green; // 按钮变为绿色时的颜色
    private Color originalColor; // 按钮原本的颜色
    private bool isGreen = false; // 控制按钮是否为绿色的状态

    void Start()
    {
        // 获取按钮的原始颜色（假设按钮有一个材质）
        originalColor = button.GetComponent<Image>().color;

        // 给按钮添加点击事件监听器
        button.onClick.AddListener(ToggleColor);
    }

    void ToggleColor()
    {
        // 切换按钮颜色
        if (isGreen)
        {
            button.GetComponent<Image>().color = originalColor; // 恢复原始颜色
        }
        else
        {
            button.GetComponent<Image>().color = greenColor; // 设置为绿色
        }

        // 切换状态
        isGreen = !isGreen;
    }
}
