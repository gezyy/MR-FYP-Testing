using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayer5Controller : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public BallController ballController;  // 引用BallController脚本
    void Start()
    {
        // 获取 VideoPlayer 组件
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 订阅播放结束事件
        videoPlayer.loopPointReached += OnVideoEnd;

        // 开始播放视频
        videoPlayer.Play();
    }

    // 视频播放结束时的回调函数
    void OnVideoEnd(VideoPlayer vp)
    {
        // 停止播放视频
        vp.Stop();
        Debug.Log("视频播放完毕，已停止播放");
        // 启动EnableBallsSequence协程
        StartCoroutine(ballController.EnableBallsSequence());
    }
}
