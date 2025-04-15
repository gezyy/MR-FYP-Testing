using UnityEngine;
using System.Collections.Generic;

public class AircraftMovement : MonoBehaviour
{
    // 储存多个目标位置
    public List<Vector3> positions = new List<Vector3>();

    // 移动参数
    public float moveSpeed = 5f;  // 飞行器最大速度
    public float acceleration = 1f;  // 加速度
    public float stopAcceleration = 2f;  // 停止时的加速度

    // 上下移动参数
    public float upDownHeight = 2f;  // 上下移动的高度
    public float upDownSpeed = 2f;  // 上下移动的速度

    // 内部控制变量
    private bool isMoving = false;  // 是否正在前往某个目标位置
    private int currentTargetIndex = -1;  // 当前目标位置的索引
    private Vector3 currentVelocity = Vector3.zero;  // 当前的速度
    private float baseYPosition = 0f;
    private List<int> targetIndices = new List<int>();  // 记录多个目标位置的索引
    private int currentTargetInSequence = 0;  // 记录当前要前往的目标索引（多目标模式）

    void Update()
    {
        // 如果没有目标位置，进行上下运动
        if (!isMoving)
        {
            PerformUpDownMovement();
        }
        else
        {
            // 移动到目标位置
            MoveToTarget();
        }
    }

    // 公开的控制飞行器移动到指定位置的方法
    // 这里传入的参数是目标位置列表的索引
    public void MoveToPositionByIndex(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < positions.Count)
        {
            currentTargetIndex = targetIndex;
            isMoving = true;
            baseYPosition = positions[targetIndex].y;
        }
        else
        {
            Debug.LogError("无效的目标位置索引！");
        }
    }
    // 新方法，用来让飞行器依次按顺序移动到多个位置
    public void MoveThroughMultiplePositions(List<int> indices)
    {
        if (indices != null && indices.Count > 0)
        {
            targetIndices = indices;
            currentTargetInSequence = 0;
            isMoving = true;
            // 设置起始目标
            MoveToNextTarget();
        }
        else
        {
            Debug.LogError("传入的索引列表无效！");
        }
    }

    // 实现飞行器移动到目标位置的逻辑
    private void MoveToTarget()
    {
        if (currentTargetIndex < 0 || currentTargetIndex >= positions.Count)
        {
            isMoving = false;
            return;
        }

        Vector3 targetPosition = positions[currentTargetIndex];

        // 计算与目标位置的距离
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > 0.1f)
        {
            // 飞行器移动的加速度
            float speed = Mathf.Lerp(0, moveSpeed, acceleration * Time.deltaTime);

            // 通过物理引擎移动
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            // 到达目标位置后停止并应用停止加速度
            if (currentVelocity.magnitude > 0)
            {
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, stopAcceleration * Time.deltaTime);
                transform.position += currentVelocity * Time.deltaTime;
            }
            else
            {
                // 到达目标位置，检查是否有下一个目标
                if (targetIndices.Count > 0 && currentTargetInSequence < targetIndices.Count - 1)
                {
                    currentTargetInSequence++;
                    MoveToNextTarget();
                }
                else
                {
                    // 完成所有目标位置的移动
                    isMoving = false;
                }
            }
        }
    }
    // 设置下一个目标位置
    private void MoveToNextTarget()
    {
        currentTargetIndex = targetIndices[currentTargetInSequence];
        baseYPosition = positions[currentTargetIndex].y; // 更新基准y坐标
    }
    // 飞行器的上下移动
    private void PerformUpDownMovement()
    {
        float newY = baseYPosition + Mathf.PingPong(Time.time * upDownSpeed, upDownHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
