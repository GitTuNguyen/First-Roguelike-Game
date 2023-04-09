using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSetting : MonoBehaviour
{
    public static CharacterSetting Instance;
    public List<GameObject> characterPrefabList;
    public List<CharacterOption> characterOptions;
    
    [Header("Character Seleted Details")]
    public GameObject characterSelectedDetails;
    public GameObject characterSelected;
    public Image characterImage;
    public Image weaponImage;
    public TextMeshProUGUI characterInfo;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    
    public void SetCharacterOptions()
    {
        int index = 0;
        foreach (var option in characterOptions)
        {
            option.SetCharacter(characterPrefabList[index]);
            option.gameObject.SetActive(true);            
            index++;
            if (index == characterPrefabList.Count)
            {
                break;
            }
        }
    }
    public void SelectCharacter(CharacterOption character)
    {
        if (!characterSelectedDetails.activeSelf)
        {
            characterSelectedDetails.SetActive(true);
        }
        characterSelected = character.characterPrefabs;
        characterImage.sprite = character.characterImage.sprite;
        weaponImage.sprite = character.weaponDefaultImage.sprite;               
        characterInfo.text = character.characterPrefabs.GetComponent<Player>().characterStats.characterInfo;
    }
}
