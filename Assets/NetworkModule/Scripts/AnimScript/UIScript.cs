using UnityEngine;

/// <summary>
/// UI 控制脚本
/// </summary>
public class UIScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Home) || Input.GetKeyUp(KeyCode.Escape))
        {
            // 退出游戏
            Application.Quit(); 
        }
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    public void Jump()
    {
        if (GameData.RedOrBlue == 0)
        {
            GameData.IsRedJump = 1;
        }
        else if (GameData.RedOrBlue == 1)
        {
            GameData.IsBlueJump = 1;
        }

        Debug.Log("Jump!");
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Quit!");
    }
}