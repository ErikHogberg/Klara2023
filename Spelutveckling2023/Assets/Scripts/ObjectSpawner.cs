using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner activeSpawner = null;
    public static bool active = true;

    public GameObject[] objectTypes;
    public Vector2Int gameSpaceSize = new Vector2Int(3, 3);
    public float gridSize = 2f;
    public float objectSpawnHeight = 10f;
    public float waitTime = 1f;

    GameObject queuedObject = null;
    
    float spawnLocationY = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (objectTypes.Length > 0)
            StartCoroutine(SpawnCycle());
    }

    void UpdateSpawnLocationY()
    {
        spawnLocationY = objectSpawnHeight + CameraManager.GetHighestY();
    }

    IEnumerator SpawnCycle()
    {
        while(active)
        {
            yield return new WaitForSeconds(waitTime);

            if (this == activeSpawner)
            {
                UpdateSpawnLocationY();
                SpawnObject();
            }
        }
    }

    void SpawnObject()
    {
        Object toCopy = objectTypes[Random.Range(0, objectTypes.Length)];

        if (queuedObject != null)
        {
            toCopy = queuedObject;
            queuedObject = null;
        }
        
        Vector3 spawnLoc = transform.position + new Vector3(gridSize/2f, spawnLocationY, gridSize/2f);
        spawnLoc.x += Random.Range(0, gameSpaceSize.x) * gridSize;
        spawnLoc.z += Random.Range(0, gameSpaceSize.y) * gridSize;

        Instantiate(toCopy, spawnLoc, Quaternion.Euler(0, 0, 0), gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        activeSpawner = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (activeSpawner == null)
            if (other.gameObject.tag == "Player")
                activeSpawner = this;
    }

    public void AddQueuedObject(GameObject toQueue)
    {
        queuedObject = toQueue;
    }
}
