using UnityEngine;
using UnityEngine.Video;

public class AthenaListen : MonoBehaviour
{
    public ThoughtBubble _thoughtBubble;
    public GameObject _listeningIndicator;
    public bool _welcomeVoicePlayed = false; // 用于判断是否播放了欢迎语音

    private void Awake()
    {
        _listeningIndicator.SetActive(false);
    }

    public void Initialize()
    {
        _thoughtBubble.gameObject.SetActive(false);
    }

    public bool CanListen()
    {
        // 判断是否播放了欢迎语音
        //bool welcomeVoicePlayed = _welcomeVoicePlayed;

        // 条件：处于 Intro 章节且欢迎语音播放完毕
        //return welcomeVoicePlayed;
        return true;
        Debug.Log("CanListen!");
    }

    // 修改后的 OnWelcomeVoicePlayed 方法
    public void OnWelcomeVoicePlayed()
    {
        // 遍历所有子物体
        foreach (Transform child in transform)
        {
            // 检查子物体是否启用
            if (child.gameObject.activeSelf)
            {
                // 查找启用了 VideoPlayer 组件的子物体
                VideoPlayer videoPlayer = child.GetComponent<VideoPlayer>();
                if (videoPlayer != null)
                {
                    // 检查视频是否已经播放完毕
                    if (videoPlayer.frame >= (long)videoPlayer.frameCount)
                    {
                        // 如果视频已经播放完一遍，返回 true //应该判断当前正在播放的视频是哪个，不需要判断视频有没有播放完。
                        _welcomeVoicePlayed = true;
                        return;
                    }
                }
            }
        }

        // 如果没有找到播放完的视频，继续保持 false
        _welcomeVoicePlayed = false;
    }

    // 新增方法：重新播放视频
    public void ReplayVideo()
    {
        // 遍历所有子物体
        foreach (Transform child in transform)
        {
            // 检查子物体是否启用
            if (child.gameObject.activeSelf)
            {
                // 查找启用了 VideoPlayer 组件的子物体
                VideoPlayer videoPlayer = child.GetComponent<VideoPlayer>();
                if (videoPlayer != null)
                {
                    // 如果找到了 VideoPlayer 组件，重新播放视频
                    videoPlayer.Stop();  // 停止当前播放
                    videoPlayer.Play();  // 重新播放视频
                    Debug.Log("Replaying video on: " + child.name);
                    return;  // 找到第一个视频并重新播放，之后退出
                }
            }
        }

        Debug.Log("No enabled VideoPlayer found to replay.");
    }
    public void DisplayThought(string thought = "")
    {
        _thoughtBubble.gameObject.SetActive(true);
        _thoughtBubble.ForceSizeUpdate();
        if (thought == "")
        {
            _thoughtBubble.ShowHint();
        }
        else
        {
            _thoughtBubble.UpdateText(thought);
        }
    }

    public void HideThought()
    {
        _thoughtBubble.gameObject.SetActive(false);
    }

    public void Listening(bool value)
    {
        _listeningIndicator.SetActive(value);
    }

    public void ListenFail()
    {
        HideThought();
        _listeningIndicator.SetActive(false);
    }

    public void VoiceCommandHandler(string actionString)
    {
        Listening(false);
        DisplayThought(actionString + "?");
    }
}
