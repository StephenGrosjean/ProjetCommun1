using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject UI_ManagerObject, player;

    [SerializeField] private int score;
    [SerializeField] private int lifes;
    [SerializeField] private int health;

    private UI_Manager ui_ManagerScript;
    private PlayerHealth playerHealthScript;


    private bool canRemoveLife = true;
    private const float HealthTime = 0.1f;
    private const int maxHealth = 6;
    private static GameManager instance;

    private void OnLevelWasLoaded(int level)
    {
       Init();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init(){
        

        player = GameObject.FindGameObjectWithTag("Player");
        UI_ManagerObject = GameObject.FindGameObjectWithTag("UI_Manager");

        ui_ManagerScript = UI_ManagerObject.GetComponent<UI_Manager>();
        playerHealthScript = player.GetComponent<PlayerHealth>();

        //Initialize UI values
        ui_ManagerScript.Lifes = lifes;
        ui_ManagerScript.Score = score;
        ui_ManagerScript.Health = health;
        playerHealthScript.Health = health;
    }

    void Update () {
        ui_ManagerScript.Lifes = lifes;
        ui_ManagerScript.Score = score;
        ui_ManagerScript.Health = health;
        playerHealthScript.Health = health;

        ui_ManagerScript.UpdateHealthUI();
        ui_ManagerScript.UpdateUI();
    }

    //Function to remove life
    public void RemoveHealth(int HealthToRemove)
    {
        if (canRemoveLife)
        {
            StartCoroutine("HealthTimer");
            health -= HealthToRemove;
            ui_ManagerScript.UpdateHealthUI();
        }
    }

    //Function to add life
    public void AddLife(int lifeToAdd)
    {
        health += lifeToAdd;
    }

    public void RemoveLife()
    {
        health = maxHealth;
        lifes--;
    }


    //Timer to remove life
    IEnumerator HealthTimer()
    {
        canRemoveLife = false;
        yield return new WaitForSeconds(HealthTime);
        canRemoveLife = true;
    }
}
