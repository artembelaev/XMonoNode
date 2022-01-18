using System;

namespace XMonoNode
{
    /// <summary> Mark a serializable field be hidden when node minimized </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HidingAttribute : Attribute
    {
    }
}
