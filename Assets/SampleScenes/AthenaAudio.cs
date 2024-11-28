using UnityEngine;
using System.Collections;

public class AthenaAudio : MonoBehaviour
{
    // 用来存储不同语音片段的数组
    public AudioClip[] audioClips;

    // 用于播放语音的 AudioSource
    private AudioSource audioSource;

    // 在 Start() 中初始化
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 启动协程，延迟 3 秒播放 Athena_1 的语音，并循环播放
        StartCoroutine(PlayAudioAfterDelay("Athena_1", 3f));
    }

    // 播放指定名称的语音，并在播放时循环
    private IEnumerator PlayAudioAfterDelay(string clipName, float delay)
    {
        // 延迟指定的时间
        yield return new WaitForSeconds(delay);

        // 播放语音
        PlayAudioByName(clipName);

        // 设置循环播放
        audioSource.loop = true;
    }

    // 播放指定名称的语音片段
    public void PlayAudioByName(string clipName)
    {
        // 查找指定名称的语音片段
        foreach (var clip in audioClips)
        {
            if (clip.name == clipName)
            {
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }

        Debug.LogWarning("Audio clip with the specified name not found");
    }

    // 停止播放语音
    public void StopAudio()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }
}
