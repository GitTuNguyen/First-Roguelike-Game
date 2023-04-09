using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float randomIntensity = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomIntensity, randomIntensity), 0, 0);
        Destroy(gameObject, timeToDestroy);
    }

}
