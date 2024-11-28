using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // 导入UI命名空间
using UnityStandardAssets.CrossPlatformInput;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // 如果按下了强制重置按钮...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            // 重载当前场景
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).path);
        }
    }
}
