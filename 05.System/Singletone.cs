using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance; //�̱��� ����(private)

    public static T Instance //������ �ν��Ͻ��� ��ȯ�ϴ� ������Ƽ
    {                        //public���� ���� �� ���ȿ� ��������⿡ �̸� ĸ��ȭ�� ��!
        get
        {
            if (!instance) { return null; } //�ν��Ͻ��� ����� ��� ��ȯ�� X
            return instance;                //�� ��) instance�� ��ȯ
        }
    }

    void Awake()
    {
        if (!instance) //�ν��Ͻ��� ��� �ִٸ�
        {
            instance = (T)FindObjectOfType(typeof(T)); //�ν��Ͻ��� ������ ��ũ��Ʈ(T)�� ����
            DontDestroyOnLoad(gameObject); //�ش� ��ũ��Ʈ�� ���� ������Ʈ�� �� ��ȯ �ÿ��� �ı����� �ʰ� ��
                                           //��, ������Ʈ�� �ٸ� ������Ʈ�� �ڽ��� ��쿣 ���� X
        }
        else
        {
            Destroy(gameObject); //�̱��� �ߺ� ���� ����
        }
    }
}
