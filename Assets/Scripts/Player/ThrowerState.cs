using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerState : MonoBehaviour {

    [SerializeField] private bool isInside;
    public bool IsInside
    {
        get { return isInside; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isInside = false;
        }
    }

}

