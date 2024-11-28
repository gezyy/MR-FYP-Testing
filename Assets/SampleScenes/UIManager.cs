using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text textBox;        // 文本框
    public Image imageBox;      // 图像
    public Button switchButton; // 按钮

    void Start()
    {
        // 默认状态：显示文本框，隐藏图像
        textBox.gameObject.SetActive(true);
        imageBox.gameObject.SetActive(false);

        // 为按钮设置点击事件
        switchButton.onClick.AddListener(OnSwitchButtonClicked);
    }

    void OnSwitchButtonClicked()
    {
        // 切换显示状态
        bool isTextBoxActive = textBox.gameObject.activeSelf;
        textBox.gameObject.SetActive(!isTextBoxActive);
        imageBox.gameObject.SetActive(isTextBoxActive);
    }
}

