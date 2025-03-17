using UnityEngine;

public class RBController: MonoBehaviour
{
    // 记录DataManager物体
    public GameObject dataManager; // 指定DataManager物体
    public GameObject R1; // 存储R1的引用
    public GameObject R2; // 存储R2的引用
    public GameObject R3; // 存储R3的引用
    public GameObject R4; // 存储R3的引用

    // 当小球与标签为"Bullet"的物体发生碰撞时启用对应子物体并禁用自己
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 启用DataManager下的相应子物体
            if (gameObject == R1)
            {
                ActivateSubObject("R1");
            }
            else if (gameObject == R2)
            {
                ActivateSubObject("R2");
            }
            else if (gameObject == R3)
            {
                ActivateSubObject("R3");
            }
            else if (gameObject == R4)
            {
                ActivateSubObject("R4");
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
