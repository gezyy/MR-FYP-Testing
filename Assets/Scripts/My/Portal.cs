using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("The player's camera, used to determine proximity to the portal.")]
    [SerializeField]
    private Camera playerCamera;  // 玩家相机

    [Tooltip("The minimum distance at which the player is considered to be in the portal area.")]
    [SerializeField]
    private float activationDistance = 1.0f;  // 传送门激活距离

    [Tooltip("The target scene name to load upon activation.")]
    [SerializeField]
    private string targetSceneName;  // 目标场景名称

    void Update()
    {
        // 确保玩家相机已分配
        if (playerCamera == null)
        {
            Debug.LogError("Player camera not assigned. Please assign the player camera in the inspector.");
            return;
        }

        // 计算玩家相机与传送门的距离
        float distanceToPortal = Vector3.Distance(playerCamera.transform.position, transform.position);

        // 如果玩家相机在激活距离内，则切换场景
        if (distanceToPortal <= activationDistance)
        {
            // 切换到目标场景
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
