using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectCharacter : MonoBehaviour
{
    public GameObject characterPrefabs;
    public Image characterImage;
    public Image weaponDefaultImage;
    // Start is called before the first frame update
    void Start()
    {        
        characterImage.sprite = characterPrefabs.GetComponent<SpriteRenderer>().sprite;
        weaponDefaultImage.sprite = characterPrefabs.GetComponent<Player>().weaponDefault.GetComponent<WeaponController>().weaponSprite;
    }
}
