using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device1Gaze : MonoBehaviour
{
    // 引用注视检测器
    public GazeFocusDetector gazeFocusDetector;
    public GameObject Panel;

    private bool isTargetInFocus = false;
    private bool allObjectsActivated = false;  // 标记所有物体是否已激活

    // 粒子系统，用于控制粒子播放和旋转
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        // 确保初始状态为禁用

            particleSystem.Stop();
            Panel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 检查目标是否在注视范围内
        if (gazeFocusDetector != null)
        {
            if (gazeFocusDetector.IsTargetInFocus && !isTargetInFocus)
            {
                // 如果目标开始被注视，开始激活物体
                isTargetInFocus = true;

                if (!allObjectsActivated)
                {
                    // 如果物体没有完全激活，开始激活物体
                    Panel.SetActive(true);  // 激活物体
                    allObjectsActivated = true;
                    if (particleSystem != null && !particleSystem.isPlaying)
                    {
                        particleSystem.Play();
                    }
                }
            }
            else if (!gazeFocusDetector.IsTargetInFocus && isTargetInFocus && allObjectsActivated)
            {
                // 如果目标失去注视，但物体没有完全激活，禁用所有物体，并停止粒子系统
                isTargetInFocus = false;
                Panel.SetActive(false);  // 激活物体
                allObjectsActivated = false;
                if (particleSystem != null && particleSystem.isPlaying)
                {
                    particleSystem.Stop();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞到目标小球，销毁自己
        if (collision.gameObject.CompareTag("Device1Pos"))
        {
            Destroy(gameObject);
        }
    }
}
