using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singletone<SoundManager>
{
    public AudioSource audioSource; //����� �ҽ�
    public AudioClip[] audioClips; //����� Ŭ��

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //������ҽ� ������Ʈ �ҷ�����

        audioSource.clip = audioClips[0]; //������� Ŭ�� ����
        audioSource.loop = true;          //�ݺ� ���

        audioSource.Play();               //����
    }

    /*
        void Update()
        {
            switch (SceneLoadManager.Instance.CurrentScene)
            {
                case SceneLoadManager.SceneStart.TITLE:
                    ChangeBGM(0);
                    break;

                case SceneLoadManager.SceneStart.LOADING:
                    ChangeBGM(0);
                    break;

                case SceneLoadManager.SceneStart.FIRSTTOWN:
                    ChangeBGM(1);
                    break;

                case SceneLoadManager.SceneStart.FOREST:
                    ChangeBGM(2);
                    break;
            }
        }

        public void ChangeBGM(int index)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    */

}