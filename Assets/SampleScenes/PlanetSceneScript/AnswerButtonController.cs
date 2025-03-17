using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnswerButtonController : MonoBehaviour
{
    // 物品A
    public GameObject itemA;
    public GameObject itemB;

    // 按钮列表
    public List<Button> buttonList;

    // Answer按钮
    public Button answerButton;

    // 按钮原始颜色
    private Color originalColor;

    // 初始化
    void Start()
    {
        // 获取Answer按钮的原始颜色
        if (answerButton != null)
        {
            originalColor = answerButton.GetComponent<Image>().color;
        }

        // 为Answer按钮添加点击事件
        answerButton.onClick.AddListener(ToggleAnswerButtonColor);

        // 为按钮列表中的每个按钮添加点击事件
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    // 点击Answer按钮时切换颜色
    void ToggleAnswerButtonColor()
    {
        Image answerButtonImage = answerButton.GetComponent<Image>();

        // 如果Answer按钮是绿色，恢复原色
        if (answerButtonImage.color == Color.green)
        {
            answerButtonImage.color = originalColor;
        }
        else
        {
            // 否则，设置为绿色
            answerButtonImage.color = Color.green;
        }
    }

    // 处理按钮点击事件
    void OnButtonClicked(Button clickedButton)
    {
        Image answerButtonImage = answerButton.GetComponent<Image>();

        // 如果Answer按钮是绿色
        if (answerButtonImage.color == Color.green)
        {
            // 判断是否点击了特定按钮
            if (clickedButton == buttonList[0]) // 假设buttonList[0]是特定按钮
            {
                EnableItemA(); // 启用物品A
                itemB.SetActive(false);
            }
            else
            {
                // 如果点击了其他按钮，恢复Answer按钮颜色
                answerButtonImage.color = originalColor;
            }
        }
    }

    // 启用物品A
    void EnableItemA()
    {
        if (itemA != null)
        {
            itemA.SetActive(true); // 启用物品A
        }
    }
}
