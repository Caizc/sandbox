using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

/// <summary>
///     Socket 测试类
/// </summary>
public class SocketTest : MonoBehaviour
{
    private static Socket _socket;
    private readonly string host = "192.168.1.186";
    private IPAddress ip;
    private IPEndPoint ipe;
    private readonly int port = 2001;

    private void Start()
    {
        ip = IPAddress.Parse(host);
        ipe = new IPEndPoint(ip, port);

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Connect(ipe); //连接socket

        var sendStr = "<#CONNECT#>"; //发送信息“连接中”

        var bs = Encoding.ASCII.GetBytes(sendStr);
        var bsLen = ByteUtils.Int2ByteArray(bs.Length); //"连接中"的长度
        _socket.Send(bsLen, bsLen.Length, 0); //发长度
        _socket.Send(bs, bs.Length, 0); //发信息

        var br = new byte[15]; //接受信息负责确定红球还是篮球
        _socket.Receive(br, 0, br.Length, SocketFlags.None);
        var readStr = Encoding.ASCII.GetString(br);

        if (readStr.StartsWith("<#REDORBLUE#>"))
        {
            readStr = readStr.Substring(13);
            GameData.RedOrBlue = int.Parse(readStr);
        }
    } 
    
    /*
    void Update() {
        try
        {
            //发信息
            string sendStr = "<#POS#>";
            byte[] bsendStr = Encoding.ASCII.GetBytes(sendStr);
            byte[] bsendStrLen = ByteUtils.int2ByteArray(bsendStr.Length);//长度
            _socket.Send(bsendStrLen, bsendStrLen.Length, 0);//发送长度
            _socket.Send(bsendStr, bsendStr.Length, 0);//传坐标
            
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
            _socket.Send(bSend, bSend.Length, 0);//传坐标

            //收信息长度
            string needmsg = "<#NEED#>";
            byte[] bNeedmsg = Encoding.ASCII.GetBytes(needmsg);
            byte[] bNeedmsgLen = ByteUtils.int2ByteArray(bNeedmsg.Length);
            _socket.Send(bNeedmsgLen, bNeedmsgLen.Length, 0);//发长度
            _socket.Send(bNeedmsg, bNeedmsg.Length, 0);

            byte[] bRead = new byte[16];
            _socket.Receive(bRead);//读取
            byte[] brx = { bRead[0], bRead[1], bRead[2], bRead[3] };
            byte[] bry = { bRead[4], bRead[5], bRead[6], bRead[7] };
            byte[] bbx = { bRead[8], bRead[9], bRead[10], bRead[11] };
            byte[] bby = { bRead[12], bRead[13], bRead[14], bRead[15] };

            if (GameData.RedOrBlue == 0)//客户端控制红色
            {
                GameData.blueX = ByteUtils.byteArray2Float(bbx, 0);
                GameData.blueY = ByteUtils.byteArray2Float(bby, 0);
            }
            else if (GameData.RedOrBlue == 1)//客户端控制蓝色
            {
                GameData.redX = ByteUtils.byteArray2Float(brx, 0);
                GameData.redY = ByteUtils.byteArray2Float(bry, 0);
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString());
        }
    
    }*/
}