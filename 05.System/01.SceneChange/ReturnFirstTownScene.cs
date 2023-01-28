using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnFirstTownScene : MonoBehaviour
{
    //Ÿ�̸� ���� ����
    float Timer = 0;
    //Ÿ�̸Ӱ� 2.5�ʺ��� ũ�ų� ���ٸ� ������ ���ư��� ��ư�� �߰� �ϴ� Ÿ�̸Ӻ���
    float ReturnFirstTownSceneTimer = 2.5f;
    private void FixedUpdate()
    {
        //playerData�� isDie�� true���
        if (Player_Data.Instance.playerData.isDie == true)
        {
            //�� ������Ʈ�� 0��° �ڽ��� Ȱ��ȭ �� �ش�.
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //Ȱ��ȭ �� Timer���� �ð��� �帣���� �� �ش�.
            Timer += Time.deltaTime;
            //Timer�� 2.5f���� ũ�ų� ���ٸ�
            if (Timer >= ReturnFirstTownSceneTimer)
            {
                //�� ������Ʈ�� 1��° �ڽ��� Ȱ��ȭ �� �ش�.
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    //Back_FirstScene_Button�� �ڽ��� Text (TMP)�� Button������Ʈ�� �� OnClick�Լ�
    public void Back_FirstSceneFunc()
    {
        //��ư�� ���ȴٸ� Ÿ�̸Ӹ� �ٽ� 0���� ������ش�.
        Timer = 0f;

        SceneLoadManager.Instance.SceneTransOK(true); //�� ��ȯ ���� ���·� ����
        SceneManager.LoadScene(1);                    //�ε� ������ ��ȯ


        //�÷��̾� ��ġ �ʱ�ȭ
        Player_Data.Instance.transform.position = new Vector3(-40, 0, 0);
        //�� ������Ʈ�� 0��° �ڽ��� ��Ȱ��ȭ �� �ش�.
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //�� ������Ʈ�� 1��° �ڽ��� ��Ȱ��ȭ �� �ش�.
        gameObject.transform.GetChild(1).gameObject.gameObject.SetActive(false);
        //playerData�� isDie�� �ٽ� false�� ���� �÷��̾ �ٽ� ������ �� �ֵ����Ѵ�.
        Player_Data.Instance.playerData.isDie = false;
        //playerData�� playerData.Hp�� �ٽ� playerData.MaxHp�� ���� �÷��̾��� ü���� �ٽ� MaxHp�� ������ش�.
        Player_Data.Instance.playerData.Hp = Player_Data.Instance.playerData.MaxHp;
        //�÷��̾��� CapsuleCollider�� �ٽ� Ȱ��ȭ �����ش�.
        Player_Data.Instance.GetComponent<CapsuleCollider>().enabled = true;
        //Player_Combat�� Ʈ����Ÿ���� Player_Live�Ķ���͸� ���� ���Ѽ� �÷��̾ �ٽ� �Ͼ�� ���ƴٴ� �� �ֵ��� �� �ش�.
        Player_Data.Instance.GetComponent<Player_Combat>().ani.SetTrigger("Player_Live");
        //Player_Combat�� rigid.isKinematic�� false�� �ٲ��༭ �÷��̾ �ٽ� �������� �浹�� �����ϵ��� ������ش�.
        Player_Data.Instance.GetComponent<Player_Combat>().rigid.isKinematic = false;
        //Player_Combat�� rigid.useGravity�� true�� �ٲ��༭ �÷��̾ �ٽ� �������� �浹�� �����ϵ��� ������ش�.
        Player_Data.Instance.GetComponent<Player_Combat>().rigid.useGravity = true;
        //Player_Combat�� sword.enabled�� true�� �ٲ��༭ �÷��̾ ���͸� �ٽ� ���� �� �� �ְ� ������ش�.
        Player_Data.Instance.GetComponent<Player_Combat>().sword.enabled = true;
        //UI_StatusBar�� hpBar.value�� maxValue�� �ٲ㼭 �ٽ� �÷��̾��� ü���� �� ������ ����� �ش�.
        UI_StatusBar.Instance.hpBar.value = UI_StatusBar.Instance.hpBar.maxValue;
    }
}