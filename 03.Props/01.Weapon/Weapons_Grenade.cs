using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Grenade : MonoBehaviour
{
    bool canon;//조건문 다시실행or다시실행정지 역할 변수
    public float AddForceSpeed = 13;//수류탄을 날릴 속도

    public float Stop_and_Destroy = 2;// 바로 삭제되지 않게 멈춘 후 삭제시키는 시간 - (1)
    public float Replay = 4;// 수류탄 던지는 딜레이 시간 - (2)

    float countTime = 0; // 타이머 초기 값 - (1)
    float recountTime = 0; // 타이머 초기 값 - (2)
    
    bool startCount = false;
   
    bool restartCount = false; // 타이머 시작 bool변수 - (2)

    public Transform M67_Throw; // 프리펩이 생성 될 위치
    public GameObject M67_Prifab;// 프리펩을 드래그해서 가져온다 (public 선언으로 아무 오브젝트에 넣어도 상관없음)

    GameObject cannon;
    Rigidbody ri;

    public int HolyDamage;

    // Start is called before the first frame update
    void Start()
    {
        canon = true; // canon 조건 초기화
    }
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.R) && canon == true)//만약 숫자0번을 누르고,
                                                              // canon맴버변수가 true일때 (둘 다 만족)
        {
            startCount = true; // 카운트 시작 On - (1)
            restartCount = true; // 카운트 시작 On - (2)
            
            //프리펩 생성 후 날리기
            cannon = Instantiate(M67_Prifab);
            cannon.transform.position = M67_Throw.transform.position;

            ri = cannon.GetComponent<Rigidbody>();
            ri.AddForce(M67_Throw.transform.forward * AddForceSpeed, ForceMode.Impulse);

            canon = false; // 초기화
        }

        // 타이머↓↓

        if (startCount == true) // 카운트 시작 - (1)
        {
            countTime += Time.deltaTime;// countTime = countTime + Time.deltaTime;
        }

        if (restartCount == true) // 카운트 시작 - (2)
        {
            recountTime += Time.deltaTime;//recountTime = recountTime + Time.deltaTime;
        }
      

        if (countTime >= Stop_and_Destroy) // - (1)
        {
            M67_Stop_and_Destroy(); // 삭제 함수 호출


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
        ri.freezeRotation = true; // 리지드바디에 있는 freezeRotation을 켠다
        Destroy(cannon,1f); // 1초후에 cannon을 삭제시킨다
        StartCoroutine(M67_Stop_and_Destroy_Test());
    }
    IEnumerator M67_Stop_and_Destroy_Test()
    {
       
        yield return new WaitForSeconds(0.95f);//0.95초후에

        //해당 위치에서 정해진 반경까지의 주변 콜라이더들을 colls배열에 담는다
        //위치          반경
        Collider[] colls = Physics.OverlapSphere(cannon.transform.position, 10f);
        foreach (Collider col in colls)//foreach문을 통해 사용한다.
        {

            if (col.gameObject.tag == "Enemy")//만약 게임오브젝트의 태그가 Cube라면
            {
                //주변 콜라이더를 담은
                //콜라이더들의 안에 있는 리지드 바디를 가져온 후 AddExplosionForce함수를 쓴다.
                //(폭발력,     폭발위치,     반경,위로 솟구쳐올리는 힘)
                //col.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 10f, 5f);
                HolyDamage = (int)(Player_Data.Instance.playerData.Attack * 1.1f);

                col.GetComponent<Enemy>().TakeDamage(HolyDamage);
            }
        }

    }
}