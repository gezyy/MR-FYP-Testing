using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class VideoEndTrigger3 : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // 视频播放器
    public GameObject land1;
    public GameObject land2;
    public Transform pos2;
    public Transform pos3;
    public float acceleration = 2f;
    private bool isMoving = false;
    private float speed = 0f;

    public GameObject spaceship;
    public Transform room;
    public CameraController cameraController;
    public GameObject portal;
    public AthenaAudioController1 athenaAudioController1;
    public GameObject effect;

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
        land1.SetActive(false);
        land2.SetActive(false);

        if(spaceship != null)
        {
            StartCoroutine(MoveSpaceship(spaceship.transform));
        }
        StartCoroutine(MoveRoom());
    }
    // 移动飞船的协程
    private IEnumerator MoveSpaceship(Transform spaceshipTransform)
    {
        Rigidbody spaceshipRigidbody = spaceship.GetComponent<Rigidbody>();

        // 确保飞船停下来
        spaceshipRigidbody.velocity = Vector3.zero;
        spaceshipRigidbody.angularVelocity = Vector3.zero;

        // 逐渐加速移动到目标位置
        while (Vector3.Distance(spaceshipTransform.position, pos2.position) > 0.1f)
        {
            // 平滑加速
            speed += acceleration * Time.deltaTime;

            // 移动飞船
            Vector3 direction = (pos2.position - spaceshipTransform.position).normalized;
            spaceshipRigidbody.MovePosition(spaceshipTransform.position + direction * speed * Time.deltaTime);

            // 如果距离目标位置较近，逐渐减速
            if (Vector3.Distance(spaceshipTransform.position, pos2.position) < 5f)
            {
                speed = Mathf.Max(0, speed - acceleration * Time.deltaTime);  // 减速
            }

            yield return null;
        }

        // 到达目标位置后，将速度归零
        spaceshipRigidbody.velocity = Vector3.zero;
        spaceshipTransform.position = pos2.position;
    }

    // 新增的房间移动协程
    private IEnumerator MoveRoom()
    {
        float roomSpeed = 0f;  // 房间的移动速度
        Vector3 roomStartPos = room.position;  // 房间的初始位置

        // 逐渐加速，直到房间到达目标位置
        while (Vector3.Distance(room.position, pos3.position) > 0.1f)
        {
            // 平滑加速
            roomSpeed += acceleration * Time.deltaTime;
            Vector3 direction = (pos3.position - room.position).normalized;
            room.position = room.position + direction * roomSpeed * Time.deltaTime;
            yield return null;
        }

        // 到达目标位置后，将速度归零
        room.position = pos3.position;
        portal.SetActive(true);
        athenaAudioController1.PlayVoiceClip(0);
        effect.SetActive(false);
        cameraController.StopMovement();
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