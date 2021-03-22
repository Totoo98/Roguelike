using System.Collections.Generic;
using UnityEngine;

public class RuntimeList<T> : ScriptableObject
{
    public List<T> List = new List<T>();

    public void Add(T element)
    {
        List.Add(element);
    }

    public void Remove(T element)
    {
        List.Remove(element);
    }
}
