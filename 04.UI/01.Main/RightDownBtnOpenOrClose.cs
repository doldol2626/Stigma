using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDownBtnOpenOrClose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
 
    public void BtnOnClick()
    {

        
        if (gameObject.transform.GetChild(0).gameObject.activeSelf==false) 
        {

            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
