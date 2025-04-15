using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AthenaAudioController : MonoBehaviour
{
    // 定义一个语音片段列表
    public List<AudioClip> voiceClips;

    // 用于存储已经播放的语音
    private HashSet<int> playedClips;

    // AudioSource 用来播放语音
    private AudioSource audioSource;

    // 用于指定场景加载时播放的语音索引
    public int specifiedClipIndex;

    public AircraftMovement aircraftMovement;

    void Awake()
    {
        // 初始化
        audioSource = GetComponent<AudioSource>();
        playedClips = new HashSet<int>();

        // 如果没有找到 AudioSource，则添加一个
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 在场景加载时播放指定的语音
        PlaySpecifiedVoiceClip();
        aircraftMovement.MoveToPositionByIndex(1);  // 将飞行器移动到positions列表中的第一个位置

    }

    // 播放指定的语音片段
    void PlaySpecifiedVoiceClip()
    {
        if (specifiedClipIndex >= 0 && specifiedClipIndex < voiceClips.Count)
        {
            // 确保语音没有被重复播放
            if (!playedClips.Contains(specifiedClipIndex))
            {
                playedClips.Add(specifiedClipIndex);
                audioSource.clip = voiceClips[specifiedClipIndex];
                audioSource.Play();
            }
            else
            {
                Debug.Log("该语音已经播放过了");
            }
        }
        else
        {
            Debug.Log("无效的语音索引");
        }
    }

    // 允许其他脚本调用的接口，用于播放指定的语音片段
    public void PlayVoiceClip(int clipIndex)
    {
        // 确保 clipIndex 在有效范围内，并且该语音没有被播放过
        if (clipIndex >= 0 && clipIndex < voiceClips.Count && !playedClips.Contains(clipIndex))
        {
            playedClips.Add(clipIndex);
            audioSource.clip = voiceClips[clipIndex];
            audioSource.Play();
        }
        else
        {
            Debug.Log("该语音已经播放过或索引无效");
        }
    }
    // 播放一条语音，然后紧接着播放下一条
    public IEnumerator PlayVoiceSequence(int[] clipIndices)
    {
        foreach (int index in clipIndices)
        {
            PlayVoiceClip(index);  // 播放当前语音
            yield return new WaitUntil(() => !audioSource.isPlaying);  // 等待直到语音播放完毕
        }
    }
}
