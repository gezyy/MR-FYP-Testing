using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class RightControllerGun: MonoBehaviour
{
    [SerializeField] private Transform rightControllerPivot; // 手柄位置
    [SerializeField] private GameObject ballPrefab; // 小球预制体
    [SerializeField] private int initialAmmo = 3; // 初始子弹数量
    [SerializeField] private Text ammoText; // 子弹数量显示的Text对象

    private int currentAmmo; // 当前子弹数量

    private void Start()
    {
        // 初始化子弹数量
        currentAmmo = initialAmmo;
        // 初始化UI显示
        UpdateAmmoText();
    }

    private void Update()
    {
        // 按下右手触发按钮
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            if (currentAmmo > 0) // 如果还有子弹
            {
                Fire(); // 生成球
                currentAmmo--; // 子弹数量减少
                UpdateAmmoText(); // 更新UI显示
                Debug.Log("剩余子弹数量：" + currentAmmo); // 打印当前子弹数量
            }
            else
            {
                Debug.Log("没有子弹了！"); // 没有子弹时提示
            }
        }
    }

    private void Fire()
    {
        // 生成球
        var newBall = Instantiate(ballPrefab);

        // 设置球的位置和方向
        newBall.transform.position = rightControllerPivot.position;

        // 设置球的初始速度
        const float throwSpeed = 10f;
        var rigidbody = newBall.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.velocity = rightControllerPivot.forward * throwSpeed;
        }
    }

    public void Reload(int ammo) // 增加一个装填子弹的方法
    {
        currentAmmo += ammo;
        UpdateAmmoText(); // 更新UI显示
        Debug.Log("子弹已装填，当前子弹数量：" + currentAmmo);
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "" + currentAmmo; // 更新Text内容
        }
        else
        {
            Debug.LogWarning("Ammo Text UI未设置！");
        }
    }
}
