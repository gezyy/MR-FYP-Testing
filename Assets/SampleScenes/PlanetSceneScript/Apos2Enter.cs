using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apos2Enter : MonoBehaviour
{
    public AthenaAudioController athenaAudioController;
    public GameObject hint3;
    public GameObject hint4;
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
        if (other.gameObject.CompareTag("Athena"))
        {
            Debug.Log("物体A进入了物体B的触发器区域！");
            athenaAudioController.PlayVoiceClip(3);
            hint3.SetActive(false);
            hint4.SetActive(true);

        }
    }
}
