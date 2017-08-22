using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class MySocket : MonoBehaviour
{
    int port = 2001;
    string host = "192.168.1.186";
    IPAddress ip ;
    IPEndPoint ipe ;
    public static Socket s = null;
    void Start()
    {
        ip = IPAddress.Parse(host);
        ipe = new IPEndPoint(ip, port);
        s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(ipe);//连接socket

        string sendStr = "<#CONNECT#>";//发送信息“连接中”
        
        byte[] bs = Encoding.ASCII.GetBytes(sendStr);
        byte[] bsLen = ByteUtil.int2ByteArray(bs.Length);//"连接中"的长度
        s.Send(bsLen, bsLen.Length, 0);//发长度
        s.Send(bs, bs.Length, 0);//发信息

        byte[] br = new byte[15];//接受信息负责确定红球还是篮球
        s.Receive(br, 0, br.Length, SocketFlags.None);
        string readStr = Encoding.ASCII.GetString(br);

        if (readStr.StartsWith("<#REDORBLUE#>"))
        {
            readStr=readStr.Substring(13);
            GameData.RedOrBlue = int.Parse(readStr);
        }

    }/*
    void Update() {
        try
        {
            //发信息
            string sendStr = "<#POS#>";
            byte[] bsendStr = Encoding.ASCII.GetBytes(sendStr);
            byte[] bsendStrLen = ByteUtil.int2ByteArray(bsendStr.Length);//长度
            s.Send(bsendStrLen, bsendStrLen.Length, 0);//发送长度
            s.Send(bsendStr, bsendStr.Length, 0);//传坐标
            
            byte[] bsrx = ByteUtil.float2ByteArray(GameData.redX);
            byte[] bsry = ByteUtil.float2ByteArray(GameData.redY);
            byte[] bsbx = ByteUtil.float2ByteArray(GameData.blueX);
            byte[] bsby = ByteUtil.float2ByteArray(GameData.blueY);
            byte[] bSend = {
                            bsrx[0],bsrx[1],bsrx[2],bsrx[3],
                            bsry[0],bsry[1],bsry[2],bsry[3],
                            bsbx[0],bsbx[1],bsbx[2],bsbx[3],
                            bsby[0],bsby[1],bsby[2],bsby[3]
                           };
            s.Send(bSend, bSend.Length, 0);//传坐标

            //收信息长度
            string needmsg = "<#NEED#>";
            byte[] bNeedmsg = Encoding.ASCII.GetBytes(needmsg);
            byte[] bNeedmsgLen = ByteUtil.int2ByteArray(bNeedmsg.Length);
            s.Send(bNeedmsgLen, bNeedmsgLen.Length, 0);//发长度
            s.Send(bNeedmsg, bNeedmsg.Length, 0);

            byte[] bRead = new byte[16];
            s.Receive(bRead);//读取
            byte[] brx = { bRead[0], bRead[1], bRead[2], bRead[3] };
            byte[] bry = { bRead[4], bRead[5], bRead[6], bRead[7] };
            byte[] bbx = { bRead[8], bRead[9], bRead[10], bRead[11] };
            byte[] bby = { bRead[12], bRead[13], bRead[14], bRead[15] };

            if (GameData.RedOrBlue == 0)//客户端控制红色
            {
                GameData.blueX = ByteUtil.byteArray2Float(bbx, 0);
                GameData.blueY = ByteUtil.byteArray2Float(bby, 0);
            }
            else if (GameData.RedOrBlue == 1)//客户端控制蓝色
            {
                GameData.redX = ByteUtil.byteArray2Float(brx, 0);
                GameData.redY = ByteUtil.byteArray2Float(bry, 0);
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.ToString());
        }
    
    }*/
}
