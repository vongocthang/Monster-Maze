using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //abc();
        //StartCoroutine(WaitForSeconds(2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //abc();
        StartCoroutine(WaitForSeconds(2));
    }

    void abc()
    {
        //Debug.Log("1");
        //StartCoroutine(WaitForSeconds(2));
        //Debug.Log("2");
    }

    //Độ trễ
    public IEnumerator WaitForSeconds(float second)
    {
        Debug.Log("1");
        yield return new WaitForSeconds(second);
        Debug.Log("2");
        
    }
}
