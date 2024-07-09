using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    //Claim all gem
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            List<GameObject> lootItemList = GameStateManager.Instance.lootItemList;
            if (lootItemList.Count > 0)
            {
                ClaimAllGem(lootItemList);
            }
            AudioManager.Instance.PlaySFX("PickUpChicken");
            Destroy(gameObject);
        }
    }

    public void ClaimAllGem(List<GameObject> lootItemList)
    {
        List<GameObject> gemList = lootItemList.Where(o => o != null && o.GetComponent<ExperienceGem>() != null).ToList();
        foreach (var gem in gemList)
        {
            FollowPlayer followPlayer = gem.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.PickUp(true);
            }
        }
    }
}
