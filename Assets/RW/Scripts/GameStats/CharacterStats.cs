using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public GameObject defaultWeapon;
    public int characterHP;
    public float characterSpeed;
}
