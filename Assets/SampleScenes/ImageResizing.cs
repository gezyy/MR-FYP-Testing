using UnityEngine;
using UnityEngine.UI;

public class ImageResizing : MonoBehaviour
{
    public Slider sizeSlider;  // 绑定到场景中的Slider
    public Image targetImage;  // 绑定到场景中的Image

    private Vector2 defaultSize = new Vector2(100f, 100f);  // 默认大小
    private float minSize = 50f;  // 最小尺寸
    private float maxSize = 200f; // 最大尺寸

    void Start()
    {
        // 确保Slider的值在0和1之间
        sizeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // 当Slider值改变时，调整Image的大小
    private void OnSliderValueChanged(float value)
    {
        // 计算新的宽高
        float newSize = Mathf.Lerp(minSize, maxSize, value);  // 根据Slider的值计算大小

        // 应用新的大小到Image
        targetImage.rectTransform.sizeDelta = new Vector2(newSize, newSize);
    }
}
