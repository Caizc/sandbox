using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary; 
public class ConnectSocket
{
    private Socket mySocket;
    private static ConnectSocket instance;
    public static ConnectSocket getSocketInstance()//获取实例化对象
    {
        if (instance == null)
        {
            instance = new ConnectSocket();
        }
        return instance;
    }

    ConnectSocket()//构造器
    {
        //实例化Socket对象
        mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ip = IPAddress.Parse("192.168.3.108");//服务器ip地址
        IPEndPoint ipe = new IPEndPoint(ip,12345);//服务器端口
        IAsyncResult result = mySocket.BeginConnect(ipe,new AsyncCallback(connectCallBack),mySocket);

        bool connectsucces = result.AsyncWaitHandle.WaitOne(5000, true);//超时检测

        if (connectsucces)//连接成功
        {
            Thread thread = new Thread(new ThreadStart(getMSG));//从服务器接受消息
            thread.IsBackground = true;
            thread.Start();
        }else//没有连接成功
        {
            Debug.Log("Time Out");       
        }
    }

    private void connectCallBack(IAsyncResult ast)//成功建立连接回调方法
    {
        Debug.Log("Connect Success");
    }
    private void getMSG()
    {
        while (true)
        {
            
            if (!mySocket.Connected)//断开连接
            {
                Debug.Log("break connect");
                mySocket.Close();
                break;
            }
            try
            {
                byte[] bytesLen=new byte[4];
                mySocket.Receive(bytesLen);//接受长度
                int length = ByteUtil.byteArray2Int(bytesLen,0);
                

                byte[] bytes = new byte[length];//声明接受数组
                int count = 0;
                while (count < length)
                {
                    int tempLength = mySocket.Receive(bytes);
                    count += tempLength;
                }
                
                splitBytes(bytes);//拆字符串
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                break;
            }
        }
    }
    public void sendMSG(byte[] bytes)
    { 
        if (!mySocket.Connected)//断开连接
        {
            Debug.Log("break connect");
            mySocket.Close();
        }
        try
        {
            int length = bytes.Length;
            byte[] blength = ByteUtil.int2ByteArray(length);
            mySocket.Send(blength,SocketFlags.None);//发长度
            mySocket.Send(bytes,SocketFlags.None);//发数据

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    private void splitBytes(byte[] bytes)//接受数据后拆包
    {

        int length = bytes.Length;//获取包长
        if (length == 4)//是4位格式代表RedOrBlue
        {
            GameData.RedOrBlue = ByteUtil.byteArray2Int(bytes,0);
        }else
        if (length == 72)//是数据格式
        //if (length ==40)//是数据格式
        {
            byte[] brd = { bytes[0], bytes[1], bytes[2], bytes[3] };
            byte[] brs = { bytes[4], bytes[5], bytes[6], bytes[7] };
            byte[] bbd = { bytes[8], bytes[9], bytes[10], bytes[11] };
            byte[] bbs = { bytes[12], bytes[13], bytes[14], bytes[15] };
            byte[] rx = { bytes[16], bytes[17], bytes[18], bytes[19] };
            byte[] ry = { bytes[20], bytes[21], bytes[22], bytes[23] };
            byte[] rz = { bytes[24], bytes[25], bytes[26], bytes[27] };
            byte[] rrx = { bytes[28], bytes[29], bytes[30], bytes[31] };
            byte[] rry = { bytes[32], bytes[33], bytes[34], bytes[35] };
            byte[] rrz = { bytes[36], bytes[37], bytes[38], bytes[39] };
            byte[] bx = { bytes[40], bytes[41], bytes[42], bytes[43] };
            byte[] by = { bytes[44], bytes[45], bytes[46], bytes[47] };
            byte[] bz = { bytes[48], bytes[49], bytes[50], bytes[51] };
            byte[] brx = { bytes[52], bytes[53], bytes[54], bytes[55] };
            byte[] bry = { bytes[56], bytes[57], bytes[58], bytes[59] };
            byte[] brz = { bytes[60], bytes[61], bytes[62], bytes[63] };
            byte[] rjump = { bytes[64], bytes[65], bytes[66], bytes[67] };
            byte[] bjump = { bytes[68], bytes[69], bytes[70], bytes[71] };
            GameData.getRedDir = ByteUtil.byteArray2Float(brd, 0);
            GameData.getRedSpeed = ByteUtil.byteArray2Float(brs, 0);
            GameData.getBlueDir = ByteUtil.byteArray2Float(bbd,0);
            GameData.getBlueSpeed = ByteUtil.byteArray2Float(bbs,0);
            GameData.getRedX = ByteUtil.byteArray2Float(rx, 0);
            GameData.getRedY = ByteUtil.byteArray2Float(ry, 0);
            GameData.getRedZ = ByteUtil.byteArray2Float(rz, 0);
            GameData.getRedRotX = ByteUtil.byteArray2Float(rrx, 0);
            GameData.getRedRotY = ByteUtil.byteArray2Float(rry, 0);
            GameData.getRedRotZ = ByteUtil.byteArray2Float(rrz, 0);
            GameData.getBlueX = ByteUtil.byteArray2Float(bx, 0);
            GameData.getBlueY = ByteUtil.byteArray2Float(by, 0);
            GameData.getBlueZ = ByteUtil.byteArray2Float(bz, 0);
            GameData.getBlueRotX = ByteUtil.byteArray2Float(brx, 0);
            GameData.getBlueRotY = ByteUtil.byteArray2Float(bry, 0);
            GameData.getBlueRotZ = ByteUtil.byteArray2Float(brz, 0);
            GameData.getisRedJump = ByteUtil.byteArray2Int(rjump, 0);
            GameData.getisBlueJump = ByteUtil.byteArray2Int(bjump, 0);
            //Debug.Log("~~~~get" + GameData.getRedX + "  " + GameData.getRedY + " " + GameData.getBlueX + " " + GameData.getBlueX);
        }
    }
}
