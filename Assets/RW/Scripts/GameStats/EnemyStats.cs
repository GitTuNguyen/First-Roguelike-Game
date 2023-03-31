using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int HP;
    public float speed;
    public int DMG;
    public int EXP;
}
