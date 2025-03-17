using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthenaGazeActive : MonoBehaviour
{
    // 引用注视检测器
    public GazeFocusDetector gazeFocusDetector;
    public GameObject Ahint1;
    public GameObject Ahint2;
    private bool isTargetInFocus = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 检查目标是否在注视范围内
        if (gazeFocusDetector != null)
        {
            if (gazeFocusDetector.IsTargetInFocus && !isTargetInFocus)
            {
                // 如果目标开始被注视，开始激活物体
                isTargetInFocus = true;
                Ahint1.SetActive(false);
                Ahint2.SetActive(true);
            }
        }
    }
}
