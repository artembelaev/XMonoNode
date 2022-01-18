using System;

namespace XMonoNode
{
    /// <summary> Mark a serializable field's label be hidden </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HideLabelAttribute : Attribute
    {
    }
}
