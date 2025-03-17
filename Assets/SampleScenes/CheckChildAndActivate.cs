using UnityEngine;

public class CheckChildAndActivate : MonoBehaviour
{
    public GameObject objectToCheck; // 需要检查的物体
    public GameObject Feature;
    public GameObject hint1;
    public AthenaAudioController1 athenaAudioController1;
    void Update()
    {
        // 检查物体下是否有子物体
        if (objectToCheck.transform.childCount == 0)
        {
            // 如果没有子物体，启用物体A
            hint1.SetActive(true);
            athenaAudioController1.PlayVoiceClip(1);
            Feature.SetActive(false);
        }
    }
}
