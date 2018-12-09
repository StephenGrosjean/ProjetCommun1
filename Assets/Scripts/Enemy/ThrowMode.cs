using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMode : MonoBehaviour {
    [SerializeField] private AudioClip destroySound;

    private bool isThrowed;
    public bool IsThrowed
    {
        set { isThrowed = value; }
    }

    private AudioSource audioSourceComponent;
    private GameManager gameManagerScript;

    private void Start()
    {
        audioSourceComponent = GetComponent<AudioSource>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isThrowed)
        {
            if(other.gameObject.tag == "Enemy")
            {
                audioSourceComponent.PlayOneShot(destroySound);
                gameManagerScript.IncreaseScore(200);
                Destroy(other.gameObject);
            }
            audioSourceComponent.PlayOneShot(destroySound);
            Destroy(gameObject);
        }
    }
}
