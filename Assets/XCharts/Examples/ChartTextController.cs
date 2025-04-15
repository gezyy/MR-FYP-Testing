using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;

namespace XCharts.Example
{
    public class ChartTextController: MonoBehaviour
    {
        // 引用文本框
        public Text textBox1;
        public Text textBox2;
        public Text textBox3;

        // 用于控制文本框的值
        private bool isTextBox1Fixed = false;
        private bool isTextBox2Fixed = false;

        private float textBox1Value = -1f;
        private float textBox2Value = -1f;
        private float textBox3Value = -1f;

        public float data1;
        public float data2;
        public float data3;

        public GameObject button;
        public GameObject chart;
        private bool isCoroutineRunning = false; // 检查协程是否已经在运行

        void Start()
        {
            // 初始化文本框为空白
            textBox1.text = "";
            textBox2.text = "";
            textBox3.text = "";
        }

        // OnSerieEnter 用来监听数据点的悬停事件
        public void OnSerieEnter(SerieEventData data)
        {
            float value = (float)data.value; // 显式转换 double -> float

            if (!isTextBox1Fixed)
            {
                // 当鼠标悬停时，给文本框1设置数据点的值
                textBox1.text = value.ToString();
                // 使用浮动比较判断数值是否匹配
                if (IsApproximatelyEqual(value, data1))
                {
                    isTextBox1Fixed = true; // 锁定文本框1
                    textBox1Value = value; // 存储固定值
                    Debug.Log("TextBox1 fixed at: " + textBox1Value);
                }
            }
            else if (!isTextBox2Fixed)
            {
                // 给文本框2设置数据点的值
                textBox2.text = value.ToString();

                if (IsApproximatelyEqual(value, data2))
                {
                    isTextBox2Fixed = true; // 锁定文本框2
                    textBox2Value = value; // 存储固定值
                    textBox3.text = data3.ToString(); // 固定文本框3为24.25
                    textBox3Value = data3;
                    Debug.Log("TextBox2 fixed at: " + textBox2Value);

                }
            }
        }

        // 也可以用 OnSerieExit 事件清空或重置文本框的值
        public void OnSerieExit(SerieEventData data)
        {
            // 只有当文本框没有被锁定时，才清空文本框内容
            if (!isTextBox1Fixed)
            {
                textBox1.text = "";
            }

            if (!isTextBox2Fixed)
            {
                textBox2.text = "";
            }
        }

        // 检查 textBox3 的值，并延迟禁用按钮
        void Update()
        {
            // 检查 textBox3 的内容是否等于固定值
            if (!string.IsNullOrEmpty(textBox3.text) && float.TryParse(textBox3.text, out float currentValue))
            {
                if (currentValue == data3 && !isCoroutineRunning)
                {
                    // 如果 textBox3 有固定值且协程未运行，启动协程延迟禁用按钮
                    StartCoroutine(DisableButtonAfterDelay(3f)); // 延迟5秒
                    isCoroutineRunning = true; // 标记协程正在运行
                }
            }
        }

        // 延迟禁用按钮的协程
        private IEnumerator DisableButtonAfterDelay(float delay)
        {
            // 等待指定的时间
            yield return new WaitForSeconds(delay);

            // 禁用按钮
            if (button != null && chart != null)
            {
                button.SetActive(false);
                chart.SetActive(false);
                Debug.Log("Button disabled after delay because textBox3 reached fixed value.");
            }

            isCoroutineRunning = false; // 重置协程运行标志
        }
        // 浮动数值比较函数，容忍误差
        private bool IsApproximatelyEqual(float a, float b)
        {
            const float epsilon = 0.0001f; // 误差容忍值
            return Mathf.Abs(a - b) < epsilon;
        }
    }
}
