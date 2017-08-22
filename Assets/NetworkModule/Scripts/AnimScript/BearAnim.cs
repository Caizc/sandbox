using UnityEngine;
using System.Collections;

public class BearAnim : MonoBehaviour {

    protected Animator Red_animator;
    protected Animator Blue_animator;
   
    public GameObject RedBear;
    public GameObject BlueBear;

    private float dirDemp = 0.25f;//方向阻尼
    private float speedDemp = 0.5f;//速度阻尼

    private AnimatorStateInfo stateinfo;

	// Use this for initialization
	void Start () {
        //Vector3 redPOS = RedBear.transform.position;
        //Vector3 bluePOS = BlueBear.transform.position;
        GameData.RedX = -1;
        GameData.RedY = 0.1f;
        GameData.RedZ = 0;
        GameData.BlueX = 1;
        GameData.BlueY = -0.1f;
        GameData.BlueZ = 0;
        GameData.getRedX = -1;
        GameData.getRedY = 0.1f;
        GameData.getRedZ = 0;
        GameData.getBlueX = 1;
        GameData.getBlueY = 0.1f;
        GameData.getBlueZ = 0;
        Red_animator = RedBear.GetComponent<Animator>();//获取动画控制器
        Blue_animator = BlueBear.GetComponent<Animator>();//获取动画控制器
         
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        Animator r = RedBear.GetComponent<Animator>();
        Animator b = BlueBear.GetComponent<Animator>();
        if (GameData.RedOrBlue == 0)
        {
            r.applyRootMotion = true;
            b.applyRootMotion = false;
        }
        else if (GameData.RedOrBlue == 1)
        {
            r.applyRootMotion = false;
            b.applyRootMotion = true;
        }


        AnimatorStateInfo Red_stateinfo = Red_animator.GetCurrentAnimatorStateInfo(0);//获取当前动画信息
        AnimatorStateInfo Blue_stateinfo = Blue_animator.GetCurrentAnimatorStateInfo(0);//获取当前动画信息

        Red_animator.SetFloat("Direction", GameData.getRedDir, dirDemp, Time.deltaTime);//红熊方向
        Red_animator.SetFloat("Speed", GameData.getRedSpeed);
        Blue_animator.SetFloat("Direction", GameData.getBlueDir, dirDemp, Time.deltaTime);//蓝熊方向
        Blue_animator.SetFloat("Speed", GameData.getBlueSpeed);

        // Debug.Log(GameData.getRedDir.ToString());
        // Debug.Log(GameData.getBlueDir.ToString());

        /********单机版******/
        //Red_animator.SetFloat("Direction", GameData.RedDir, dirDemp, Time.deltaTime);//红熊方向
        //Red_animator.SetFloat("Speed", GameData.RedSpeed);
        //Blue_animator.SetFloat("Direction", GameData.BlueDir, dirDemp, Time.deltaTime);//蓝熊方向
        //Blue_animator.SetFloat("Speed", GameData.BlueSpeed);
        /*******单机版*******/
        if (Blue_stateinfo.IsName("Base Layer.Run") && GameData.getisBlueJump == 1)//蓝熊当前是跑步状态
        {
            Blue_animator.SetTrigger("Jump");//跳
            GameData.isBlueJump = 0;
        }
        if (Red_stateinfo.IsName("Base Layer.Run") && GameData.getisRedJump == 1)//蓝熊当前是跑步状态
        {
            Red_animator.SetTrigger("Jump");//跳
            GameData.isRedJump = 0;
        }
        if (GameData.RedOrBlue == 1)//蓝熊
        {
            
            RedBear.transform.localPosition = new Vector3(GameData.getRedX, GameData.getRedY, GameData.getRedZ);//设置两个熊的位置和角度
            RedBear.transform.eulerAngles = new Vector3(GameData.getRedRotX, GameData.getRedRotY, GameData.getRedRotZ);
        }else
        if(GameData.RedOrBlue==0)
        {
            
            BlueBear.transform.localPosition = new Vector3(GameData.getBlueX, GameData.getBlueY, GameData.getBlueZ);
            BlueBear.transform.eulerAngles = new Vector3(GameData.getBlueRotX, GameData.getBlueRotY, GameData.getBlueRotZ);
        }
            
	}
}
