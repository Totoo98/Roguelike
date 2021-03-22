using System.Collections.Generic;
using UnityEngine;

public class RuntimeHashSet<T> : ScriptableObject
{
    public HashSet<T> Collection = new HashSet<T>();

    public void Add(T element)
    {
        Collection.Add(element);
    }

    public void Remove(T element)
    {
        Collection.Remove(element);
    }
}