using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 frontdDir;
    public Vector2 moveDir;
    public CameraController cameraController;
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
        cameraController.player = character;
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
        moveDir = new Vector2(horizontal, vertical);
        if (moveDir != Vector2.zero)
        {
            frontdDir = moveDir;
            if ((frontdDir.x < 1 && frontdDir.x > -1) || (frontdDir.y < 1 && frontdDir.y > -1))
            {
                frontdDir = frontdDir.normalized;
            }
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
