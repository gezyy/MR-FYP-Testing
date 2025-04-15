using UnityEngine;
using UnityEngine.UI;  // 引入UI库

public class ImageManager: MonoBehaviour
{
    public Image[] images;  // 存储图片的数组
    public Button nextButton;  // 下一张按钮
    public Button previousButton;  // 上一张按钮

    private int currentIndex = 0;  // 当前显示的图片索引

    void Start()
    {
        // 初始化时，确保只有第一张图片是启用的
        UpdateImageDisplay();

        // 给按钮添加点击事件
        nextButton.onClick.AddListener(OnNextButtonClicked);
        previousButton.onClick.AddListener(OnPreviousButtonClicked);
    }

    // 更新图片显示
    void UpdateImageDisplay()
    {
        // 禁用所有图片
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }

        // 启用当前索引对应的图片
        if (images.Length > 0 && currentIndex >= 0 && currentIndex < images.Length)
        {
            images[currentIndex].gameObject.SetActive(true);
        }
    }

    // 下一张按钮点击事件
    void OnNextButtonClicked()
    {
        if (currentIndex < images.Length - 1)  // 防止索引越界
        {
            currentIndex++;  // 索引加1
            UpdateImageDisplay();  // 更新显示的图片
        }
    }

    // 上一张按钮点击事件
    void OnPreviousButtonClicked()
    {
        if (currentIndex > 0)  // 防止索引越界
        {
            currentIndex--;  // 索引减1
            UpdateImageDisplay();  // 更新显示的图片
        }
    }
}
