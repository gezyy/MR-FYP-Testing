using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    public GameObject objectA; // 物体A
    public GameObject objectB; // 物体B

    private void OnTriggerEnter(Collider other)
    {
        // 检测与tag为Stone, Orange, Egipt, Ice的碰撞
        if (other.CompareTag("Stone") || other.CompareTag("Orange") ||
            other.CompareTag("Egipt") || other.CompareTag("Ice"))
        {
            StartCoroutine(ActivateAndDeactivate(objectA));
        }

        // 检测与tag为CheckPoint的碰撞
        if (other.CompareTag("CheckPoint"))
        {
            StartCoroutine(ActivateAndDeactivate(objectB));
        }
    }

    private IEnumerator ActivateAndDeactivate(GameObject obj)
    {
        // 启用物体
        obj.SetActive(true);

        // 等待3秒
        yield return new WaitForSeconds(3f);

        // 禁用物体
        obj.SetActive(false);
    }
}
