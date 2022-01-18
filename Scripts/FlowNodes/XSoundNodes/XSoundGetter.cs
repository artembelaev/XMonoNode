using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    [System.Serializable]
    public class XSoundGetter : FlowNodeGraphGetter, ISerializationCallbackReceiver
    {
        public XSoundGetter(string pathToContainers, string containerFileName = "", string graphId = "") :
            base(pathToContainers, containerFileName, graphId)
        {
            drawPathToContainers = false;
        }

        public XSoundGetter() :
            base()
        {
            drawPathToContainers = false;
            PathToContainers = "Sounds/XContainers/";
        }

        public virtual void OnAfterDeserialize()
        {
            drawPathToContainers = false;
        }

        public virtual void OnBeforeSerialize()
        {
           // throw new System.NotImplementedException();
        }
    }
}
