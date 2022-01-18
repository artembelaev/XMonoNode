using System;

namespace XMonoNode
{
    /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted. </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireNodeAttribute : Attribute
    {
        public Type type0;
        public Type type1;
        public Type type2;

        /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
        public RequireNodeAttribute(Type type)
        {
            this.type0 = type;
            this.type1 = null;
            this.type2 = null;
        }

        /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
        public RequireNodeAttribute(Type type, Type type2)
        {
            this.type0 = type;
            this.type1 = type2;
            this.type2 = null;
        }

        /// <summary> Automatically ensures the existance of a certain node type, and prevents it from being deleted </summary>
        public RequireNodeAttribute(Type type, Type type2, Type type3)
        {
            this.type0 = type;
            this.type1 = type2;
            this.type2 = type3;
        }

        public bool Requires(Type type)
        {
            if (type == null)
                return false;
            if (type == type0)
                return true;
            else if (type == type1)
                return true;
            else if (type == type2)
                return true;
            return false;
        }
    }
}
