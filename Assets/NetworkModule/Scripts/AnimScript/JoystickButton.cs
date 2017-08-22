using UnityEngine;
using System.Collections;

public class JoystickButton : MonoBehaviour {

     void OnEnable()  
    {  
        EasyJoystick.On_JoystickMove += OnJoystickMove;  
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;  
    }

     void OnDisable()
     {
         EasyJoystick.On_JoystickMove -= OnJoystickMove;
         EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
     }

     void OnDestroy()
     {
         EasyJoystick.On_JoystickMove -= OnJoystickMove;
         EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
     }
    //移动摇杆结束  
    void OnJoystickMoveEnd(MovingJoystick move)  
    {  
        //停止时，角色恢复idle  
        if (move.joystickName == "myjoystick")  
        {   
            if (GameData.RedOrBlue == 0)//红色
            {
                GameData.RedDir = 0;
                GameData.RedSpeed = 0;//-1~+1
            }
            else if (GameData.RedOrBlue == 1)//控制的是蓝色
            {
                GameData.BlueDir = 0;
                GameData.BlueSpeed = 0;//-1~+1
            }
        }  
    }  
    
  
    //移动摇杆中  
    void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "myjoystick")
        {
            return;
        }
        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;
        if (GameData.RedOrBlue == 0)//操控指令  红熊
        {
            GameData.RedDir = joyPositionX;
            GameData.RedSpeed = joyPositionY;//-1~+1
        }
        else if (GameData.RedOrBlue == 1)//操控指令    蓝熊
        {
            GameData.BlueDir = joyPositionX;
            GameData.BlueSpeed = joyPositionY;//-1~+1 
        }
        //Debug.Log(joyPositionX + "  " + joyPositionY);
    }
}
