using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // 前进方向初速度
    public float ForwardSpeed;

    // 振动方向初速度
    public float ShakeSpeed;

    // 振动周期（走完一个完整周期的时间）
    public float ShakeFrequence;

    // 子弹已飞行时间（每一个新的周期开始时都会重置）
    private float _time;

    // 二分之一振动周期
    private float _halfT;

    // 四分之一振动周期
    private float _quarterT;

    // Z 轴方向上的速度
    private float _vz;

    // X 轴方向上的速度
    private float _vx;

    private Rigidbody _rigidbody;

    void Start()
    {
        _time = 0f;
        _halfT = ShakeFrequence / 2f;
        _quarterT = ShakeFrequence / 4f;
        _vz = ForwardSpeed;
        _vx = ShakeSpeed;

        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time < _halfT)
        {
            // 计算前二分之一振动周期 X 轴方向上的速度
            _vx = ShakeSpeed * (1f - _time / _quarterT);
        }
        else if (_time <= ShakeFrequence)
        {
            // 计算后二分之一振动周期 X 轴方向上的速度
            _vx = (-1f * ShakeSpeed) * (1f - (_time - _halfT) / _quarterT);
        }
        else
        {
            // 走完一个完整周期后重置计时器
            _time = 0f;
        }

        Vector3 vz = new Vector3(0f, 0f, _vz);
        Vector3 vx = new Vector3(_vx, 0f, 0f);

        // 本地坐标系下的子弹瞬时速度
        Vector3 vLocal = vz + vx;
        // 世界坐标系下的子弹瞬时速度
        Vector3 vWorld = transform.TransformVector(vLocal);

        // 将瞬时速度赋值给 Rigidbody
        _rigidbody.velocity = vWorld;
    }
}