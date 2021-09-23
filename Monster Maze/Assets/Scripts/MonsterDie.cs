using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : MonoBehaviour
{
    MonsterController monCon;
    Animator animator;
    Collider colli;

    // Start is called before the first frame update
    void Start()
    {
        monCon = GetComponent<MonsterController>();
        animator = GetComponent<Animator>();
        colli = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    //Quái vật chết
    void Die()
    {
        if (monCon.healthPoint <= 0 && monCon.dead == false)
        {
            monCon.enabled = false;
            monCon.dead = true;
            StartCoroutine(WaitForDie(3));
        }
    }

    
    //Độ trễ cho hoạt hình chết
    IEnumerator WaitForDie(float second)
    {
        animator.Play("Die");
        colli.enabled = false;
        Debug.Log(gameObject.name + "Animation Die");

        yield return new WaitForSeconds(second);

        Destroy(gameObject);
    }
}
