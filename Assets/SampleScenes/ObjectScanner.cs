using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectScanner : MonoBehaviour
{
    public Camera camera1; // 相机

    // 用于存储每个物体的UI、显示模型和粒子系统
    [System.Serializable]
    public class ObjectData
    {
        public string tag; // 物体的Tag
        public GameObject ui; // 对应的UI组件
        public GameObject model; // 对应的显示模型
        public ParticleSystem particleSystem; // 对应的粒子系统
    }

    public List<ObjectData> objectDataList = new List<ObjectData>(); // 可编辑的物体数据列表
    public GameObject defaultUI; // 未找到物体时的UI
    public GameObject defaultModel; // 未找到物体时的模型

    void Start()
    {
        // 初始化：隐藏所有UI和模型，停止粒子系统
        foreach (var data in objectDataList)
        {
            if (data.ui != null) data.ui.SetActive(false);
            if (data.model != null) data.model.SetActive(false);
            if (data.particleSystem != null) data.particleSystem.Stop();
        }
        if (defaultUI != null) defaultUI.SetActive(false);
        if (defaultModel != null) defaultModel.SetActive(false);
    }

    public void OnScanButtonPressed()
    {
        // 隐藏所有UI和模型，停止粒子系统
        foreach (var data in objectDataList)
        {
            if (data.ui != null) data.ui.SetActive(false);
            if (data.model != null) data.model.SetActive(false);
            if (data.particleSystem != null) data.particleSystem.Stop();
        }
        if (defaultUI != null) defaultUI.SetActive(false);
        if (defaultModel != null) defaultModel.SetActive(false);

        // 从相机中心发射射线
        Ray ray = new Ray(camera1.transform.position, camera1.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 检查碰撞物体是否匹配Tag
            foreach (var data in objectDataList)
            {
                if (hit.collider.CompareTag(data.tag))
                {
                    // 显示对应的UI和模型
                    if (data.ui != null) data.ui.SetActive(true);
                    if (data.model != null) data.model.SetActive(true);

                    // 启用对应的粒子系统
                    if (data.particleSystem != null)
                    {
                        data.particleSystem.transform.position = hit.collider.transform.position;
                        data.particleSystem.Play();
                    }

                    return;
                }
            }

            // 如果没有匹配的Tag，显示默认UI和模型
            if (defaultUI != null) defaultUI.SetActive(true);
            if (defaultModel != null) defaultModel.SetActive(true);
        }
        else
        {
            // 如果未检测到任何物体，显示默认UI和模型
            if (defaultUI != null) defaultUI.SetActive(true);
            if (defaultModel != null) defaultModel.SetActive(true);
        }
    }
}
