using Oculus.Interaction;
using UnityEngine;

namespace Oculus.Interaction.HandGrab
{
    // 自定义的 MovementProvider，创建一个不移动的 Movement
    public class NoMovementProvider: MonoBehaviour, IMovementProvider
    {
        public IMovement CreateMovement()
        {
            // 返回一个不执行任何移动的 Movement 实例
            return new NoMovement();
        }
    }

    // 实现 IMovement 接口，但不对物体位置进行任何更新
    public class NoMovement : IMovement
    {
        public Pose Pose => _currentPose;
        public bool Stopped => true;

        // 保持当前物体的初始状态
        private Pose _currentPose = Pose.identity;

        // MoveTo 只是简单保存目标位置，不进行实际的移动
        public void MoveTo(Pose target)
        {
            // 目标位置不影响物体位置，保持当前状态
            _currentPose = target;
        }

        // 更新目标位置时，也只是保存目标，但不执行任何动作
        public void UpdateTarget(Pose target)
        {
            _currentPose = target;
        }

        // 停止并设置物体的初始位置
        public void StopAndSetPose(Pose source)
        {
            _currentPose = source;  // 保持当前位置
        }

        // 每帧调用，但不做任何事
        public void Tick() { }
    }
}
