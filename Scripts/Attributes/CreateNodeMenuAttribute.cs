using System;

namespace XMonoNode
{
    /// <summary> Manually supply node class with a context menu path </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CreateNodeMenuAttribute : Attribute
    {
        public string menuName;
        public int order;
        /// <summary> Manually supply node class with a context menu path </summary>
        /// <param name="menuName"> Path to this node in the context menu. Null or empty hides it. </param>
        public CreateNodeMenuAttribute(string menuName, params object[] parameters)
        {
            this.menuName = menuName;
            this.order = 0;
        }

        /// <summary> Manually supply node class with a context menu path </summary>
        /// <param name="menuName"> Path to this node in the context menu. Null or empty hides it. </param>
        /// <param name="order"> The order by which the menu items are displayed. </param>
        public CreateNodeMenuAttribute(string menuName, int order)
        {
            this.menuName = menuName;
            this.order = order;
        }
    }
}

