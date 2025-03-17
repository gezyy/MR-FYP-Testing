using UnityEngine;
using UnityEngine.UI;

public class GrayScaleController : MonoBehaviour
{
    public Slider grayScaleSlider;  // 引用Slider控件
    public Image targetImage;       // 引用要更新的UI Image

    private Material originalMaterial;  // 原始材质引用，用于后续替换
    private Shader currentShader;  // 当前的Shader引用

    void Start()
    {
        // 获取当前材质的Shader
        currentShader = targetImage.material.shader;

        // 初始化Slider，并监听其值变化
        grayScaleSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // 备份原始材质
        originalMaterial = targetImage.material;
    }

    void OnSliderValueChanged(float value)
    {
        // 创建一个新的材质，并应用当前的Shader
        Material newMaterial = new Material(currentShader);

        // 设置新材质的灰度因子
        newMaterial.SetFloat("_GrayScaleFactor", value);

        // 将新的材质应用到 Image 上
        targetImage.material = newMaterial;

        // 可以根据需要，也可以选择释放之前的材质来节省内存
         Destroy(originalMaterial);  // 如果需要销毁原始材质，释放内存
    }
}
