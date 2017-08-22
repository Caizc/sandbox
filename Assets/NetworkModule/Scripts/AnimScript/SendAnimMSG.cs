using UnityEngine;
using System.Text;

/// <summary>
/// 发送操控消息
/// </summary>
public class SendAnimMSG : MonoBehaviour
{
    public GameObject RedBear;
    public GameObject BlueBear;

    private float _redDir;
    private float _redSpeed;
    private float _blueDir;
    private float _blueSpeed;
    private Vector3 _redPos;
    private Vector3 _bluePos;

    private ConnectSocket _socket;

    //private float temptime=0;

    void Start()
    {
        if (_socket == null)
        {
            _socket = ConnectSocket.GetSocketInstance();
            byte[] bconnect = Encoding.ASCII.GetBytes("<#CONNECT#>");
            // 发送连接请求
            _socket.SendMessage(bconnect);
        }
    }

    void FixedUpdate()
    {
        // 当前发送信息与上次发送信息一样就跳过，不作处理
        if (_redDir == GameData.RedDir &&
            _redSpeed == GameData.RedSpeed &&
            _blueDir == GameData.BlueDir &&
            _blueSpeed == GameData.BlueSpeed &&
            _redPos == RedBear.transform.localPosition &&
            _bluePos == BlueBear.transform.localPosition
        )
        {
            Debug.Log("return");
            return;
        }

        if (GameData.RedOrBlue == 0) // 操控红熊
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
        else if (GameData.RedOrBlue == 1) // 操控蓝熊
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

        byte[] rd = ByteUtils.Float2ByteArray(GameData.RedDir);
        byte[] rs = ByteUtils.Float2ByteArray(GameData.RedSpeed);
        byte[] bd = ByteUtils.Float2ByteArray(GameData.BlueDir);
        byte[] bs = ByteUtils.Float2ByteArray(GameData.BlueSpeed);

        byte[] rx = ByteUtils.Float2ByteArray(GameData.RedX);
        byte[] ry = ByteUtils.Float2ByteArray(GameData.RedY);
        byte[] rz = ByteUtils.Float2ByteArray(GameData.RedZ);
        byte[] rrx = ByteUtils.Float2ByteArray(GameData.RedRotX);
        byte[] rry = ByteUtils.Float2ByteArray(GameData.RedRotY);
        byte[] rrz = ByteUtils.Float2ByteArray(GameData.RedRotZ);

        byte[] bx = ByteUtils.Float2ByteArray(GameData.BlueX);
        byte[] by = ByteUtils.Float2ByteArray(GameData.BlueY);
        byte[] bz = ByteUtils.Float2ByteArray(GameData.BlueZ);
        byte[] brx = ByteUtils.Float2ByteArray(GameData.BlueRotX);
        byte[] bry = ByteUtils.Float2ByteArray(GameData.BlueRotY);
        byte[] brz = ByteUtils.Float2ByteArray(GameData.BlueRotZ);
        byte[] rjump = ByteUtils.Int2ByteArray(GameData.IsRedJump);
        byte[] bjump = ByteUtils.Int2ByteArray(GameData.IsBlueJump);

        byte[] sendMsg =
        {
            rd[0], rd[1], rd[2], rd[3],
            rs[0], rs[1], rs[2], rs[3],
            bd[0], bd[1], bd[2], bd[3],
            bs[0], bs[1], bs[2], bs[3],
            rx[0], rx[1], rx[2], rx[3],
            ry[0], ry[1], ry[2], ry[3],
            rz[0], rz[1], rz[2], rz[3],
            rrx[0], rrx[1], rrx[2], rrx[3],
            rry[0], rry[1], rry[2], rry[3],
            rrz[0], rrz[1], rrz[2], rrz[3],
            bx[0], bx[1], bx[2], bx[3],
            by[0], by[1], by[2], by[3],
            bz[0], bz[1], bz[2], bz[3],
            brx[0], brx[1], brx[2], brx[3],
            bry[0], bry[1], bry[2], bry[3],
            brz[0], brz[1], brz[2], brz[3],
            rjump[0], rjump[1], rjump[2], rjump[3],
            bjump[0], bjump[1], bjump[2], bjump[3]
        };

        // 暂存状态信息，用于下次比较
        _redDir = GameData.RedDir;
        _redSpeed = GameData.RedSpeed;
        _blueDir = GameData.BlueDir;
        _blueSpeed = GameData.BlueSpeed;
        _redPos = RedBear.transform.localPosition;
        _bluePos = BlueBear.transform.localPosition;

        // 发送消息
        _socket.SendMessage(sendMsg);
    }
}