using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpawner : MonoBehaviour
{
    public GameObject[] spawnPoint = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bot")
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject scoringElement = ConePool.SharedInstance1.GetPooledObject();
                scoringElement.transform.position = spawnPoint[i].transform.position;
                scoringElement.SetActive(true);
                Debug.Log("Triggered: " + other.name);
            }
        }
    }
}
