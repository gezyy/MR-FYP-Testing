using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device1TopSet : MonoBehaviour
{
    public GameObject devicepart;
    // Start is called before the first frame update
    void Start()
    {
        devicepart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞到目标小球，销毁自己
        if (collision.gameObject.CompareTag("DevicePart1"))
        {
            devicepart.SetActive(true);
            Destroy(gameObject);
        }
    }
}
