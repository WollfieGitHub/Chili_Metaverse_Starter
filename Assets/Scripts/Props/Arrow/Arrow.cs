using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public string UID { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        UID = "Arrow:" + Environment.TickCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private sealed class UidEqualityComparer : IEqualityComparer<Arrow>
    {
        public bool Equals(Arrow x, Arrow y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.UID == y.UID;
        }

        public int GetHashCode(Arrow obj)
        {
            return (obj.UID != null ? obj.UID.GetHashCode() : 0);
        }
    }

    public static IEqualityComparer<Arrow> UidComparer { get; } = new UidEqualityComparer();
}
