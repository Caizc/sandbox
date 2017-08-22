using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class SendText : MonoBehaviour {
    public ConnectSocket mySocket;
    private float rx;
    private float ry;
    private float bx;
    private float by;
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
	/*void Update () {
        if (rx == GameData.redX &&
            ry == GameData.redY &&
            bx == GameData.blueX &&
            by == GameData.blueY)
        {
            Debug.Log("return");
            return;
        }
        
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
        //Debug.Log(
        //                bSend.Length + "              " +
        //                ByteUtil.byteArray2Float(bSend, 0) + " " +
        //                ByteUtil.byteArray2Float(bSend, 4) + "   " +
        //                ByteUtil.byteArray2Float(bSend, 8) + " " +
        //                ByteUtil.byteArray2Float(bSend, 12)
        //            );
        rx = GameData.redX;//拷贝数据
        ry = GameData.redY;
        bx = GameData.blueX;
        by = GameData.blueY;
        mySocket.sendMSG(bSend);
	}*/
}
