using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private float minimumPosition;
    [SerializeField] private float destroyPosition;
    [SerializeField] private float deathForceFall, deathForceLife;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private GameObject spriteObj;

    private int health = 1;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    private int life = 3;
    public int Life
    {
        set { life = value; }
    }
    private GameManager gameManager;

    private bool deathToggled;
    private string thisScene;
    private bool canKill;

    //Scripts
    private PlayerMovement playerMovementScript;
    private Rigidbody2D rigid;
    private Collider2D colliderScript;

    private void Start(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerMovementScript = GetComponent<PlayerMovement>();
        rigid = GetComponent<Rigidbody2D>();
        colliderScript = GetComponent<Collider2D>();
        thisScene = SceneManager.GetActiveScene().name;
    }

    private void Update(){
       if(health <= 0 && !deathToggled){
            deathToggled = true;
            StartCoroutine("DeathSequence", "Life");
        }

       if(transform.position.y <= minimumPosition && !deathToggled){
            deathToggled = true;
            StartCoroutine("DeathSequence", "Fall");
        }

       if(transform.position.y <= destroyPosition && canKill){
            SceneManager.LoadScene(thisScene);
        }

       if(life < 0)
        {
            SceneManager.LoadScene("Scene_GameOver");
        }
    }


    //Death sequence
    IEnumerator DeathSequence(string Type){
        playerMovementScript.enabled = false;
        colliderScript.isTrigger = true;
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;

        switch (Type)
        {
            case "Fall":
                rigid.AddForce(new Vector2(0, deathForceFall), ForceMode2D.Impulse);
                gameManager.RemoveHealth(Health);
                break;

            case "Life":
                rigid.AddForce(new Vector2(0, deathForceLife), ForceMode2D.Impulse);
                spriteObj.GetComponent<Animator>().SetBool("isJumping", true);
                break;
        }
        yield return new WaitForSeconds(1.5f);
        gameManager.RemoveLife();
        PlayerPrefs.DeleteAll();
        canKill = true;
    }
}
