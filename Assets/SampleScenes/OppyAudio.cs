using System.Collections.Generic;
using UnityEngine;

public class OppyAudio : MonoBehaviour
{
    // 存储所有自定义语音序列的音频剪辑
    public List<AudioClip> audioClips;

    // 音频源，用于播放音频
    private AudioSource audioSource;

    // 初始化，获取AudioSource组件
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("没有找到AudioSource组件，请确保该脚本附加在包含AudioSource的对象上！");
        }
    }

    // 播放指定索引的音频
    public void PlaySound(int index)
    {
        // 确保索引在合法范围内
        if (index >= 0 && index < audioClips.Count)
        {
            // 如果当前没有播放音频，或播放的不是相同的音频剪辑
            if (!audioSource.isPlaying || audioSource.clip != audioClips[index])
            {
                // 设置要播放的音频剪辑
                audioSource.clip = audioClips[index];
                // 播放音频
                audioSource.Play();
            }
            else
            {
                Debug.Log("相同的音频正在播放，跳过重复播放。");
            }
        }
        else
        {
            Debug.LogError("音频索引不合法！请确保索引在音频列表的范围内。");
        }
    }
}
