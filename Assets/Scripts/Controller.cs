using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject BulletsParent;

    // 攻击角度（等于0：直射；小于0：逆时针；大于0：顺时针；等于正/负360：圆形；大于负360或小于正360：扇形）
    public float AttackDegree;

    // 间隔发射角度
    // 如该参数不为 0，则启用另一种模式：
    // - 以 AttackDegree 计算出来的初始发射角度；
    // - 每隔 IntervalTime 时长，间隔 IntervalDegree 发射一颗子弹；
    // - 达到 TotalCount 颗子弹时结束（即使已经超出攻击角度）。
    // 使用这种模式可以配置出‘螺旋散射’的效果
    public float IntervalDegree;

    // 间隔发射时间
    public float IntervalTime;

    // 子弹总个数
    public int TotalCount;

    // 前进方向初速度
    public float ForwardSpeed;

    // 振动方向初速度
    public float ShakeSpeed;

    // 振动周期（走完一个完整周期的时间）
    public float ShakeFrequence;

    private GameObject _hero;

    void Start()
    {
        _hero = GameObject.FindWithTag("Player");
    }

    public void OnButtonClick()
    {
        Debug.Log("发射 ===>");

        Cast();
    }

    private void Cast()
    {
        // - 发射起点位置 position
        // - 计算每颗子弹转向 rotation
        // - 调用协程，传入子弹的初始位置 position 和转向 rotation，每隔 IntervalTime 时长创建一颗子弹
        // - 传入调整每颗子弹运行轨迹的其他参数

        Vector3 heroPos = _hero.transform.position;
        Quaternion heroRot = _hero.transform.rotation;

        // 限制 (-360 <= 攻击角度 <= 360)
        AttackDegree = Mathf.Clamp(AttackDegree, -360f, 360f);

        // 第一颗子弹发射角度
        float firstBulletDegree = (180f - AttackDegree) / 2f;

        // 相邻子弹间隔发射角度
        float perBulletDegree;

        if (Mathf.Approximately(IntervalDegree, 0.0f))
        {
            perBulletDegree = AttackDegree / (TotalCount - 1);
        }
        else
        {
            perBulletDegree = IntervalDegree;
        }

        Vector3 position = heroPos;
        Quaternion rotation = Quaternion.identity;

        for (int iCounter = 0; iCounter < TotalCount; iCounter++)
        {
            // 在发射点朝向的基础上，计算出每颗子弹的发射方向
            rotation = Quaternion.Euler(heroRot.eulerAngles.x,
                heroRot.eulerAngles.y + (firstBulletDegree + perBulletDegree * iCounter) - 90f, heroRot.eulerAngles.z);

            // 调用协程，每隔 IntervalTime 时长创建一颗子弹
            StartCoroutine(SpawnBullet(iCounter, position, rotation));
        }
    }

    private IEnumerator SpawnBullet(int iCounter, Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(IntervalTime * iCounter);

        GameObject bullet = Instantiate(BulletPrefab, position, rotation, BulletsParent.transform);

        // 设置每颗子弹的运行轨迹参数
        Moving moving = bullet.GetComponentInChildren<Moving>();
        moving.ForwardSpeed = ForwardSpeed;
        moving.ShakeSpeed = ShakeSpeed;
        moving.ShakeFrequence = ShakeFrequence;
    }
}