using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceResolution : MonoBehaviour {

    [SerializeField] private int xReso = 640;
    [SerializeField] private int yReso = 576;
    
    void Start () {
        Screen.SetResolution(xReso, yReso, false);
    }
	
}
