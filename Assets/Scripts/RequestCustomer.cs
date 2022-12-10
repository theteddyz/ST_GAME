using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCustomer : MonoBehaviour
{
    public GameHandler gameHandler;
    public QueueManager queueManager;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Customer")
        {
            Debug.Log("Entered");
            Debug.Log(gameHandler.isOrdering);
          
            
                StartCoroutine(OrderFreeCheck());
            
            
        }
    }


    IEnumerator OrderFreeCheck()
    {
        
        yield return new WaitUntil(() => gameHandler.isOrdering == false);
        
        if (gameHandler.isOrdering == false)
        {
            
            StartCoroutine(queueManager.GetFirstInQueue());
            gameHandler.isOrdering = true;
            StartCoroutine(queueManager.RelocateCustomers());
        }
       
        
       
    }
}
