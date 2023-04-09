using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnProps()
    {
        foreach(GameObject spawnPoint in propSpawnPoints)
        {
            GameObject prop =  Instantiate(propPrefabs[Random.Range(0, propPrefabs.Count)], spawnPoint.transform.position, Quaternion.identity) as GameObject;
            prop.transform.parent = spawnPoint.transform;
        }
    }
}
