﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private GameObject magnet;
    [SerializeField] private Transform stuckPosition;
    [SerializeField] private GameObject lockedObject;
    [SerializeField] private Transform throwPositionLeft, throwPositionRight;
    [SerializeField] private int throwForce;
    [SerializeField] private AudioClip throwSound, magnetSound;

    private bool locked;
    private SpriteRenderer playerSpriteRenderer;
    private AudioSource audioSourceComponent;
    private GameManager gameManagerScript;

    private ThrowerState throwerScriptLeft, throwScriptRight;
    private bool canThrowLeft, canThrowRight;

    private void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        audioSourceComponent = GetComponent<AudioSource>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        throwerScriptLeft = throwPositionLeft.GetComponent<ThrowerState>();
        throwScriptRight = throwPositionRight.GetComponent<ThrowerState>();
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            canThrowLeft = !throwerScriptLeft.IsInside;
            canThrowRight = !throwScriptRight.IsInside;

            if (locked)
            {
                lockedObject.transform.position = stuckPosition.position;
                Invoke("CancelCatch", 0);
            }

            if (Input.GetButtonDown("Fire1") && locked)
            {
                Invoke("Release", 0);
            }
            else if (Input.GetButtonDown("Fire1") && !locked)
            {
                Invoke("Catch", 0);
            }
            else if (Input.GetButtonUp("Fire1") && !locked)
            {
                Invoke("CancelCatch", 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            locked = true;
            lockedObject = other.gameObject;
            lockedObject.GetComponent<EnemyController>().enabled = false;
            lockedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            lockedObject.layer = 10;
            gameManagerScript.IncreaseScore(100);
        }
    }

    void Catch()
    { 
        audioSourceComponent.clip = magnetSound;
        audioSourceComponent.loop = true;
        if (!audioSourceComponent.isPlaying)
        {
            audioSourceComponent.Play();

        }
        magnet.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = !locked;
    }

    void CancelCatch()
    {
        audioSourceComponent.loop = false;
        audioSourceComponent.Stop();
        magnet.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    void Release()
    {

        if (playerSpriteRenderer.flipX && canThrowLeft)
        {
            audioSourceComponent.PlayOneShot(throwSound);

            Rigidbody2D lockedObjectRigid = lockedObject.GetComponent<Rigidbody2D>();


            lockedObject.GetComponent<EnemyController>().enabled = true;
            lockedObjectRigid.constraints = RigidbodyConstraints2D.None;
            lockedObjectRigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            lockedObjectRigid.gravityScale = 0;

            lockedObject.layer = 12;
            locked = false;

            lockedObject.transform.position = throwPositionLeft.position;
            lockedObjectRigid.velocity = Vector2.left * throwForce;

            lockedObject.GetComponent<ThrowMode>().IsThrowed = true;
            lockedObject = null;
        }
        else if(!playerSpriteRenderer.flipX && canThrowRight)
        {
            audioSourceComponent.PlayOneShot(throwSound);

            Rigidbody2D lockedObjectRigid = lockedObject.GetComponent<Rigidbody2D>();


            lockedObject.GetComponent<EnemyController>().enabled = true;
            lockedObjectRigid.constraints = RigidbodyConstraints2D.None;
            lockedObjectRigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            lockedObjectRigid.gravityScale = 0;

            lockedObject.layer = 12;
            locked = false;

            lockedObject.transform.position = throwPositionRight.position;
            lockedObjectRigid.velocity = Vector2.right * throwForce;

            lockedObject.GetComponent<ThrowMode>().IsThrowed = true;
            lockedObject = null;
        }
    }
}
