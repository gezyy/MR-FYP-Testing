using UnityEngine;

public class LeftControllerGun: MonoBehaviour
{
    [SerializeField] private Transform leftControllerPivot;
    [SerializeField] private GameObject targetObject; // 预先放置在场景中的物体

    private bool objectHeld;

    private void Update()
    {
        // 按下左手握紧按钮
        if (!objectHeld && OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            // 启用物体并跟随左手控制器
            targetObject.SetActive(true);
            targetObject.transform.position = leftControllerPivot.position;
            objectHeld = true;
        }

        // 当物体被抓住时，持续跟随左手控制器移动
        if (objectHeld)
        {
            targetObject.transform.position = leftControllerPivot.position;

            // 松开左手握紧按钮，物体保持当前状态
            if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
            {
                targetObject.SetActive(false);
                objectHeld = false;
            }
        }
    }
}
