using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskSystem{
    


    public class TaskSystem
    {
        
        public abstract class Task
        {
            
            
            public class MoveToPosition : Task
            {
                public Vector3 targetPosition;
            }
            public class Order : Task
            {
                
            }

            public class ChooseSeat : Task
            {
                
            }
        }

        private List<Task> taskList;

        public TaskSystem()
        {
            taskList = new List<Task>();
        }

        public Task RequestNextTask()
        {
            if (taskList.Count > 0)
            {
                Task task = taskList[0];
                taskList.RemoveAt(0);
                return task;
            }else
            {
                return null;
            }
        }

        public void AddTask(Task task)
        {
            taskList.Add(task);
        }
    }
    
}
