using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XLua;

namespace Demos.Sandbox
{
    /// <summary>
    /// 与服务端联调测试
    /// </summary>
    public class ConnectServer
    {
        private static ConnectSocket _socket;

        public static bool SendMessage(params int[] args)
        {
            if (_socket == null)
            {
                Debug.Log("=== Conneting Server...");

                _socket = ConnectSocket.GetSocketInstance();

                Debug.Log("=== Preparing Data...");

                // byte[] errorCodeBytes = ByteUtils.Int2ByteArray(args[0]);

                byte[] dataBytes =
                {
                    ByteUtils.Int2ByteArray(args[0])[0],
                    ByteUtils.Int2ByteArray(args[1])[0],
                    ByteUtils.Int2ByteArray(args[2])[0]
                };

                Debug.Log("=== Sending Message...");

                // 发送连接请求
                _socket.SendRawMessage(dataBytes);

                Debug.Log("=== Message has sended.");

                return true;
            }

            return false;
        }

        /// <summary>
        /// 向服务端发送 Protobuf 协议消息
        /// </summary>
        /// <param name="encodeBytes">编码后的字节数组</param>
        /// <returns></returns>
        [LuaCallCSharp]
        public static bool SendProtobufMessage(byte[] encodeBytes)
        {
            if (_socket == null)
            {
                _socket = ConnectSocket.GetSocketInstance();
            }

            Debug.Log("=== Sending Protobuf Message...");

            // 发送编码的协议消息前，还发送了 4 个字节的消息包长度
            _socket.SendMessage(encodeBytes);

            // 只发送编码的协议消息
            // _socket.SendRawMessage(encodeBytes);

            Debug.Log("=== Message has sended.");

            return true;
        }
    }
}