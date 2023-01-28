using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Grenade : MonoBehaviour
{
    bool canon;//���ǹ� �ٽý���or�ٽý������� ���� ����
    public float AddForceSpeed = 13;//����ź�� ���� �ӵ�

    public float Stop_and_Destroy = 2;// �ٷ� �������� �ʰ� ���� �� ������Ű�� �ð� - (1)
    public float Replay = 4;// ����ź ������ ������ �ð� - (2)

    float countTime = 0; // Ÿ�̸� �ʱ� �� - (1)
    float recountTime = 0; // Ÿ�̸� �ʱ� �� - (2)
    
    bool startCount = false;
   
    bool restartCount = false; // Ÿ�̸� ���� bool���� - (2)

    public Transform M67_Throw; // �������� ���� �� ��ġ
    public GameObject M67_Prifab;// �������� �巡���ؼ� �����´� (public �������� �ƹ� ������Ʈ�� �־ �������)

    GameObject cannon;
    Rigidbody ri;

    public int HolyDamage;

    // Start is called before the first frame update
    void Start()
    {
        canon = true; // canon ���� �ʱ�ȭ
    }
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.R) && canon == true)//���� ����0���� ������,
                                                              // canon�ɹ������� true�϶� (�� �� ����)
        {
            startCount = true; // ī��Ʈ ���� On - (1)
            restartCount = true; // ī��Ʈ ���� On - (2)
            
            //������ ���� �� ������
            cannon = Instantiate(M67_Prifab);
            cannon.transform.position = M67_Throw.transform.position;

            ri = cannon.GetComponent<Rigidbody>();
            ri.AddForce(M67_Throw.transform.forward * AddForceSpeed, ForceMode.Impulse);

            canon = false; // �ʱ�ȭ
        }

        // Ÿ�̸ӡ��

        if (startCount == true) // ī��Ʈ ���� - (1)
        {
            countTime += Time.deltaTime;// countTime = countTime + Time.deltaTime;
        }

        if (restartCount == true) // ī��Ʈ ���� - (2)
        {
            recountTime += Time.deltaTime;//recountTime = recountTime + Time.deltaTime;
        }
      

        if (countTime >= Stop_and_Destroy) // - (1)
        {
            M67_Stop_and_Destroy(); // ���� �Լ� ȣ��


            countTime = 0;
            startCount = false;
        }

        if (recountTime >= Replay) // - (2)
        {
            canon = true;

            recountTime = 0;
            restartCount = false;
        }
    }

    void M67_Stop_and_Destroy()
    {
        ri.freezeRotation = true; // ������ٵ� �ִ� freezeRotation�� �Ҵ�
        Destroy(cannon,1f); // 1���Ŀ� cannon�� ������Ų��
        StartCoroutine(M67_Stop_and_Destroy_Test());
    }
    IEnumerator M67_Stop_and_Destroy_Test()
    {
       
        yield return new WaitForSeconds(0.95f);//0.95���Ŀ�

        //�ش� ��ġ���� ������ �ݰ������ �ֺ� �ݶ��̴����� colls�迭�� ��´�
        //��ġ          �ݰ�
        Collider[] colls = Physics.OverlapSphere(cannon.transform.position, 10f);
        foreach (Collider col in colls)//foreach���� ���� ����Ѵ�.
        {

            if (col.gameObject.tag == "Enemy")//���� ���ӿ�����Ʈ�� �±װ� Cube���
            {
                //�ֺ� �ݶ��̴��� ����
                //�ݶ��̴����� �ȿ� �ִ� ������ �ٵ� ������ �� AddExplosionForce�Լ��� ����.
                //(���߷�,     ������ġ,     �ݰ�,���� �ڱ��Ŀø��� ��)
                //col.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 10f, 5f);
                HolyDamage = (int)(Player_Data.Instance.playerData.Attack * 1.1f);

                col.GetComponent<Enemy>().TakeDamage(HolyDamage);
            }
        }

    }
}