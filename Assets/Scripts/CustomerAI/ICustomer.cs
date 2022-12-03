using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace CM_TaskSystem
{
    
    public interface Icustomer
    {
        void MoveTo(Vector3 position, Action onArrivedAtPosition = null);
    }
}
