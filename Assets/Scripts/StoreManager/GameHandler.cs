using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net.Mime;
using CodeMonkey.Utils;
using ForTeddy.Databases;
using Unity.VisualScripting;
using UnityEngine.AI;

namespace TaskSystem
{
    

    public class GameHandler : MonoBehaviour
    {
        
   
        private TaskSystem taskSystem;
        [SerializeField] public List<GameObject> waypoints;
         public static List<String> menu = new List<string>();
         public static  List<Seats> seats = new List<Seats>();
        private GameObject[] chairs;



        private void Start()
        {
            menu.Add("coffee");
            menu.Add("cake");
            menu.Add("muffin");

            chairs = GameObject.FindGameObjectsWithTag("Seat");
            seats.Clear();
            for (int i = 0; i < chairs.Length; i++)
            {
                var obj = new Seats()
                {
                    chair = chairs[i],
                    isTaken = false


                };
                seats.Add(obj);

            }
            taskSystem = new TaskSystem();

            Customer customer = Customer.Create(waypoints[0].transform.position);

            CustomerTaskAI customerTaskAI = customer.gameObject.AddComponent<CustomerTaskAI>();
            customerTaskAI.Setup(customer, taskSystem);

            TaskSystem.Task task = new TaskSystem.Task.MoveToPosition { targetPosition = waypoints[1].transform.position };
            taskSystem.AddTask(task);
           

        }
        [System.Serializable]
        public class Seats
        {
            public GameObject chair;
            public bool isTaken = false;
            
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            
            if (col.gameObject.tag == "Customer")
            {
                Debug.Log("AAAAAAAAAAAAA");
                TaskSystem.Task task = new TaskSystem.Task.Order {  };
                taskSystem.AddTask(task);
            }
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TaskSystem.Task task = new TaskSystem.Task.ChooseSeat { };
                taskSystem.AddTask(task);
                
            }
            

           

        }
    }
    
    
    
}