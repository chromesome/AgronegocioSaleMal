using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int IN_GAME = 1;
    public const int ENDGAME = 3;

    public static GameManager instance;

    public double worldHealth;
    public int level;
    public int money;
    public int kills;
    public float timeLimit = 90f;

    public BoardView boardView;
    public JsonReader jsonReader;

    public ActionManager actionManager;

    [SerializeField]
    private List<TextAsset> mapTextAssets;
    private Dictionary<int, MapInfo> mapDictionary;

    float currentTime;
    bool timeStarted = false;

    public Text moneyText;
    public Text timeText;

    private Tile tileSelected;
    public Tile SelectedTile
    {
        get => tileSelected;
        set
        {
            if(tileSelected != null)
            {
                tileSelected.Unselect();
            }
            tileSelected = value;
            OnTileUpdated();
        }
    }

    void Awake()
    {
        // Esto lo hacemos para que la instancia de GameManager este a lo largo de todo el juego.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Almacenamos todos los niveles en un diccionario
        mapDictionary = new Dictionary<int, MapInfo>();

        foreach (TextAsset map in mapTextAssets)
        {
            MapInfo mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>(map.text);
            int mapInfoLevel = mapInfo.level;
            mapDictionary.Add(mapInfoLevel, mapInfo);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<UnityEngine.UI.Text>();
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<UnityEngine.UI.Text>();

        // Empezamos siempre en level 0
        level = 0;
        SetupMap();

        SetTimerCountdown();
    }

    private void SetTimerCountdown()
    {
        currentTime = timeLimit;
        timeStarted = true;
    }

    private void Update()
    {
        if(timeStarted)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                WinLevel();
            }
        }
    }

    private void OnGUI()
    {
        RefreshTimerText();

        // Debug GUI
        //if (tileSelected != null)
        //    GUI.TextArea(new Rect(10, 40, 100, 150), tileSelected.GetDetails());
    }

    private void RefreshTimerText()
    {
        if (timeText != null)
        {
            float minutes = Mathf.Floor(currentTime / 60);
            float seconds = Mathf.RoundToInt(currentTime % 60);
            string strMinutes = minutes > 9 ? minutes.ToString() : "0" + minutes.ToString();
            string strSeconds = seconds > 9 ? seconds.ToString() : "0" + seconds.ToString();

            timeText.text = strMinutes + ":" + strSeconds;
        }
    }

    // Llamamos a este método cada vez que queremos inicializar un nivel
    void SetupMap()
    {
        if (timeText == null)
            timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<UnityEngine.UI.Text>();

        // HACK
        if (moneyText == null)
            moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<UnityEngine.UI.Text>();
            
        // OTRO HACK
        gameObject.GetComponent<AudioManager>().CheckForNewFires();

        MapInfo mapInfo;

        

        MapInfo mapInfo;
        if (mapDictionary.TryGetValue(level, out mapInfo))
        {
            boardView.SetupBoard(mapInfo);
        }
        else
        {
            throw new System.Exception("Mapa no reconocido");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Trato todavía de entender porque a pesar de destruir el gameobject me sigue cargando este método, en consecuencia necesito discernir por el scene name
        if(scene.name == "Main")
            SetupMap();
    }

    public void WinLevel()
    {
        level++;
        if (level < mapDictionary.Count)
        {
            SceneManager.LoadScene(IN_GAME);
            timeStarted = false;
            SetTimerCountdown();
        }
        else 
        {
            WinGame();
        }
    }

    public void LoseGame()
    {
        // TODO: Hacer que aca pierda
    }

    public void WinGame()
    {
        // TODO: Hacer que acá ""gane"" (?
        Debug.Log("Felicitaciones kpo, hiciste concha todo, ahora fijate si te podes comer lo' dolare (?");
        SceneManager.LoadScene(ENDGAME);
    }

    public void OnTileUpdated()
    {
        if (SelectedTile != null)
        {
            DisplayActions();
        }
    }

    private void DisplayActions()
    {
        List<ActionItem> actions = SelectedTile.GetActions();
        actionManager.InstantiateActions(actions);
    }

    private void ActionBuild(int actorId)
    {
        ActorFactory actorFactory = this.GetComponent<ActorFactory>();
        Actor actor = actorFactory.CreateNewActor(actorId); // 0 pertenece a Factory, hacer un enum con esto o una const
        SelectedTile.Actor = actor;
        SelectedTile.SetupActions();
        actionManager.InstantiateActions(SelectedTile.GetActions());
    }

    private void ActionBuildFarm()
    {
        ActionBuild(0); // 0 pertenece a Factory, hacer un enum con esto o una const
        money -= 60; // Esto tiene que estar definido en otra parte
    }

    private void ActionBuildFactory()
    {
        ActionBuild(1); // 1 pertenece a Farm, hacer un enum con esto o una const
        money -= 100; // Esto tiene que estar definido en otra parte
    }

    private void ActionFire()
    {
        if(!SelectedTile.IsOnFire())
        {
            ActorFactory actorFactory = this.GetComponent<ActorFactory>();
            SelectedTile.Fire = actorFactory.CreateNewActor(3) as Fire;
            SelectedTile.SetupActions();
            actionManager.InstantiateActions(SelectedTile.GetActions());
            GetComponent<AudioManager>().CheckForNewFires();
        }
    }

    private void ActionDeforest()
    {
        Tree tree = SelectedTile.Actor as Tree;
        if (tree != null)
        {
            tree.Chop();
            SelectedTile.SetupActions();
            actionManager.InstantiateActions(SelectedTile.GetActions());
        }
    }

    private void ActionMakeMoney()
    {
        IMakeMoney makeMoneyActor = SelectedTile.Actor as IMakeMoney;
        if (makeMoneyActor != null)
        {
            makeMoneyActor.MakeMoney();
        }
        else
        {
            throw new System.Exception("Interfaz makemoney no encontrada en actor");
        }
    }

    private void ActionMitigate()
    {
        Fire fire = SelectedTile.Fire;
        if(fire != null)
        {
            fire.Mitigate();
            if (SelectedTile.Fire == null)
            {
                SelectedTile.SetupActions();
                actionManager.InstantiateActions(SelectedTile.GetActions());
            }
        }
    }

    private void ActionUpgrade()
    {
        IMakeMoney makeMoneyActor = SelectedTile.Actor as IMakeMoney;
        if (makeMoneyActor != null)
        {
            makeMoneyActor.Upgradeable();
            actionManager.InstantiateActions(SelectedTile.GetActions());
        }
        else
        {
            throw new System.Exception("Interfaz makemoney no encontrada en actor");
        }
    }
}

