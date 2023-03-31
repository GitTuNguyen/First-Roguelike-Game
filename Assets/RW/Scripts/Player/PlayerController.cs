using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 frontdDir;
    [HideInInspector]
    public Vector2 moveDir;

    private CharacterAnimationController characterAnimatorController;

    private void Awake()
    {
        frontdDir = new Vector2(1, 0);
    }
    void Start()
    {
        InitializeCharacter();
    }

    private void InitializeCharacter()
    {
        GameObject character = Instantiate(CharacterSetting.Instance.characterSelected, transform.position, Quaternion.identity, gameObject.transform);
        characterAnimatorController = character.GetComponent<CharacterAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        if (characterAnimatorController != null)
        {
            TranslateAnimation();
        }
    }
    
    private void InputManager()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontal, vertical).normalized;
        if (moveDir != Vector2.zero)
        {
            frontdDir = moveDir;
        }        
    }    

    private void TranslateAnimation()
    {
        if (moveDir == Vector2.zero)
        {
            characterAnimatorController.IdleAnimation();
        }
        else
        {
            characterAnimatorController.RunAnimation();
            if (moveDir.x < 0)
            {
                characterAnimatorController.SetFlip(true);
            }
            else if (moveDir.x > 0)
            {
                characterAnimatorController.SetFlip(false);
            }
        }
    }
}
