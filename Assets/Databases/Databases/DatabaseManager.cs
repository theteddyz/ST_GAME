using System;
using UnityEngine;

namespace ForTeddy.Databases
{
    public class DatabaseManager : MonoBehaviour
    {
        public static DatabaseManager Instance;
        public CustomerDatabase CustomerDatabase;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        
        
    }
}
