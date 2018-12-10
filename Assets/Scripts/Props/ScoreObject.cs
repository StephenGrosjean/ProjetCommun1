using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour {
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject destroyParticles;
    [SerializeField] private int scorePoints;
    [SerializeField] private string objectName;
    [SerializeField] private int uniqueID;
    private GameObject gameManager;
    private GameManager gameManagerScript;

    private int asBeenPickedUp;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        asBeenPickedUp = PlayerPrefs.GetInt(objectName + uniqueID.ToString());
        audioSource = GetComponent<AudioSource>();


        if (asBeenPickedUp == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource = other.gameObject.GetComponent<AudioSource>();
            PlayerPrefs.SetInt(objectName + uniqueID.ToString(), 1);
            gameManagerScript.IncreaseScore(scorePoints);
            DestroyParticles();
            audioSource.clip = pickupSound;
            audioSource.Play();
            Destroy(gameObject);
        }
    }

    private void DestroyParticles()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
}
