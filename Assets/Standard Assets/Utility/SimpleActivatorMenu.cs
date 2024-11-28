using System;
using UnityEngine;
using UnityEngine.UI;  // 导入UI命名空间

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // 使用UI.Text替换GUIText
        public Text camSwitchButton;  // Text类型代替GUIText
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            // 激活的物体从数组中的第一个开始
            m_CurrentActiveObject = 0;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }

        public void NextCamera()
        {
            int nextactiveobject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextactiveobject);
            }

            m_CurrentActiveObject = nextactiveobject;
            camSwitchButton.text = objects[m_CurrentActiveObject].name;
        }
    }
}

