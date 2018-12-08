using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMode : MonoBehaviour {

    private bool isThrowed;
    public bool IsThrowed
    {
        set { isThrowed = value; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isThrowed)
        {
            if(other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
