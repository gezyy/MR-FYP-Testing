using UnityEngine;
using UnityEngine.UI;

public class UITextPositionController: MonoBehaviour
{
    public Text uiText;  // 要控制的 UI Text 对象
    public float speed = 6f;  // 每秒增加的速度
    public float targetIncrease = 30f;  // 目标增加的总值

    private float totalIncrease = 0f;  // 当前已经增加的总值
    private bool isIncreasing = false;  // 是否开始增加位置
    private Vector3 originalPosition;  // 原始位置

    void OnEnable()
    {
        // 记录原始位置，并重置已增加的值
        originalPosition = uiText.rectTransform.localPosition;
        totalIncrease = 0f;  // 重置增加值
        isIncreasing = true;  // 开始增加位置
    }

    void Update()
    {
        if (isIncreasing)
        {
            // 每帧增加 Y 轴位置，直到总增加值达到目标
            float increaseAmount = speed * Time.deltaTime;
            totalIncrease += increaseAmount;

            if (totalIncrease >= targetIncrease)
            {
                increaseAmount -= totalIncrease - targetIncrease;  // 让总增量不超过目标值
                totalIncrease = targetIncrease;
                isIncreasing = false;  // 达到目标后停止
            }

            // 更新 UI Text 的位置
            uiText.rectTransform.localPosition = originalPosition + new Vector3(0, totalIncrease, 0);
        }
    }
}
