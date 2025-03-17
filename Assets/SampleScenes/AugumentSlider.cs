using UnityEngine;
using UnityEngine.UI;

public class AugumentSlider : MonoBehaviour
{
    public Slider exposureSlider;  // 引用Slider控件
    public Image targetImage;      // 引用要更新的UI Image

    private Material originalMaterial;  // 原始材质引用，用于后续替换
    private Shader currentShader;  // 当前的Shader引用

    void Start()
    {
        // 获取当前材质的Shader
        currentShader = targetImage.material.shader;

        // 初始化Slider，并监听其值变化
        exposureSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // 备份原始材质
        originalMaterial = targetImage.material;
    }

    void OnSliderValueChanged(float value)
    {
        // 创建一个新的材质，并应用当前的Shader
        Material newMaterial = new Material(currentShader);

        // 设置新的曝光值，范围为1到50
        float exposureValue = Mathf.Lerp(1f, 50f, value);
        newMaterial.SetFloat("_Exposure", exposureValue);

        // 将新的材质应用到 Image 上
        targetImage.material = newMaterial;

        // 可以根据需要，也可以选择释放之前的材质来节省内存
        Destroy(originalMaterial);  // 如果需要销毁原始材质，释放内存
    }
}
