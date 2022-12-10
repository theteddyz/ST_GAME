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



    

    public class GameHandler : MonoBehaviour
    {
        
        
        public static List<_waypoint> waypoints = new List<_waypoint>();
        public static List<String> menu = new List<String>();
        public static  List<Seats> seats = new List<Seats>();
        private GameObject[] chairs;
        private GameObject[] waypoint;
        [SerializeField] public Sprite sittingSprite;
        private QueueManager queueManger;
        public  bool isOrdering;
        
       



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

            waypoint = GameObject.FindGameObjectsWithTag("Waypoint");
            waypoints.Clear();
            for (int i = 0; i < waypoint.Length; i++)
            {
                var _obj = new _waypoint()
                {
                    addedWaypoint =  waypoint[i]
                };
                waypoints.Add(_obj);

            }


            StartCoroutine(spawnCustomer());

        }
        [System.Serializable]
        public class Seats
        {
            public GameObject chair;
            public bool isTaken = false;
            
        }
        [System.Serializable]
        public class _waypoint
        {
            public GameObject addedWaypoint;
            
            
        }
        
        [System.Serializable]
        public class _Customer
        {
            public GameObject _customer;
            
            
        }
        
        
        
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            
            if (col.gameObject.tag == "Customer")
            {


                StartCoroutine(col.gameObject.GetComponent<CustomerTaskAI>().ExecuteTask_Order());
                isOrdering = true;
            }
            
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            StartCoroutine(IsOrderingSwitch(col.gameObject));
        }
        

        IEnumerator spawnCustomer()
        {
            
            yield return new WaitForSeconds(3);
            GameObject gameObject;
            int ranroll = UnityEngine.Random.Range(0, 4);
            
            gameObject = Instantiate(DatabaseManager.Instance.CustomerDatabase.GetRandomCustomer(), waypoint[ranroll].transform.position, Quaternion.identity);
            
            CustomerTaskAI customerTaskAI = gameObject.AddComponent<CustomerTaskAI>();
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            gameObject.GetComponent<CustomerTaskAI>().orginLocation = ranroll;
    

            agent.destination = waypoints[5].addedWaypoint.transform.position;
            StartCoroutine(spawnCustomer());
            

        }

        IEnumerator IsOrderingSwitch(GameObject customer)
        {
            yield return new WaitForSeconds(4);
            if (customer.tag == "Customer")
            {
                isOrdering = false;
            }
        }

        

    }
    
    
    