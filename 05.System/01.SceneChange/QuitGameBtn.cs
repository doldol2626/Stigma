using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameBtn : MonoBehaviour
{
    //���������� ���� �����ų �� �ִ� OnClick�Լ�
    public void GameQuitBtn()
    {
            Application.Quit();
    }
    //����Ƽ �ȿ��� ������ �����Ű�°��� �׽�Ʈ �� �����ִ� OnClick�Լ�
    public void UnityExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
