using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间来操作按钮
using Oculus.Interaction;

public class ButtonClickHandler2 : MonoBehaviour
{
    public GameObject Task;  // 在Inspector中链接物品A
    public GameObject hint1;
    public GameObject Athena1;

    void Start()
    {
    }

    // 当按钮被点击时启用物品A
    public void OnButtonClick()
    {
        Task.SetActive(true);  // 启用物品A
        hint1.SetActive(true);
        Athena1.SetActive(false);
    }
}
