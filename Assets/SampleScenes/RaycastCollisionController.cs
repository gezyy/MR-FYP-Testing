using UnityEngine;

public class RaycastCollisionController : MonoBehaviour
{
    public Camera mainCamera;      // 主摄像头
    public string targetTag1 = "Orange";  // 要检测的Tag
    public string targetTag2 = "Ice";  // 要检测的Tag
    public string targetTag3 = "Egipt";  // 要检测的Tag

    public GameObject uiObjectA;   // UI物体A
    public GameObject uiObjectB;   // UI物体B
    public GameObject uiTextA;
    public GameObject uiTextB;
    public GameObject uiData;

    public float collisionTimeThreshold = 5f;  // 碰撞持续时间阈值

    public bool iceData = false; // 公开的bool变量，用于标记是否与Orange碰撞
    public bool orangeData = false; // 公开的bool变量，用于标记是否与Orange碰撞
    public bool egiptData = false; // 公开的bool变量，用于标记是否与Orange碰撞

    private float collisionTime = 0f;  // 碰撞持续时间计时器
    private bool isColliding = false;  // 是否正在与物体碰撞
    private GameObject collidedObject = null; // 当前碰撞的物体
    private bool hasSwitchedToB = false; // 确保已经切换到 UI 物体 B

    public Material scanMat;       // 扫描材质
    public float scanSpeed = 20f;  // 扫描速度
    private Vector3 scanPoint = Vector3.zero; // 扫描中心点
    private Camera scanCam;
    public float scanTimer = 0f;  // 扫描计时器

    void Awake()
    {
        scanCam = mainCamera;  // 获取摄像机引用
        scanCam.depthTextureMode |= DepthTextureMode.Depth;
        scanCam.depthTextureMode |= DepthTextureMode.DepthNormals;
    }

    void Update()
    {
        // 从相机的屏幕中心发射射线
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 如果射线击中并且物体带有特定的Tag
            if (hit.collider.CompareTag(targetTag1) || hit.collider.CompareTag(targetTag2) || hit.collider.CompareTag(targetTag3))
            {
                Debug.Log("Find Planet");

                // 如果之前没有碰撞过或是与同一个物体发生碰撞
                if (collidedObject == null || collidedObject != hit.collider.gameObject)
                {
                    collidedObject = hit.collider.gameObject;
                    collisionTime = 0f;  // 重置碰撞计时器
                    isColliding = true;
                    hasSwitchedToB = false;  // 碰撞重新开始时，确保 UI 物体 A 会被启用

                    // 启用UI物体A
                    uiObjectA.SetActive(true);
                    uiObjectB.SetActive(false);
                    uiTextA.SetActive(true);
                    uiTextB.SetActive(false);
                    uiData.SetActive(true);
                }

                // 如果射线与同一物体的碰撞持续时间大于或等于阈值，切换到UI物体B
                if (isColliding)
                {
                    collisionTime += Time.deltaTime;
                    if (collisionTime >= collisionTimeThreshold && !hasSwitchedToB)
                    {
                        scanTimer = 0;
                        scanPoint = hit.point; // 更新扫描中心为当前碰撞点
                        uiObjectA.SetActive(false);
                        uiObjectB.SetActive(true);
                        uiTextA.SetActive(false);
                        uiTextB.SetActive(true);
                        uiData.SetActive(true);
                        hasSwitchedToB = true;  // 设置为已切换到 B

                        // 根据碰撞物体的Tag更新数据
                        if (hit.collider.CompareTag("Orange"))
                        {
                            orangeData = true;
                        }
                        if (hit.collider.CompareTag("Ice"))
                        {
                            iceData = true;
                        }
                        if (hit.collider.CompareTag("Egipt"))
                        {
                            egiptData = true;
                        }

                        // 触发扫描效果
                        TriggerScanEffect(hit.point);
                    }
                }
            }
        }
        else
        {
            // 如果射线没有与任何目标物体碰撞
            if (isColliding)
            {
                // 隐藏UI
                uiObjectA.SetActive(false);
                uiObjectB.SetActive(false);
                uiTextA.SetActive(false);
                uiTextB.SetActive(false);
                uiData.SetActive(false);
                isColliding = false;
                collidedObject = null;  // 清除当前碰撞物体
                orangeData = false;  // 重置orangeData
                iceData = false;  // 重置iceData
                egiptData = false;  // 重置egiptData
                hasSwitchedToB = false;  // 重置切换状态
            }
        }
    }

    // 执行扫描效果
    private void TriggerScanEffect(Vector3 hitPoint)
    {
        // 使用传递进来的 hitPoint 来更新材质的扫描中心
        scanMat.SetVector("_ScanCenter", hitPoint);  // 改为使用 hitPoint
        scanMat.SetFloat("_ScanRange", scanTimer * scanSpeed);  // 设置扫描范围
        scanMat.SetMatrix("_CamToWorld", scanCam.cameraToWorldMatrix);  // 设置相机到世界的矩阵

        // 在渲染过程中应用扫描效果
        scanCam.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);
        Graphics.Blit(null, scanCam.targetTexture, scanMat);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // 确保每次渲染时，扫描效果都会被更新
        scanMat.SetFloat("_CamFar", mainCamera.farClipPlane);
        Graphics.Blit(source, destination, scanMat);
    }
}
