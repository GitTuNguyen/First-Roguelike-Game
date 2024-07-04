using System.Collections.Generic;
public class PassivesSkillController : SkillController
{
    public List<PassivesSkillStats> stats;

    protected override void Start()
    {
        base.Start();
        skillType = stats[0].skillType;
        maxLevel = stats.Count;
    }
    public override void SetStats(int level)
    {
        player.SetPassiveBonus(stats[level - 1]);
    }

    public override string getDescriptionNextLevel()
    {
        try
        {
            return stats[level].description;
        }
        catch (System.Exception e)
        {
            print(stats[level - 1].name + "has error at level " + level);
            return "";
        }        
    }
}
