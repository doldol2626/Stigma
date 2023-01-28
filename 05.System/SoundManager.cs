using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singletone<SoundManager>
{
    public AudioSource audioSource; //오디오 소스
    public AudioClip[] audioClips; //오디오 클립

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //오디오소스 컴포넌트 불러오기

        audioSource.clip = audioClips[0]; //배경음악 클립 지정
        audioSource.loop = true;          //반복 재생

        audioSource.Play();               //시작
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