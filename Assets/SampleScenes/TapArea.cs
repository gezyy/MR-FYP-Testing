using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapArea : MonoBehaviour
{
    public AthenaAudioController1 athenaAudioController1;
    public GameObject hint1;
    public GameObject hint2;
    public GameObject gaze;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 当物体A进入物体B的触发器时会调用该方法
    private void OnTriggerEnter(Collider other)
    {
        // 判断触发的物体是否是物体A
        if (other.gameObject.CompareTag("ID"))
        {
            Debug.Log("ID Tapped");
            athenaAudioController1.PlayVoiceClip(0);
            hint1.SetActive(false);
            hint2.SetActive(true);

            // 启动协程延时15秒后启用gaze
            StartCoroutine(EnableGazeAfterDelay(15f));
        }
    }

    // 协程：延时15秒后启用gaze物体
    private IEnumerator EnableGazeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gaze.SetActive(true);
    }
}

