using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetting : MonoBehaviour
{
    public static CharacterSetting Instance;
    public GameObject characterSelected;
    public Image characterImage;
    public Image weaponImage;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    
    public void SelectCharacter(SelectCharacter character)
    {
        characterSelected = character.characterPrefabs;
        characterImage.sprite = character.characterImage.sprite;
        weaponImage.sprite = character.weaponDefaultImage.sprite;
    }
}
