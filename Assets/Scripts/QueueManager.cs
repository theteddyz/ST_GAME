using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public static  List<QueuePosition> queue = new List<QueuePosition>();
    private GameObject[] QueuePlace;
    private GameObject[] _Customer;
    public static List<Vector3> positionList;
    public Vector3 entrancePosition;
    public GameHandler gameHandler;
    public  List<GameObject> customerList;
    private bool isOrdering;
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


    }

    private void Update()
    {

        StartCoroutine(UpdateQueueWaitTime());
    }

    IEnumerator PositionChecker(List<Vector3> positionList)
    {
        
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
        agent.autoBraking = true;
        customerList.Add(col.gameObject);
        Debug.Log(customerList.Count);
        agent.destination = (positionList[customerList.IndexOf(col.gameObject)]);
        
        



    }
    
    public bool CanAddCustomer()
    {
        return customerList.Count < positionList.Count;
    }

    

    IEnumerator RelocateCustomers()
    {
        yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => gameHandler.isOrdering == false);
        for (int i = 0; i < customerList.Count; i++)
        {


            customerList[i].GetComponent<NavMeshAgent>().destination = positionList[i];

        }
       
    }


    IEnumerator CustomerMoveTo(Vector2 positon, GameObject customer)
    {
        NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
        agent.destination = positon;
        yield break;
    }
     IEnumerator GetFirstInQueue()
    {
        GameObject customer;
        if (customerList.Count == 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitUntil(() => gameHandler.isOrdering == false);
            customer = customerList[0];
            NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            agent.destination = gameObject.transform.parent.position;
            customerList.RemoveAt(0);
            yield return new WaitUntil(() => gameHandler.isOrdering == false);
            StartCoroutine(RelocateCustomers());
            Debug.Log(customerList.Count);
            Debug.Log(positionList.Count);
            yield break;
        }
    }

     IEnumerator UpdateQueueWaitTime()
     {
         yield return new WaitUntil(() => gameHandler.isOrdering == false);
         yield return new WaitForSeconds(3);
         if (gameHandler.isOrdering == false)
         {
             StartCoroutine(GetFirstInQueue());
         }
     }
     
     public void MoveTo(Vector3 position, GameObject customer)
     {


         NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            
            
         agent.destination = position;

           

     }

    
}
