using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConePool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public static ConePool SharedInstance1 { get; private set; }
    void Awake()
    {
        if (SharedInstance1 != null && SharedInstance1 != this)
        {
            Debug.Log("Held in: " + this.gameObject.name);
            Debug.Log("Destroyed: " + SharedInstance1.ToString());
            Destroy(this);
        }
        else
        {
            SharedInstance1 = this;
        }
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            Rigidbody rb = tmp.GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            //Debug.Log(pooledObjects.Count);
        }
    }

    public GameObject GetPooledObject()
    {

        for (int i = 0; i < amountToPool; i++)
        {
            Debug.Log("Requested: " + i + " List size: " + pooledObjects.Count);
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;

    }

    private void FixedUpdate()
    {
        //Debug.Log("Pooled objects list size: " + pooledObjects.Count);
    }
}
