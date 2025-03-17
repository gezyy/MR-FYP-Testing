using UnityEngine;

public class BBController: MonoBehaviour
{
    // 记录DataManager物体
    public GameObject dataManager; // 指定DataManager物体
    public GameObject B1; // 存储R1的引用
    public GameObject B2; // 存储R2的引用
    public GameObject B3; // 存储R3的引用

    // 当小球与标签为"Bullet"的物体发生碰撞时启用对应子物体并禁用自己
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 启用DataManager下的相应子物体
            if (gameObject == B1)
            {
                ActivateSubObject("B1");
            }
            else if (gameObject == B2)
            {
                ActivateSubObject("B2");
            }
            else if (gameObject == B3)
            {
                ActivateSubObject("B3");
            }
            // 禁用当前小球物体
            gameObject.SetActive(false); // 禁用当前小球物体
        }
    }

    // 启用DataManager下的指定子物体
    private void ActivateSubObject(string objectName)
    {
        // 确保dataManager存在并且没有被销毁
        if (dataManager != null)
        {
            Transform subObject = dataManager.transform.Find(objectName); // 查找DataManager下的子物体
            if (subObject != null)
            {
                subObject.gameObject.SetActive(true); // 启用指定的子物体
            }
            else
            {
                Debug.LogWarning("DataManager下的子物体 " + objectName + " 没有找到！");
            }
        }
        else
        {
            Debug.LogWarning("DataManager对象为null或已销毁！");
        }
    }
}
