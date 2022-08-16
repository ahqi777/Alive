using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    NavMeshAgent meshAgent;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.SetDestination(target.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
