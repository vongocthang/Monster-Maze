using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public MonsterInformation playerInfor;//Thông tin Player lưu ở ScriptableObject
    Animator animator;
    int count = 1;//Dùng để chạy các Animation Attack
    float timeLine;//
    AudioSource attackAudio;
    GameObject character;
    GameObject[] monsterList;
    Vector3 monsterPos;
    bool inAttackRange = false;
    public float healthPoint;//Điểm sự sống của Player
    public HealthBar health;
    public bool dead = false;//Player đã chết
    public bool complete = false;//Hoàn thành - chạm vào lối thoát khỏi mê cung

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Main").GetComponent<Animator>();
        timeLine = Time.time;
        attackAudio = GameObject.Find("Player/Audio/Attack").GetComponent<AudioSource>();
        character = GameObject.Find("Character");
        monsterList = GameObject.FindGameObjectsWithTag("Monster");
        healthPoint = playerInfor.health;
        health.SetHealth(healthPoint, playerInfor.health);
    }

    // Update is called once per frame
    void Update()
    {
        monsterList = GameObject.FindGameObjectsWithTag("Monster");
        PlayerDie();
    }

    //Tấn công
    public void Attack()
    {
        if (Time.time > timeLine + playerInfor.attackRate)
        {
            animator.PlayInFixedTime("Attack" + count);
            attackAudio.PlayDelayed(0);
            timeLine = Time.time;
            count++;
            if (count == 3)
            {
                count = 1;
            }

            //Các Monster chịu sát thương từ đòn tấn công
            for (int i=0; i<monsterList.Length; i++)
            {
                MonsterController mc = monsterList[i].GetComponent<MonsterController>();
                if (mc.inAttackRange == true)
                {
                    mc.HealthControll();
                }
            }
        }
    }

    //Quay người về hướng quái vật khi tấn công
    void LookMonster()
    {
        //Quái vật trong tầm đánh và ấn nút tấn công
        if (inAttackRange == true && Time.time - timeLine < playerInfor.attackRate)
        {
            Vector3 relativePos = monsterPos - character.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = character.transform.localRotation;
            character.transform.localRotation = Quaternion.Slerp(current, rotation, 5 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            monsterPos = other.transform.position;
            inAttackRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            inAttackRange = false;
        }
    }

    //Quản lý thanh máu
    public void HealthControll(float monsterDamge)
    {
        healthPoint -= monsterDamge;
        health.SetHealth(healthPoint, playerInfor.health);
        //Debug.Log(gameObject.name + " bi tan cong");
    }

    //Player die
    void PlayerDie()
    {
        if (healthPoint <= 0)
        {
            dead = true;
            animator.Play("Die");
        }
    }
}
