using System;
using UnityEngine;

namespace XMonoNode
{
    /// <summary> Makes a serializable field hidden in node in node graph, but shown in ordinary unity inspector </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HideInNodeInspectorAttribute : Attribute
    {
    }

}