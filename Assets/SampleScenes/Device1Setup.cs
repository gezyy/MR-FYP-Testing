using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device1Setup : MonoBehaviour
{
    public GameObject devicepart2;
    // 引用 AudioManager 脚本的实例
    public AthenaAudioController audioManager;  // 在 Unity 编辑器中将 AudioManager 组件拖到此字段
    public GameObject exampleball;
    public GameObject hint1;
    public GameObject hint2;
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
            // 播放指定的语音片段，假设要播放索引为 0 的语音片段
            if (audioManager != null)
            {
                audioManager.PlayVoiceClip(1);
                exampleball.SetActive(true);
                hint1.SetActive(false);
                hint2.SetActive(true);
            }
        }
    }
}
