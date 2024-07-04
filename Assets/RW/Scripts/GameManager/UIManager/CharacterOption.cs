using UnityEngine;
using UnityEngine.UI;



public class CharacterOption : MonoBehaviour
{
    public GameObject characterPrefabs;
    public Image characterImage;
    public Image weaponDefaultImage;    

    public void SetCharacter(GameObject character)
    {
        characterPrefabs = character;
        characterImage.sprite = characterPrefabs.GetComponent<SpriteRenderer>().sprite;
        weaponDefaultImage.sprite = characterPrefabs.GetComponent<Player>().characterStats.defaultWeapon.GetComponent<WeaponController>().sprite;
    }
}
