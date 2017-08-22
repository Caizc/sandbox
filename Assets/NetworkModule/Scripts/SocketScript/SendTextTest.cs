using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// 测试类
/// </summary>
public class SendTextTest : MonoBehaviour
{
    private ConnectSocket _mySocket;
    private float _rx;
    private float _ry;
    private float _bx;
    private float _by;

    void Start()
    {
        if (_mySocket == null)
        {
            _mySocket = ConnectSocket.GetSocketInstance();
            byte[] bconnect = Encoding.ASCII.GetBytes("<#CONNECT#>");
            _mySocket.SendMessage(bconnect); //发送连接请求
        }
    }

    /*void Update () {
        if (rx == GameData.redX &&
            ry == GameData.redY &&
            bx == GameData.blueX &&
            by == GameData.blueY)
        {
            Debug.Log("return");
            return;
        }
        
        byte[] bsrx = ByteUtils.float2ByteArray(GameData.redX);
        byte[] bsry = ByteUtils.float2ByteArray(GameData.redY);
        byte[] bsbx = ByteUtils.float2ByteArray(GameData.blueX);
        byte[] bsby = ByteUtils.float2ByteArray(GameData.blueY);

        byte[] bSend = {
                            bsrx[0],bsrx[1],bsrx[2],bsrx[3],
                            bsry[0],bsry[1],bsry[2],bsry[3],
                            bsbx[0],bsbx[1],bsbx[2],bsbx[3],
                            bsby[0],bsby[1],bsby[2],bsby[3]
                           };
        //Debug.Log(
        //                bSend.Length + "              " +
        //                ByteUtils.byteArray2Float(bSend, 0) + " " +
        //                ByteUtils.byteArray2Float(bSend, 4) + "   " +
        //                ByteUtils.byteArray2Float(bSend, 8) + " " +
        //                ByteUtils.byteArray2Float(bSend, 12)
        //            );
        rx = GameData.redX;//拷贝数据
        ry = GameData.redY;
        bx = GameData.blueX;
        by = GameData.blueY;
        mySocket.SendMessage(bSend);
    }*/
}