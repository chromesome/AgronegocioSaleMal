using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int IN_GAME = 1;
    public const int ENDGAME = 3;

    public static GameManager instance;

    public double worldHealth;
    public int level;
    public int money;

    public BoardView boardView;
    public JsonReader jsonReader;

    [SerializeField]
    private List<TextAsset> mapTextAssets;
    private Dictionary<int, MapInfo> mapDictionary;

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

        // Empezamos siempre en level 0
        level = 0;
        SetupMap();
    }

    // Llamamos a este método cada vez que queremos inicializar un nivel
    void SetupMap()
    {
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


    // Debug GUI
    private void OnGUI()
    {
        if(tileSelected != null)
            GUI.TextArea(new Rect(10, 10, 100, 150), tileSelected.GetDetails());
    }
}

