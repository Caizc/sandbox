using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class SendAnimMSG : MonoBehaviour {
    private float redDir;
    private float redSpeed;
    private float blueDir;
    private float blueSpeed;
    private Vector3 redPOS;
    private Vector3 bluePOS;
    private ConnectSocket mySocket;
    //private float temptime=0;
    public GameObject RedBear;
    public GameObject BlueBear;
	// Use this for initialization
	void Start () {
        if (mySocket == null)
        {
            mySocket = ConnectSocket.getSocketInstance();
            byte[] bconnect = Encoding.ASCII.GetBytes("<#CONNECT#>");
            mySocket.sendMSG(bconnect);//发送连接请求
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (redDir == GameData.RedDir &&           //当前信息与上次发送信息一样就跳过
            redSpeed == GameData.RedSpeed &&
            blueDir == GameData.BlueDir &&
            blueSpeed == GameData.BlueSpeed &&
            redPOS == RedBear.transform.localPosition &&
            bluePOS == BlueBear.transform.localPosition
            )
        {
            Debug.Log("return");
            return;
        }
        if (GameData.RedOrBlue == 0)//操控指令  红熊
        {
            Vector3 redPostion = RedBear.transform.localPosition;
            Vector3 redRotation = RedBear.transform.localRotation.eulerAngles;
            GameData.RedX = redPostion.x;
            GameData.RedY = redPostion.y;
            GameData.RedZ = redPostion.z;
            GameData.RedRotX = redRotation.x;
            GameData.RedRotY = redRotation.y;
            GameData.RedRotZ = redRotation.z;
        }
        else if (GameData.RedOrBlue == 1)//操控指令    蓝熊
        {
            Vector3 bluePostion = BlueBear.transform.localPosition;
            Vector3 blueRotation = BlueBear.transform.localRotation.eulerAngles;
            GameData.BlueX = bluePostion.x;
            GameData.BlueY = bluePostion.y;
            GameData.BlueZ = bluePostion.z;
            GameData.BlueRotX = blueRotation.x;
            GameData.BlueRotY = blueRotation.y;
            GameData.BlueRotZ = blueRotation.z;
        }
        //Debug.Log("~~~~send"+GameData.RedX + " " + GameData.RedY + " " + GameData.BlueX + " " + GameData.BlueY);
        byte[] rd = ByteUtil.float2ByteArray(GameData.RedDir);
        byte[] rs = ByteUtil.float2ByteArray(GameData.RedSpeed);
        byte[] bd = ByteUtil.float2ByteArray(GameData.BlueDir);
        byte[] bs = ByteUtil.float2ByteArray(GameData.BlueSpeed);
        byte[] rx = ByteUtil.float2ByteArray(GameData.RedX);//红位置
        byte[] ry = ByteUtil.float2ByteArray(GameData.RedY);
        byte[] rz = ByteUtil.float2ByteArray(GameData.RedZ);
        byte[] rrx = ByteUtil.float2ByteArray(GameData.RedRotX);
        byte[] rry = ByteUtil.float2ByteArray(GameData.RedRotY);
        byte[] rrz = ByteUtil.float2ByteArray(GameData.RedRotZ);
        byte[] bx = ByteUtil.float2ByteArray(GameData.BlueX);
        byte[] by = ByteUtil.float2ByteArray(GameData.BlueY);
        byte[] bz = ByteUtil.float2ByteArray(GameData.BlueZ);
        byte[] brx = ByteUtil.float2ByteArray(GameData.BlueRotX);
        byte[] bry = ByteUtil.float2ByteArray(GameData.BlueRotY);
        byte[] brz = ByteUtil.float2ByteArray(GameData.BlueRotZ);
        byte[] rjump = ByteUtil.int2ByteArray(GameData.isRedJump);
        byte[] bjump = ByteUtil.int2ByteArray(GameData.isBlueJump);

        byte[] sendMSG ={ 
                        rd[0],rd[1],rd[2],rd[3],
                        rs[0],rs[1],rs[2],rs[3],
                        bd[0],bd[1],bd[2],bd[3],
                        bs[0],bs[1],bs[2],bs[3],
                        rx[0],rx[1],rx[2],rx[3],
                        ry[0],ry[1],ry[2],ry[3],
                        rz[0],rz[1],rz[2],rz[3],
                        rrx[0],rrx[1],rrx[2],rrx[3],
                        rry[0],rry[1],rry[2],rry[3],
                        rrz[0],rrz[1],rrz[2],rrz[3],
                        bx[0],bx[1],bx[2],bx[3],
                        by[0],by[1],by[2],by[3],
                        bz[0],bz[1],bz[2],bz[3],
                        brx[0],brx[1],brx[2],brx[3],
                        bry[0],bry[1],bry[2],bry[3],
                        brz[0],brz[1],brz[2],brz[3],
                        rjump[0],rjump[1],rjump[2],rjump[3],
                        bjump[0],bjump[1],bjump[2],bjump[3]
                         };

        redDir = GameData.RedDir;         //拷贝信息
        redSpeed = GameData.RedSpeed;
        blueDir = GameData.BlueDir;
        blueSpeed = GameData.BlueSpeed;
        redPOS = RedBear.transform.localPosition;
        bluePOS = BlueBear.transform.localPosition;
        mySocket.sendMSG(sendMSG);
	}
}
