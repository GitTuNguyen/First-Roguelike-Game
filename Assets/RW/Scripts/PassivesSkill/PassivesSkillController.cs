using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivesSkillController : SkillController
{
    public List<PassivesSkillStats> stats;

    protected override void Start()
    {
        base.Start();
        skillType = stats[0].skillType;
    }
    public override void SetStats(int level)
    {
        player.SetPassiveBonus(stats[level - 1]);
    }

    public override string getDescriptionNextLevel()
    {
        return stats[level].description;
    }

}
