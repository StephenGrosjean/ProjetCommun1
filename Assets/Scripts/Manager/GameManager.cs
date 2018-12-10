using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject UI_ManagerObject, player;

    [SerializeField] private int score;
    [SerializeField] private int lifes;
    [SerializeField] private int health;
    [SerializeField] private PosHolder positionHolder;
    [SerializeField] private List<Transform> spawnPos;

    [SerializeField] private string level1, level2;
    [SerializeField] AudioClip normalClip, caveClip;

    private UI_Manager ui_ManagerScript;
    private PlayerHealth playerHealthScript;
    private AudioSource audioSourceComponent;


    private bool canRemoveLife = true;
    private const float HealthTime = 0.1f;
    private const int maxHealth = 6;
    private static GameManager instance;

    private string currentScene;

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
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Scene_GameOver" || currentScene == "Scene_Final")
        {
            Destroy(gameObject);
        }

        audioSourceComponent = GetComponent<AudioSource>();
        audioSourceComponent.Play();
        
        GameObject posHolderGameObject = GameObject.FindGameObjectWithTag("PosHolder");

        player = GameObject.FindGameObjectWithTag("Player");
        UI_ManagerObject = GameObject.FindGameObjectWithTag("UI_Manager");

        ui_ManagerScript = UI_ManagerObject.GetComponent<UI_Manager>();
        playerHealthScript = player.GetComponent<PlayerHealth>();

        //Initialize UI values
        ui_ManagerScript.Lifes = lifes;
        ui_ManagerScript.Score = score;
        ui_ManagerScript.Health = health;
        playerHealthScript.Health = health;
        playerHealthScript.Life = lifes;

        if (posHolderGameObject != null)
        {
            positionHolder = posHolderGameObject.GetComponent<PosHolder>();

            Invoke("SpawnPos", 0);
        }
    }

    void Update () {
        ui_ManagerScript.Lifes = lifes;
        ui_ManagerScript.Score = score;
        ui_ManagerScript.Health = health;
        playerHealthScript.Health = health;
        playerHealthScript.Life = lifes;

        ui_ManagerScript.UpdateHealthUI();
        ui_ManagerScript.UpdateUI();

        if(currentScene == level1 || currentScene == level2)
        {
            
        }
        else
        {
            audioSourceComponent.Stop();

        }
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

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }


    //Timer to remove life
    IEnumerator HealthTimer()
    {
        canRemoveLife = false;
        yield return new WaitForSeconds(HealthTime);
        canRemoveLife = true;
    }

    void SpawnPos()
    {
        int spawnID = PlayerPrefs.GetInt("NextSpawnPos");

        player.transform.position = positionHolder.Pos[spawnID].position;

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
