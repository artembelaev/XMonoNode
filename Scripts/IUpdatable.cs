using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public interface IUpdatable
    {
        public void OnUpdate(float deltaTime);
    }
    
}
