using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameBtn : MonoBehaviour
{
    //빌드했을때 게임 종료시킬 수 있는 OnClick함수
    public void GameQuitBtn()
    {
            Application.Quit();
    }
    //유니티 안에서 게임을 종료시키는것을 테스트 해 볼수있는 OnClick함수
    public void UnityExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
