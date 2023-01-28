using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ���ӽ����̽� �߰�
using UnityEngine.EventSystems;// ���� �����̽� �߰�

public class Player_Move : MonoBehaviour
{
    // Ű ��
    float hdown;
    float vdown;
    bool roll; // �����⿡ ���� ����!
    bool isRoll; // ���� �����⸦ ���� ���� ����!

    // �⺻ ������
    public float moveSpeed = 10.0f;
    Vector3 dir;

    // �ִϸ�����
    public Animator ani;

    [HideInInspector] //<-Public�ν�����â���� ���������ʰ� �ϱ�
    //�÷�ġ�� ��ų�� ������ �Ǹ� �÷��̾��� ����,������,�������� ���� bool����
    public bool AttackUp;


    // public InventoryStart inventory;
    void Awake()
    {
        // �ڽ��� �ִϸ����� ������Ʈ�� �ҷ��´�.
        ani = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // Ű �� �Է�
        hdown = Input.GetAxis("Horizontal");
        vdown = Input.GetAxis("Vertical"); // ����Ű Ű �� �Է�
        roll = Input.GetButtonDown("Jump");// �����̽��� Ŭ������ ������!

        if (!IsUnReady())
            //�ε� ���� �ƴ� �� + PlayerData���� false�� �� �Լ� ����, true�� ����X
        {
            // �Լ� ����
            Move(); // ������
            Roll(); // �����⿡ ���� Ŭ���� �߰�!
        }
    }

    // ������
    void Move()
    {
        // AttackUp�� false���
        if (!AttackUp)
        {
            // ���� ����ȭ
            dir = new Vector3(hdown, 0, vdown).normalized;

            // isWalk �Ķ���� �ִϸ��̼�
            ani.SetFloat("isWalk", dir.magnitude);

            // �÷��̾� �����̱�
            transform.position += dir * Player_Data.Instance.playerData.Speed * Time.deltaTime;

            // ���� ���� ���Ͱ����� �ٶ󺸱�
            transform.LookAt(transform.position + dir);
        }
    }

    // ������
    void Roll()
    {
        // AttackUp�� false���
        if (!AttackUp)
        {
            //������ ����
            // ������ ��ư�� ���Ȱų� ������ ���� ���� ������ false�� ��
            if (roll && !isRoll)
            {
                // ������ ���� ���� ������ true
                isRoll = true;

                Debug.Log("������");

                // ������ �ִϸ��̼� �۵�
                ani.SetTrigger("doRoll");

                Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[0];
                Player_Data.Instance.audioSource.Play();
            }
        }
    }

    // �浹�ϰ� �ִ� ���¶��
    void OnCollisionStay(Collision collision)
    {
        // �΋H���� ��ü�� �±װ� �ٴ��̸�
        if (collision.gameObject.tag == "Ground")
        {
            // ������ ���� ���� ������ false�� �ȴ�!
            isRoll = false;
        }
    }

    // �浹 ���¸� �������� ����
    void OnCollisionExit(Collision collision)
    {
        // �浹�� �������� ���ӿ�����Ʈ tag�� Floor���
        if (collision.gameObject.tag == "Ground")
        {
            // ������ ���� ���� ������ true�� �ٲپ��.
            isRoll = true;
        }
    }

    public bool IsUnReady()
    {
        List<bool> isUnReady = new();

        isUnReady.Add(SceneLoadManager.Instance.isLoading);
        isUnReady.Add(UI_ChatLog.Instance.inputField.isFocused);
        isUnReady.Add(Player_Data.Instance.playerData.isDie);

        isUnReady.TrimExcess(); //���ʿ��� �޸� ��ȯ

        return isUnReady.Contains(true); //�� ���� �� �ϳ��� ���´��� �Ǵ�
    }
}