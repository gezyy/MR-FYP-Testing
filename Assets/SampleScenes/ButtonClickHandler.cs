using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间来操作按钮

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;   // 在Inspector中链接按钮
    public GameObject itemA;  // 在Inspector中链接物品A
    public GameObject itemB;

    void Start()
    {
        // 确保按钮和物品A不为空
        if (myButton != null && itemA != null)
        {
            // 为按钮添加点击事件
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    // 当按钮被点击时启用物品A
    void OnButtonClick()
    {
        itemA.SetActive(true);  // 启用物品A
        itemB.SetActive(false);
    }
}
