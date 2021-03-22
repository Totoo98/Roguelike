
using System.Collections.Generic;
using UnityEngine;


    public class Preallocation
    {
        public HashSet<Vector2Int> prealloc = new HashSet<Vector2Int>();

        public Preallocation()
        {
            for(int i = 0; i < 10000; ++i)
            {
                prealloc.Add(new Vector2Int(0, 0));
            }

            prealloc.Clear();
        }
    }
