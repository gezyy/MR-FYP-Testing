using UnityEngine;

public class SampleShooter : MonoBehaviour
{
    public static SampleShooter Instance = null; // 单例实例

    public GameObject _micPrefab;
    public Transform _rightHandAnchor;

    GameObject _rightMic;
    private bool isShooting = false; // 控制射线是否显示

    // 将 isTrigger 设置为 public，这样其他脚本可以访问
    public bool isTrigger { get; private set; } = false;

    void Awake()
    {
        // 初始化单例
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 防止有多个实例
        }
    }

    void Start()
    {
        _rightMic = Instantiate(_micPrefab);
        _rightMic.transform.SetParent(_rightHandAnchor, false);
    }

    void Update()
    {
        // 按下右Trigger时开始发射射线，并让麦克风材质发亮
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            isShooting = true;
            isTrigger = true; // 设置 isTrigger 为 true
        }

        // 松开右Trigger时停止发射射线，并取消发亮
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            isShooting = false;
            isTrigger = false; // 设置 isTrigger 为 false
        }
    }
}

