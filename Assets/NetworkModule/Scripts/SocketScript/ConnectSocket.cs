using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ConnectSocket
{
    private static ConnectSocket _instance;
    private readonly Socket _socket;

    /// <summary>
    /// 构造方法
    /// </summary>
    private ConnectSocket()
    {
        // 实例化 Socket 对象
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // TODO

        // 办公室 MacBook IP 地址
        var ip = IPAddress.Parse("192.168.1.100");
        var port = 2001;

        // 办公室服务器 IP 地址
//         IPAddress ip = IPAddress.Parse("192.168.3.108");
//         var port = 12345;

        var ipe = new IPEndPoint(ip, port);

        var result = _socket.BeginConnect(ipe, ConnectCallBack, _socket);

        // 超时检测
        var connectsucces = result.AsyncWaitHandle.WaitOne(5000, true);

        if (connectsucces) //连接成功
        {
            var thread = new Thread(GetMessage); //从服务器接收消息
            thread.IsBackground = true;
            thread.Start();
        }
        else //连接失败
        {
            Debug.Log("Time Out");
        }
    }

    /// <summary>
    /// 获取 Socket 实例
    /// </summary>
    /// <returns></returns>
    public static ConnectSocket GetSocketInstance()
    {
        if (_instance == null)
        {
            _instance = new ConnectSocket();
        }

        return _instance;
    }

    /// <summary>
    /// 成功建立连接回调方法
    /// </summary>
    /// <param name="asyncResult"></param>
    private void ConnectCallBack(IAsyncResult asyncResult)
    {
        Debug.Log("Connect to Server Success");
    }

    /// <summary>
    /// 接收服务端发来的消息
    /// </summary>
    private void GetMessage()
    {
        while (true)
        {
            if (!_socket.Connected) // 断开连接
            {
                Debug.Log("break connect");
                _socket.Close();
                break;
            }

            try
            {
                var bytesLen = new byte[4];
                _socket.Receive(bytesLen); // 接收消息长度
                var length = ByteUtils.ByteArray2Int(bytesLen, 0);

                var bytes = new byte[length]; // 声明接收的消息字节数组
                var count = 0;

                // 接收消息内容
                while (count < length)
                {
                    var tempLength = _socket.Receive(bytes);
                    count += tempLength;
                }

                SplitBytes(bytes); // 拆字符串
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                break;
            }
        }
    }

    /// <summary>
    /// 向服务端发送消息
    /// </summary>
    /// <param name="bytes"></param>
    public void SendMessage(byte[] bytes)
    {
        if (!_socket.Connected) // 断开连接
        {
            Debug.Log("break connect");
            _socket.Close();
        }

        try
        {
            var length = bytes.Length;
            var blength = ByteUtils.Int2ByteArray(length);

            _socket.Send(blength, SocketFlags.None); // 发送消息长度
            _socket.Send(bytes, SocketFlags.None); // 发送消息内容
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    /// <summary>
    /// 拆分消息包
    /// </summary>
    /// <param name="bytes">消息字节数组</param>
    private void SplitBytes(byte[] bytes)
    {
        // 获取消息包大小
        var length = bytes.Length;

        if (length == 4)
        {
            // 消息包为 4 个字节表示是分配玩家单位指令
            GameData.RedOrBlue = ByteUtils.ByteArray2Int(bytes, 0);
        }
        else if (length == 72)
        {
            // 消息包为 72 个字节表示是操控和状态数据指令

            byte[] brd = {bytes[0], bytes[1], bytes[2], bytes[3]};
            byte[] brs = {bytes[4], bytes[5], bytes[6], bytes[7]};
            byte[] bbd = {bytes[8], bytes[9], bytes[10], bytes[11]};
            byte[] bbs = {bytes[12], bytes[13], bytes[14], bytes[15]};
            byte[] rx = {bytes[16], bytes[17], bytes[18], bytes[19]};
            byte[] ry = {bytes[20], bytes[21], bytes[22], bytes[23]};
            byte[] rz = {bytes[24], bytes[25], bytes[26], bytes[27]};
            byte[] rrx = {bytes[28], bytes[29], bytes[30], bytes[31]};
            byte[] rry = {bytes[32], bytes[33], bytes[34], bytes[35]};
            byte[] rrz = {bytes[36], bytes[37], bytes[38], bytes[39]};
            byte[] bx = {bytes[40], bytes[41], bytes[42], bytes[43]};
            byte[] by = {bytes[44], bytes[45], bytes[46], bytes[47]};
            byte[] bz = {bytes[48], bytes[49], bytes[50], bytes[51]};
            byte[] brx = {bytes[52], bytes[53], bytes[54], bytes[55]};
            byte[] bry = {bytes[56], bytes[57], bytes[58], bytes[59]};
            byte[] brz = {bytes[60], bytes[61], bytes[62], bytes[63]};
            byte[] rjump = {bytes[64], bytes[65], bytes[66], bytes[67]};
            byte[] bjump = {bytes[68], bytes[69], bytes[70], bytes[71]};

            GameData.GetRedDir = ByteUtils.ByteArray2Float(brd, 0);
            GameData.GetRedSpeed = ByteUtils.ByteArray2Float(brs, 0);
            GameData.GetBlueDir = ByteUtils.ByteArray2Float(bbd, 0);
            GameData.GetBlueSpeed = ByteUtils.ByteArray2Float(bbs, 0);
            GameData.GetRedX = ByteUtils.ByteArray2Float(rx, 0);
            GameData.GetRedY = ByteUtils.ByteArray2Float(ry, 0);
            GameData.GetRedZ = ByteUtils.ByteArray2Float(rz, 0);
            GameData.GetRedRotX = ByteUtils.ByteArray2Float(rrx, 0);
            GameData.GetRedRotY = ByteUtils.ByteArray2Float(rry, 0);
            GameData.GetRedRotZ = ByteUtils.ByteArray2Float(rrz, 0);
            GameData.GetBlueX = ByteUtils.ByteArray2Float(bx, 0);
            GameData.GetBlueY = ByteUtils.ByteArray2Float(by, 0);
            GameData.GetBlueZ = ByteUtils.ByteArray2Float(bz, 0);
            GameData.GetBlueRotX = ByteUtils.ByteArray2Float(brx, 0);
            GameData.GetBlueRotY = ByteUtils.ByteArray2Float(bry, 0);
            GameData.GetBlueRotZ = ByteUtils.ByteArray2Float(brz, 0);
            GameData.GetisRedJump = ByteUtils.ByteArray2Int(rjump, 0);
            GameData.GetisBlueJump = ByteUtils.ByteArray2Int(bjump, 0);
        }
    }
}