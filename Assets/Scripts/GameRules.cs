using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BalistaTuning))]
[RequireComponent(typeof(InputController))]
public class GameRules : MonoBehaviour
{

    private static GameRules instance;
    public static GameRules Instance
    {
        get
        {
            return instance;
        }
    }
    //DESIGNERS EDITABLE
    public GameObject hiderEffect;

    //USABLE FROM OTHER CLASSES
    [HideInInspector]
    public int currentStep;
    [HideInInspector]
    public int turn = 1;
    [HideInInspector]
    public int nPlayer = 2;//improve more players?
    [HideInInspector]
    public int currentPlayer;
    [HideInInspector]
    public CameraMovement mainCamera;
    [HideInInspector]
    public Animator targetAni;
    [HideInInspector]
    public WindHandler wind;
    [HideInInspector]
    public float hiderEffectDuration;
    [HideInInspector]
    public double[] score;
    [HideInInspector]
    public GameplayBehaviour[] allBehaviours;
    [HideInInspector]
    public Herrow[] herrows;
    [HideInInspector]
    public ObstacleBehaviour[] obstacles;
    [HideInInspector]
    public Level lvlP1;
    [HideInInspector]
    public GameObject scene;

    //PRIVATE VARIABLES
    private Player[] players;

    private string sceneName;

    private int nStep;



    public AnimationCurve herrowLevel;
    public AnimationCurve basePoints;
    public AnimationCurve stepMultiplier;


    private void Awake()
    {
        //init Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of GameRules");
        }

        DontDestroyOnLoad(gameObject);

        //init vars
        currentStep = 1;
        players = new Player[nPlayer];
        score = new double[nPlayer];
        for (int i = 0; i < nPlayer; i++)
        {
            score[i] = new double();
            players[i] = new Player();// prendere da pre partita
        }
        currentPlayer = 0;
        nStep = 0;
        mainCamera = Camera.main.GetComponent<CameraMovement>();
        hiderEffectDuration = 1.85f;
       


        //get all Herrows
        herrows = Resources.LoadAll<Herrow>("");



        //get Level to Duplicate
        lvlP1 = FindObjectOfType(typeof(Level)) as Level;
        scene = lvlP1.gameObject;
        sceneName = scene.name;
    }

    private void Start()
    {
        //instantiate player2 scene
        scene = Instantiate(scene);
        scene.name = sceneName + "P2";
        scene.transform.position = new Vector3(scene.transform.position.x + mainCamera.horizontalDiff, scene.transform.position.y, scene.transform.position.z);

        //disable all gameplay behaviour and calculate nStep
        allBehaviours = FindObjectsOfType<GameplayBehaviour>();
        foreach (GameplayBehaviour behaviour in allBehaviours)
        {
            if (behaviour != null)
            {
                if (behaviour.ToString().Contains("PlayerInteractible"))
                {
                    nStep++;
                }
                if (!behaviour.ToString().Contains("quiver"))
                {
                    behaviour.enabled = false;
                }
            }
        }
        nStep /= nPlayer;

        obstacles = GameObject.FindObjectsOfType<ObstacleBehaviour>();

        //init Wind
        if (wind != null) wind.AlterWind();

    }



    public void EndMatch(float waitForDisplayEnd)
    {
        //handle winner and looser


        StartCoroutine(Win(waitForDisplayEnd));
    }

    public IEnumerator Win(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Victory");
    }

    public bool AllPlayersPlayedStep()
    {
        if (turn == nPlayer)
        {
            return true;
        }
        return false;
    }

    public bool IsNotLastStep()
    {
        if (currentStep < nStep)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerOne()
    {
        if (currentPlayer == 0)
        {
            return true;
        }
        return false;
    }

    private void ChangeStep()
    {
        mainCamera.moveCamera = mainCamera.Up;
        currentStep++;
        wind.AlterWind();
        turn = 1;
        TextAppear.Instance.ShowText("Step " + currentStep + " begins! \n Mult. " + stepMultiplier.Evaluate(currentStep), 4);
    }

    public void ChangeTurn(float waitForSeconds)
    {
        Invoke("ChangeTurn", waitForSeconds);
    }

    public void ChangeTurn()
    {
        if (AllPlayersPlayedStep())
        {
            if (IsNotLastStep())
            {
                ChangeStep();
            }
        }
        else
        {
            if (IsPlayerOne())
            {
                mainCamera.moveCamera = mainCamera.Right;
                currentPlayer++;
                TextAppear.Instance.ShowText("Player 2, \n It's Your Turn!", 2);

            }
            else
            {
                mainCamera.moveCamera = mainCamera.Left;
                currentPlayer--;
                TextAppear.Instance.ShowText("Player 1, \n It's Your Turn!", 2);
            }
            turn++;
        }
        Timer.Instance.Reset();
        DeactivateQuiverButton.Instance.ResetState();

    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Victory")
        {
            Destroy(gameObject);
        }
    }









}
