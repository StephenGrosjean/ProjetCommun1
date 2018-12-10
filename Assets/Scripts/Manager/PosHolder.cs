using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosHolder : MonoBehaviour {
    [SerializeField] private Transform[] pos;
    public Transform[] Pos
    {
        get { return pos; }
    }

    private void Start()
    {
        
    }
}
