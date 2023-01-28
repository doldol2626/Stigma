using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singletone<SceneLoadManager>
{
    public enum SceneStart
    {
        TITLE,
        LOADING,
        START,
        FIRSTTOWN,
        FOREST,
    };

    [SerializeField] SceneStart currentScene; //���� ��
    [SerializeField] SceneStart nextScene;    //���� ��

    bool sceneTransOK; //���� ��ȯ�ص� �Ǵ� ��Ȳ���� �Ǵ�

    AsyncOperation operation; //���� �ҷ����� �ִ� ��
    Slider loadingSlider;     //�ε� �����̴�

    public bool isLoading;

    public NoticeText noticeText;

    public SceneStart CurrentScene => currentScene;
    //currentScene�� ��ȯ�ϴ� ������Ƽ(ĸ��ȭ)

    public void SceneTransOK(bool isOK) => sceneTransOK = isOK;
    //sceneTransOK�� �Է°����� �����ϴ� �Լ�(ĸ��ȭ)

    void Start()
    {
        SceneTransOK(true);               //�� ��ȯ ���� ����
        nextScene = SceneStart.FIRSTTOWN; //���� ���� ������ ����
    }

    void Update()
    {
        currentScene = (SceneStart)SceneManager.GetActiveScene().buildIndex; //���� �� ���� �ݿ�
        ChooseScene(); //�� ���� �Լ� ����

        if (currentScene == SceneStart.LOADING) { isLoading = true; }
        else { isLoading = false; }
    }

    IEnumerator SceneLoad()
    {
        yield return null;

        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        //�ε� ������ �����̴� �ҷ�����

        operation = SceneManager.LoadSceneAsync((int)nextScene);
        //�񵿱� ������� ���� �ҷ����� ���߿��� �ٸ� �۾��� �� ���ִ� LoadSceneAsync �Լ�
        //�ݴ�� �׳� LoadScene�� ���� �ҷ����� ���߿� �ٸ� �۾��� �� ���� ����.
        //��ü�� LoadSceneAsync�� �������� �ڷ�ƾ�̶� ���� ���°� ������.

        operation.allowSceneActivation = false; //�ε��� ������ ���� �ٷ� ���� ���ϰ� �Ѵ�

        //isDone�� false�� �� ����(while������ ��� �ݺ�), �� �ε��� ������ isDone�� true�� �Ǳ� ������ ��� �ݺ�
        while (!operation.isDone)
        {
            yield return null;

            if (operation.progress < 0.9f) //�ε���
            {
                loadingSlider.value = operation.progress; //�����̴��� ���� �ҷ��� ���� �ݿ�
            }
            else //�ε� �Ϸ�
            {
                loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, Time.deltaTime * 0.3f);
                //Time.deltaTime�� *0.3f �߰�. �� �߰� �κ��� �������� �ϳ��� ���� ��
                //�ν�����â���� �����ϰų� ��ġ�� �����ؼ� �׽�Ʈ�غ��鼭 ���� �ٲ㰡�� �� ���ϴ�.

                if (!SceneManager.GetSceneByBuildIndex(2).isLoaded)
                {
                    SceneManager.LoadScene(2, LoadSceneMode.Additive); //StartScene �߰�
                }

                operation.allowSceneActivation = true; //�ε��� �������Ƿ� ���� ���۽�Ų��.
            }
        }
    }

    void ChooseScene()
    {
        while (sceneTransOK) //�� ��ȯ ���� ������ ���
        {
            switch (currentScene) //���� ���� ���� ��� ����
            {
                case SceneStart.TITLE:
                    if (Input.anyKeyDown) { SceneManager.LoadScene(1); }
                    //�ƹ� Ű�� �ԷµǸ� �ε� ������ ��ȯ
                    break;

                case SceneStart.LOADING:
                    StartCoroutine(SceneLoad()); //�� ��ȯ �ڷ�ƾ ����
                    SceneTransOK(false); //�� ��ȯ�� �Ϸ�Ǹ� �� ��ȯ �Ұ� ���·� ����

                    break;

                case SceneStart.FIRSTTOWN:
                    nextScene = SceneStart.FOREST; //���� ���� ������ ����
                    break;

                case SceneStart.FOREST:
                    nextScene = SceneStart.FIRSTTOWN; //���� ���� ������ ����
                    break;
            }
            break;
        }
    }
}