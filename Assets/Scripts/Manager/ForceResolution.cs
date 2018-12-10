using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceResolution : MonoBehaviour {

    [SerializeField] private int xRes, yRes;
    
    void Start () {
        Screen.SetResolution(xRes, yRes, false);
    }
	
}
