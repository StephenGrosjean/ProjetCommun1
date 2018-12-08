using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private GameObject magnet;
    [SerializeField] private Transform stuckPosition;
    [SerializeField] private GameObject lockedObject;
    [SerializeField] private Transform throwPositionLeft, throwPositionRight;
    [SerializeField] private int throwForce;


    private bool locked;
    private SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (locked)
        {
            lockedObject.transform.position = stuckPosition.position;
            Invoke("CancelCatch", 0);
        }

        if (Input.GetButtonDown("Fire1") && locked)
        {
            Invoke("Release", 0);
        }
        else if(Input.GetButtonDown("Fire1") && !locked)
        {
            Invoke("Catch", 0);
        }else if(Input.GetButtonUp("Fire1") && !locked)
        {
            Invoke("CancelCatch", 0);
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
           
        }
    }

    void Catch()
    {
        magnet.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = !locked;
    }

    void CancelCatch()
    {
        magnet.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    void Release()
    {
        Rigidbody2D lockedObjectRigid = lockedObject.GetComponent<Rigidbody2D>();


        lockedObject.GetComponent<EnemyController>().enabled = true;
        lockedObjectRigid.constraints = RigidbodyConstraints2D.None;
        lockedObjectRigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        lockedObjectRigid.gravityScale = 0;

        lockedObject.layer = 12;
        locked = false;

        if (playerSpriteRenderer.flipX)
        {
            lockedObject.transform.position = throwPositionLeft.position;
            lockedObjectRigid.velocity = Vector2.left * throwForce;
        }
        else
        {
            lockedObject.transform.position = throwPositionRight.position;
            lockedObjectRigid.velocity = Vector2.right * throwForce;
        }

        lockedObject.GetComponent<ThrowMode>().IsThrowed = true;
        lockedObject = null;

        




    }
}
