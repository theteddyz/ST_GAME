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
        if (col.gameObject.tag == "Customer")
        {
            NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
            agent.autoBraking = true;
            customerList.Add(col.gameObject);
            Debug.Log(customerList.Count);
            agent.destination = (positionList[customerList.IndexOf(col.gameObject)]);
        }
        
        
    }
    
    public bool CanAddCustomer()
    {
        return customerList.Count < positionList.Count;
    }

    

    public IEnumerator RelocateCustomers()
    {
        
        for (int i = 0; i < customerList.Count; i++)
        {
            
            
            customerList[i].GetComponent<NavMeshAgent>().destination = positionList[i];

        } yield break;
    }

    

    public IEnumerator GetFirstInQueue()
    {
        GameObject customer;
        if (customerList.Count == 0)
        {
            yield return null;
        }
        else
        {
            
            
            customer = customerList[0];
            NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            agent.destination = gameObject.transform.parent.position;
            customerList.RemoveAt(0);
            
            yield break;
        }
    }

    
     
     

    
}
