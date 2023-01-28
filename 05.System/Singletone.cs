using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance; //싱글톤 생성(private)

    public static T Instance //생성된 인스턴스를 반환하는 프로퍼티
    {                        //public으로 선언 시 보안에 취약해지기에 이를 캡슐화한 것!
        get
        {
            if (!instance) { return null; } //인스턴스가 비었을 경우 반환값 X
            return instance;                //그 외) instance값 반환
        }
    }

    void Awake()
    {
        if (!instance) //인스턴스가 비어 있다면
        {
            instance = (T)FindObjectOfType(typeof(T)); //인스턴스를 지정된 스크립트(T)로 지정
            DontDestroyOnLoad(gameObject); //해당 스크립트가 붙은 오브젝트가 씬 전환 시에도 파괴되지 않게 함
                                           //단, 오브젝트가 다른 오브젝트의 자식일 경우엔 적용 X
        }
        else
        {
            Destroy(gameObject); //싱글톤 중복 생성 방지
        }
    }
}
