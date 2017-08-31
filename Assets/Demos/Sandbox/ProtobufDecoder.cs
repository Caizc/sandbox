using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XLua;

namespace Assets.Demos.Sandbox
{
    /// <summary>
    /// protobuf 协议数据解码器
    /// </summary>
    [CSharpCallLua]
    public class ProtobufDecoder
    {
        private LuaEnv luaEnv;

        [CSharpCallLua]
        public delegate int DecoderDelegate(byte[] bytes, out int c);

        public ProtobufDecoder()
        {
            // 应用中共用一个 LuaEnv，而不是每个类中 new 一个
            this.luaEnv = LuaBehaviour.LuaEnv;
        }

        /// <summary>
        /// 解码 Protobuf 字节数组
        /// </summary>
        /// <param name="bytes">Protobuf 字节数组</param>
        public void Decode(byte[] bytes)
        {
            Debug.Log("Decoding Protocol Buffers Message...");

            // 获取 lua 脚本中 decodeprotobuf 函数的委托
            DecoderDelegate decoder = this.luaEnv.Global.Get<DecoderDelegate>("decodeprotobuf");

            // 使用 lua 脚本中提供的功能函数解码 protobuf 消息内容
            int i_ret;
            int id = decoder(bytes, out i_ret);

            Debug.Log("Protocol Buffers Message has decoded.");
            Debug.Log("Get the person.id = " + id);
        }
    }
}