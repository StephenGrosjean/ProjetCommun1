using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool useSpawnPos;
    [SerializeField] private int spawnPos;
    [SerializeField] private bool isEndDoor;

    private PlayerMovement playerMovementScript;
    private Rigidbody2D playerRigidbody2D;
    private FadeScript fadeScript;
    private bool isInside;
    private bool called = false;

	void Start () {
        playerRigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        fadeScript = GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScript>();
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire2") && isInside && !called){
            called = true;
            playerMovementScript.enabled = false;
            playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            fadeScript.StartCoroutine("FadeOut", sceneToLoad);
            if (useSpawnPos)
            {
                PlayerPrefs.SetInt("NextSpawnPos", spawnPos);
            }
            if (isEndDoor)
            {
                PlayerPrefs.DeleteAll();
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Player"){
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            isInside = false;
        }
    }
}
