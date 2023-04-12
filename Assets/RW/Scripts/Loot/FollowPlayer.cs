using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isFollowing;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    public float modifier;
    // Start is called before the first frame update
    void Start()
    {
        isFollowing = false;
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {

            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, Time.deltaTime * modifier);
        }
        
    }

    public void Follow()
    {
        isFollowing = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpArea"))
        {
            animator.SetTrigger("PickUp");
        }
    }
}
