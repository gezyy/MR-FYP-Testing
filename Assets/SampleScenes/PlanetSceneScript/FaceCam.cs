using UnityEngine;

public class FaceCam : MonoBehaviour
{
    // 可选：设置一个目标摄像机，不设置时默认使用主摄像机
    public Camera targetCamera;

    void Start()
    {
        // 如果没有设置摄像机，使用主摄像机
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {
            // 获取摄像机的位置
            Vector3 cameraPosition = targetCamera.transform.position;

            // 计算物体朝向摄像机的方向
            Vector3 directionToFace = cameraPosition - transform.position;

            // 计算朝向并添加180度水平翻转
            Quaternion targetRotation = Quaternion.LookRotation(directionToFace) * Quaternion.Euler(0, 90, 0);

            // 应用旋转
            transform.rotation = targetRotation;
        }
    }
}


