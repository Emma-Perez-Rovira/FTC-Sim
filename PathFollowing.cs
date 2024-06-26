using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector3 botPosition;
    public float speedModifier = 0.02f;
    private bool coroutineAllowed;
    private bool reversed = false;
    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }
    private void OnEnable()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }
    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coroutineAllowed) 
        { 
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        Vector3 p3 = Vector3.zero;
        if (!reversed)
        {
            p0 = routes[routeNumber].GetChild(0).position;
            p1 = routes[routeNumber].GetChild(1).position;
            p2 = routes[routeNumber].GetChild(2).position;
            p3 = routes[routeNumber].GetChild(3).position;
        } else
        {
            p0 = routes[routeNumber].GetChild(3).position;
            p1 = routes[routeNumber].GetChild(2).position;
            p2 = routes[routeNumber].GetChild(1).position;
            p3 = routes[routeNumber].GetChild(0).position;
        }

        while(tParam < 1)
        {
            tParam += Time.fixedDeltaTime * speedModifier;
            botPosition =  Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
            transform.position = botPosition;
            yield return new WaitForFixedUpdate();
        }
        tParam = 0f;
        if (!reversed)
        {
            routeToGo += 1;
            if (routeToGo > routes.Length - 1)
            {
                routeToGo--;
                reversed = true;
            }
        } else
        {
            routeToGo --;
            if (routeToGo < 0)
            {
                routeToGo++;
                reversed = false;
            }
        }
        coroutineAllowed = true;
    }
}
