using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarine
{
    public abstract class BaseModule : MonoBehaviour
    {
        public bool IsEnabled { get; private set; } = true;
        
        public virtual void EnableModule()
        {
            IsEnabled = true;
        }

        public virtual void DisableModule() 
        {
            IsEnabled = false;
        }
    }
}