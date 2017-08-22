using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
public class UIScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Home) || Input.GetKeyUp(KeyCode.Escape))
        {							//对手机“Home”键的监听
            Application.Quit();										//游戏退出
        }
	}

    public void JumpClick()
    {
        if (GameData.RedOrBlue == 0)
        {
            GameData.isRedJump = 1;
        }
        else if (GameData.RedOrBlue == 1)
        {
            GameData.isBlueJump = 1;
        }
        
        Debug.Log("BBB");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("AAA");
    }

}
