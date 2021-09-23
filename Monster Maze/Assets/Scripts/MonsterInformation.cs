using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Monster Information", menuName ="MonsterInformation")]
public class MonsterInformation : ScriptableObject
{
    public new string name;
    public float damge;
    public float attackRate;
    public float health;
    public float speed;
}
