using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour
{
    // 小球对象列表
    public List<GameObject> balls = new List<GameObject>();

    private int currentBallIndex = 0;  // 当前启用的小球索引
    private bool allBallsUsed = false; // 是否所有小球都已启用过一次
    public GameObject deleteArea;

    public AthenaAudioController athenaAudioController;

    void Start()
    {
        // 确保所有小球对象一开始都处于禁用状态
        foreach (var ball in balls)
        {
            ball.SetActive(false);
        }

        // 开始启用小球
        //StartCoroutine(EnableBallsSequence());
    }

    // 启用小球的顺序
    public IEnumerator EnableBallsSequence()
    {
        while (!allBallsUsed)
        {
            // 当前小球
            GameObject currentBall = balls[currentBallIndex];

            // 启用小球
            currentBall.SetActive(true);

            // 等待5秒或直到小球被销毁
            float elapsedTime = 0f;
            while (elapsedTime < 5f && currentBall != null && currentBall.activeSelf)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 如果小球被销毁或者时间超过5秒，禁用当前小球并启用下一个
            currentBall.SetActive(false);

            // 更新索引
            currentBallIndex++;

            // 检查是否所有小球都已启用过
            if (currentBallIndex >= balls.Count)
            {
                allBallsUsed = true;
                // 启用 deleteArea
                if (deleteArea != null)
                {
                    deleteArea.SetActive(true);
                }

                athenaAudioController.PlayVoiceClip(5);
            }
        }
    }
}

