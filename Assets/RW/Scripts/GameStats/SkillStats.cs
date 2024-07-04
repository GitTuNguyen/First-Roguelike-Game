using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Passive,
    Weapon
}
public class SkillStats : ScriptableObject
{
    public Sprite sprite;
    public SkillType skillType;
    public int level;
    public int maxLevel;    
    public string description;
}
