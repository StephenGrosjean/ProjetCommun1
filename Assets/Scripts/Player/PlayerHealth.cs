﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int health = 6;
    [SerializeField] private int lifes = 3;
    [SerializeField] private float minimumPosition;
    [SerializeField] private float destroyPosition;

    private bool canRemoveLife = true;
    private const float lifeTime = 0.1f;
    public int deathForce;
    private bool deathToggled;
    private string thisScene;
    private bool canKill;

    //Scripts
    private PlayerMovement playerMovementScript;
    private Rigidbody2D rigid;
    private CircleCollider2D colliderScript;

    private void Start(){
        playerMovementScript = GetComponent<PlayerMovement>();
        rigid = GetComponent<Rigidbody2D>();
        colliderScript = GetComponent<CircleCollider2D>();
        thisScene = "TestScene";
    }

    private void Update(){
       if(health <= 0 && !deathToggled){
            deathToggled = true;
            StartCoroutine("DeathSequence");
        }

       if(transform.position.y <= minimumPosition && !deathToggled){
            deathToggled = true;
            StartCoroutine("DeathSequence");
        }

       if(transform.position.y <= destroyPosition && canKill){
            SceneManager.LoadScene(thisScene);
        }
    }

    public void RemoveLife(int lifeToRemove){
        if (canRemoveLife) {
            StartCoroutine("LifeTimer");
            health -= lifeToRemove;
        }
    }

    public void AddLife(int lifeToAdd){
        health += lifeToAdd;
    }

    IEnumerator LifeTimer(){
        canRemoveLife = false;
        yield return new WaitForSeconds(lifeTime);
        canRemoveLife = true;
    }

    IEnumerator DeathSequence(){
        yield return new WaitForSeconds(0.1f);
        playerMovementScript.enabled = false;
        colliderScript.isTrigger = true;
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        rigid.AddForce(new Vector2(0, deathForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        canKill = true;
    }
}