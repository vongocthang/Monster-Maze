using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public MonsterInformation monsterInfor;//Thông tin Monster lưu ở ScriptableObject
    public MonsterInformation playerInfor;//Thông tin Player lưu ở ScriptableObject
    public HealthBar health;
    Animator animator;
    NavMeshAgent nav;//AI nằm ở đối tượng Parent
    public AI_Monster ai;//Script AI nằm ở đối tượng Parent
    public bool inAttackRange = false;//Trong tầm đánh của Player
    public bool touchPlayer = false;//Chạm vào Player
    GameObject player;//Đối tượng Player
    float timeLine;//Mốc thời gian để xác định lần Attack tiếp theo
    public float healthPoint;//Điểm sự sống của Monster
    PlayerController pc;
    public bool dead = false;//Đã chết

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        ai = GetComponentInParent<AI_Monster>();
        timeLine = Time.time;
        healthPoint = monsterInfor.health;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        health.SetHealth(healthPoint, monsterInfor.health);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        AttackPlayer();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            timeLine = Time.time - monsterInfor.attackRate / 2;//Mốc thời gian tính attackRate, lần đầu tấn công
            touchPlayer = true;
        }

        if (other.tag == "InAttackRange")
        {
            inAttackRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            touchPlayer = false;
        }

        if (other.tag == "InAttackRange")
        {
            inAttackRange = false;
        }
    }

    void FollowPlayer()
    {
        if (ai.seePlayer == true && touchPlayer == false)
        {
            nav.speed = monsterInfor.speed;
            nav.SetDestination(player.transform.position);
            animator.PlayInFixedTime("Walk");
        }
    }
    
    void AttackPlayer()
    {
        if (touchPlayer == true && dead == false)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = transform.parent.localRotation;
            transform.parent.localRotation = Quaternion.Slerp(current, rotation, 5 * Time.deltaTime);

            if (Time.time > timeLine + monsterInfor.attackRate)
            {
                animator.PlayInFixedTime("Attack");
                timeLine = Time.time;
                pc.HealthControll(monsterInfor.damge);
            }
        }
    }

    //Quản lý thanh máu
    public void HealthControll()
    {
        healthPoint -= playerInfor.damge;
        health.SetHealth(healthPoint, monsterInfor.health);
        //Debug.Log(gameObject.name + " bi tan cong");
    }

    //IEnumerator WaitForHit(float second)
    //{
    //    yield return new WaitForSeconds(second);
    //    pc.attackNow = false;
    //}
}
