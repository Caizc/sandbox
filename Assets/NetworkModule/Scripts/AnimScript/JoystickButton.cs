using UnityEngine;

/// <summary>
/// 摇杆
/// </summary>
public class JoystickButton : MonoBehaviour
{
    private void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    private void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

    /// <summary>
    ///     移动摇杆结束
    /// </summary>
    /// <param name="move"></param>
    private void OnJoystickMoveEnd(MovingJoystick move)
    {
        // 停止时，角色恢复 idle
        if (move.joystickName == "myjoystick")
        {
            if (GameData.RedOrBlue == 0) // 控制的是红色
            {
                GameData.RedDir = 0;
                GameData.RedSpeed = 0; //-1~+1
            }
            else if (GameData.RedOrBlue == 1) // 控制的是蓝色
            {
                GameData.BlueDir = 0;
                GameData.BlueSpeed = 0; //-1~+1
            }
        }
    }

    /// <summary>
    ///     移动摇杆中
    /// </summary>
    /// <param name="move"></param>
    private void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "myjoystick")
        {
            return;
        }

        var joyPositionX = move.joystickAxis.x;
        var joyPositionY = move.joystickAxis.y;

        if (GameData.RedOrBlue == 0) // 操控红熊
        {
            GameData.RedDir = joyPositionX;
            GameData.RedSpeed = joyPositionY; //-1~+1
        }
        else if (GameData.RedOrBlue == 1) // 操控蓝熊
        {
            GameData.BlueDir = joyPositionX;
            GameData.BlueSpeed = joyPositionY; //-1~+1 
        }

        //Debug.Log(joyPositionX + "  " + joyPositionY);
    }

    private void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }
}