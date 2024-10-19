using UnityEngine;

public class SampleShooter : MonoBehaviour
{
    public static SampleShooter Instance = null; // 单例实例

    public GameObject _micPrefab;
    public GameObject _LhandPrefab;
    public Transform _leftHandAnchor;
    public Transform _rightHandAnchor;

    GameObject _leftHand;
    GameObject _rightMic;
    private bool isShooting = false; // 控制射线是否显示
    private Material micMaterial; // 麦克风的材质
    private Color originalEmissionColor; // 材质的原始发射颜色

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
        _leftHand = Instantiate(_LhandPrefab);
        _leftHand.transform.SetParent(_leftHandAnchor, false);
        _rightMic = Instantiate(_micPrefab);
        _rightMic.transform.SetParent(_rightHandAnchor, false);

        // 查找子物体 Fillet2 并获取其材质
        Transform fillet2Transform = _rightMic.transform.Find("Microphone/Fillet2");
        if (fillet2Transform != null)
        {
            Renderer micRenderer = fillet2Transform.GetComponent<Renderer>();
            if (micRenderer != null)
            {
                micMaterial = micRenderer.materials[0];
                // 获取材质的初始发射颜色
                if (micMaterial.HasProperty("_EmissionColor"))
                {
                    originalEmissionColor = micMaterial.GetColor("_EmissionColor");
                }
            }
        }
        else
        {
            Debug.LogError("Fillet2 not found!");
        }
    }

    void Update()
    {
        // 按下右Trigger时开始发射射线，并让麦克风材质发亮
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            isShooting = true;
            isTrigger = true; // 设置 isTrigger 为 true
            SetMicEmissionColor(Color.yellow); // 发亮（设为黄色，也可以改成其他颜色）
        }

        // 松开右Trigger时停止发射射线，并取消发亮
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            isShooting = false;
            isTrigger = false; // 设置 isTrigger 为 false
            SetMicEmissionColor(originalEmissionColor); // 恢复原来的颜色
        }
    }

    // 设置麦克风材质的发射颜色
    private void SetMicEmissionColor(Color emissionColor)
    {
        if (micMaterial != null && micMaterial.HasProperty("_EmissionColor"))
        {
            micMaterial.SetColor("_EmissionColor", emissionColor);

            // 如果使用的是标准着色器，需要启用发射效果
            if (emissionColor != Color.black)
            {
                micMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                micMaterial.DisableKeyword("_EMISSION");
            }
        }
    }
}

