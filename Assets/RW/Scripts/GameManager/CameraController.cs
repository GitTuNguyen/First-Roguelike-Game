using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;    

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        } else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }        
    }
}
