﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    
    public static class NodeUtilities
    {

        /// <summary> Return a prettiefied type name. </summary>
        public static string PrettyName(this Type type)
        {
            if (type == null)
                return "null";
            if (type == typeof(System.Object))
                return "object";
            if (type == typeof(float))
                return "float";
            else if (type == typeof(int))
                return "int";
            else if (type == typeof(long))
                return "long";
            else if (type == typeof(double))
                return "double";
            else if (type == typeof(string))
                return "string";
            else if (type == typeof(bool))
                return "bool";
            else if (type.IsGenericType)
            {
                string s = "";
                Type genericType = type.GetGenericTypeDefinition();
                if (genericType == typeof(List<>))
                    s = "List";
                else
                    s = type.GetGenericTypeDefinition().ToString();

                Type[] types = type.GetGenericArguments();
                string[] stypes = new string[types.Length];
                for (int i = 0; i < types.Length; i++)
                {
                    stypes[i] = types[i].PrettyName();
                }
                return s + "<" + string.Join(", ", stypes) + ">";
            }
            else if (type.IsArray)
            {
                string rank = "";
                for (int i = 1; i < type.GetArrayRank(); i++)
                {
                    rank += ",";
                }
                Type elementType = type.GetElementType();
                if (!elementType.IsArray)
                    return elementType.PrettyName() + "[" + rank + "]";
                else
                {
                    string s = elementType.PrettyName();
                    int i = s.IndexOf('[');
                    return s.Substring(0, i) + "[" + rank + "]" + s.Substring(i);
                }
            }
            else
                return type.Name;
        }

    }
}
