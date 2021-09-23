using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Monster : MonoBehaviour
{
    public bool seePlayer = false;
    //Player đi vào vùng nhìn thấy của Monster
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            seePlayer = true;
            Destroy(GetComponent<Collider>());
        }
    }
}
