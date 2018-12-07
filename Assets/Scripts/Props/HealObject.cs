using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour {
    [SerializeField] private int healPoints;
    private GameObject gameManager;
    private GameManager gameManagerScript;
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameManagerScript.AddLife(healPoints);
            Destroy(gameObject);
        }
    }
}
