using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public static  List<QueuePosition> queue = new List<QueuePosition>();
    private bool allIsTaken =false;
    private GameObject[] QueuePlace;
    private GameObject[] _Customer;
    private List<Vector3> positionList;
    public Vector3 entrancePosition;
    private GameHandler gameHandler;
    private List<GameObject> customerList;
    private void Start()
    {

        positionList = new List<Vector3>();
        

        QueuePlace = GameObject.FindGameObjectsWithTag("QueuePosition");
        queue.Clear();
        for (int i = 0; i < QueuePlace.Length; i++)
        {
            var obj = new QueuePosition()
            {
                queuePlace = QueuePlace[i],
                isTaken = false


            };
            queue.Add(obj);

        }
        for (int i = 0; i <  queue.Count; i++)
        {
        
            positionList.Add(queue[i].queuePlace.transform.position);

        }

       
        entrancePosition = queue[5].queuePlace.transform.position;
        customerList = new List<GameObject>();
        Debug.Log(queue[5].queuePlace.transform.position);


    }

    IEnumerator PositionChecker(List<Vector3> positionList)
    {
        this.positionList = positionList;
        yield break;
    }
    
    
    [System.Serializable]
    public class QueuePosition
    {
        public GameObject queuePlace;
        public bool isTaken = false;
            
    }
    [System.Serializable]
    public class Customer
    {
        public GameObject customer;
        
            
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
        customerList.Add(col.gameObject);
        
        agent.destination = (positionList[customerList.IndexOf(col.gameObject)]);
        
    }


    public bool CanAddCustomer()
    {
        return customerList.Count < positionList.Count;
    }


   

    



}
