using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelActivator : MonoBehaviour
{
    // 需要激活的物体列表
    public List<GameObject> objectsToActivate = new List<GameObject>();

    // 激活每个物体的延迟时间（秒）
    public float activationDelay = 1f;

    // 引用注视检测器
    public GazeFocusDetector gazeFocusDetector;

    // 音频源，用于控制语音播放
    public AudioSource audioSource;

    // 语音片段，Athena_1 和 Athena_2
    public AudioClip athena1Clip;
    public AudioClip athena2Clip;

    // 粒子系统，用于控制粒子播放和旋转
    public ParticleSystem particleSystem;

    // 粒子系统的旋转起始角度和目标角度
    public float startYAngle = 200f;     // 初始Y轴角度
    public float endYAngle = 125f;       // 目标Y轴角度
    public float rotationDuration = 5f;  // 旋转持续时间

    private Coroutine activationCoroutine;
    private Coroutine particleRotationCoroutine;
    private bool isTargetInFocus = false;
    private bool allObjectsActivated = false;  // 标记所有物体是否已激活

    void Start()
    {
        // 确保在 Start 时就播放 Athena_1，并让它循环播放
        if (audioSource != null && athena1Clip != null)
        {
            audioSource.clip = athena1Clip;
            audioSource.loop = true;
            audioSource.Play();
        }

        // 确保粒子系统初始状态为禁用
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

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
                    StartActivationSequence();
                    StartParticleSystemRotation();
                }
            }
            else if (!gazeFocusDetector.IsTargetInFocus && isTargetInFocus && !allObjectsActivated)
            {
                // 如果目标失去注视，但物体没有完全激活，禁用所有物体，并停止粒子系统
                isTargetInFocus = false;
                DeactivateAllObjects();
                StopParticleSystemRotation();
            }
        }
    }

    // 启动物体逐一激活的协程
    private void StartActivationSequence()
    {
        // 如果当前有激活的协程在运行，先停止它
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
        }

        // 启动新的激活协程
        activationCoroutine = StartCoroutine(ActivateObjectsWithDelay());
    }

    // 按照顺序激活物体
    private IEnumerator ActivateObjectsWithDelay()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);  // 激活物体
            yield return new WaitForSeconds(activationDelay);  // 等待指定的延迟时间
        }

        // 所有物体激活后，设置标志
        allObjectsActivated = true;

        // 停止播放 Athena_1 并播放 Athena_2
        if (audioSource != null)
        {
            // 停止播放 Athena_1
            audioSource.Stop();

            // 播放 Athena_2，只播放一次
            audioSource.clip = athena2Clip;
            audioSource.loop = false;  // 禁止循环播放
            audioSource.Play();
        }

        // 禁用粒子系统
        StopParticleSystemRotation();
    }

    // 禁用所有物体
    private void DeactivateAllObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);  // 禁用物体
        }

        // 如果有激活的协程，停止它
        if (activationCoroutine != null)
        {
            StopCoroutine(activationCoroutine);
            activationCoroutine = null;
        }

        // 设置标志为未激活状态
        allObjectsActivated = false;
    }

    // 启动粒子系统的旋转
    private void StartParticleSystemRotation()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        // 启动粒子系统旋转的协程
        if (particleRotationCoroutine != null)
        {
            StopCoroutine(particleRotationCoroutine);
        }
        particleRotationCoroutine = StartCoroutine(RotateParticleSystemOverTime());
    }

    // 停止粒子系统的旋转
    private void StopParticleSystemRotation()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }

        // 如果旋转协程在运行，停止它
        if (particleRotationCoroutine != null)
        {
            StopCoroutine(particleRotationCoroutine);
            particleRotationCoroutine = null;
        }
    }

    // 粒子系统旋转的协程
    private IEnumerator RotateParticleSystemOverTime()
    {
        // 确保粒子系统的初始旋转角度为 startYAngle
        particleSystem.transform.rotation = Quaternion.Euler(0f, startYAngle, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            // 计算当前旋转的比例
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);

            // 使用Lerp平滑插值计算当前Y轴角度
            float currentYAngle = Mathf.Lerp(startYAngle, endYAngle, t);

            // 更新粒子系统的旋转
            particleSystem.transform.rotation = Quaternion.Euler(0f, currentYAngle, 0f);

            // 等待下一帧
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最终确保粒子系统的角度正确
        particleSystem.transform.rotation = Quaternion.Euler(0f, endYAngle, 0f);
    }

}

