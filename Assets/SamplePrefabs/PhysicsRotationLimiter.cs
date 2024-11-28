using UnityEngine;

public class PhysicsRotationLimiter: MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isGrabbed = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_isGrabbed)
        {
            // 获取当前物体的旋转
            Quaternion currentRotation = _rigidbody.rotation;

            // 限制物体的旋转：只允许 x 轴旋转
            _rigidbody.MoveRotation(new Quaternion(
                currentRotation.x, // 保留 x 轴的旋转
                currentRotation.y, // 保留 y 轴的旋转
                currentRotation.z, // 保留 z 轴的旋转
                currentRotation.w)); // 保留 w 轴的旋转
        }
    }

    public void OnGrabbed()
    {
        _isGrabbed = true;
    }

    public void OnReleased()
    {
        _isGrabbed = false;
    }
}
