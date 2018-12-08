using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    [SerializeField] private string sceneToLoad;

    private bool isInside;

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire2") && isInside){
            SceneManager.LoadScene(sceneToLoad);
        }
	}

    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Player"){
            isInside = true;
        }
    }
}
