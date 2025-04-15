using UnityEngine;
using UnityEngine.Video;

public class VideoEndTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // 视频播放器
    public GameObject ImagePreprocessing;         // 需要启用的物品A
    public GameObject Intro;
    public GameObject hint2;
    public GameObject hint3;

    // Start is called before the first frame update
    void Start()
    {
        // 确保视频播放器存在，并订阅到 loopPointReached 事件
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // 监听视频播放完毕事件
        }
    }

    // 视频播放完一遍后调用这个方法
    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video has finished playing.");
        Intro.SetActive(false);
        ImagePreprocessing.SetActive(true); // 启用物品A
        hint2.SetActive(false);
        hint3.SetActive(true);
    }

    // 确保销毁时取消订阅事件
    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}

