using System.Collections.Generic;
using UnityEngine;

namespace ForTeddy.Databases
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Databases/CustomerDatabase")]
    public class CustomerDatabase : ScriptableObject
    {
        public List<CustomerSC> customers; 



        public GameObject GetRandomCustomer()
        {
            return customers[UnityEngine.Random.Range(0, customers.Count)].prefab;
        }
    }
}
