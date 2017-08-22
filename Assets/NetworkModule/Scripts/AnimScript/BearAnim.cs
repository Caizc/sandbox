using UnityEngine;
using System.Collections;

/// <summary>
/// 小熊动画控制
/// </summary>
public class BearAnim : MonoBehaviour
{
    public GameObject RedBear;
    public GameObject BlueBear;

    private Animator _redAnimator;
    private Animator _blueAnimator;

    // 方向阻尼
    private float _directionDamp = 0.25f;

    // 速度阻尼
    private float _speedDamp = 0.5f;

    private AnimatorStateInfo stateinfo;

    void Start()
    {
        //Vector3 redPOS = RedBear.transform.position;
        //Vector3 bluePOS = BlueBear.transform.position;

        GameData.RedX = -1;
        GameData.RedY = 0.1f;
        GameData.RedZ = 0;
        GameData.BlueX = 1;
        GameData.BlueY = -0.1f;
        GameData.BlueZ = 0;
        GameData.GetRedX = -1;
        GameData.GetRedY = 0.1f;
        GameData.GetRedZ = 0;
        GameData.GetBlueX = 1;
        GameData.GetBlueY = 0.1f;
        GameData.GetBlueZ = 0;

        _redAnimator = RedBear.GetComponent<Animator>();
        _blueAnimator = BlueBear.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // TODO: 这里可以优化为，等服务端分配该客户端控制哪一个单位后，再执行 applyRootMotion 设置，而不是每次 FixedUpdate 中执行一次

        #region 待优化

        Animator redBearAnim = RedBear.GetComponent<Animator>();
        Animator blueBearAnim = BlueBear.GetComponent<Animator>();

        if (GameData.RedOrBlue == 0)
        {
            redBearAnim.applyRootMotion = true;
            blueBearAnim.applyRootMotion = false;
        }
        else if (GameData.RedOrBlue == 1)
        {
            redBearAnim.applyRootMotion = false;
            blueBearAnim.applyRootMotion = true;
        }

        #endregion

        // 获取当前动画信息
        AnimatorStateInfo redStateinfo = _redAnimator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo blueStateinfo = _blueAnimator.GetCurrentAnimatorStateInfo(0);

        _redAnimator.SetFloat("Direction", GameData.GetRedDir, _directionDamp, Time.deltaTime); // 设置红熊方向
        _redAnimator.SetFloat("Speed", GameData.GetRedSpeed);
        _blueAnimator.SetFloat("Direction", GameData.GetBlueDir, _directionDamp, Time.deltaTime); // 设置蓝熊方向
        _blueAnimator.SetFloat("Speed", GameData.GetBlueSpeed);

        // 单机调试使用
        //Red_animator.SetFloat("Direction", GameData.RedDir, _directionDamp, Time.deltaTime);
        //Red_animator.SetFloat("Speed", GameData.RedSpeed);
        //Blue_animator.SetFloat("Direction", GameData.BlueDir, _directionDamp, Time.deltaTime);
        //Blue_animator.SetFloat("Speed", GameData.BlueSpeed);

        // 当前是跑步状态且按下 Jump 按钮可跳跃
        if (blueStateinfo.IsName("Base Layer.Run") && GameData.GetisBlueJump == 1)
        {
            _blueAnimator.SetTrigger("Jump"); // 跳
            GameData.IsBlueJump = 0;
        }

        if (redStateinfo.IsName("Base Layer.Run") && GameData.GetisRedJump == 1)
        {
            _redAnimator.SetTrigger("Jump"); // 跳
            GameData.IsRedJump = 0;
        }

        // 设置熊的位置和角度
        if (GameData.RedOrBlue == 1)
        {
            RedBear.transform.localPosition =
                new Vector3(GameData.GetRedX, GameData.GetRedY, GameData.GetRedZ);
            RedBear.transform.eulerAngles = new Vector3(GameData.GetRedRotX, GameData.GetRedRotY, GameData.GetRedRotZ);
        }
        else if (GameData.RedOrBlue == 0)
        {
            BlueBear.transform.localPosition = new Vector3(GameData.GetBlueX, GameData.GetBlueY, GameData.GetBlueZ);
            BlueBear.transform.eulerAngles = new Vector3(GameData.GetBlueRotX, GameData.GetBlueRotY,
                GameData.GetBlueRotZ);
        }
    }
}