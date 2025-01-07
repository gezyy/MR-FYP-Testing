using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device1Setup : MonoBehaviour
{
    public GameObject devicepart2;
    // Start is called before the first frame update
    void Start()
    {
        devicepart2.SetActive(false);
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
            devicepart2.SetActive(true);
        }
    }
}
