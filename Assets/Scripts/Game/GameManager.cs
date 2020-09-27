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
    private Dictionary<int, TextAsset> mapDictionary;


    void Awake()
    {
        // Esto lo hacemos para que la instancia de GameManager este a lo largo de todo el juego.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Almacenamos todos los niveles en un diccionario
        mapDictionary = new Dictionary<int, TextAsset>();

        foreach (TextAsset map in mapTextAssets)
        {
            MapInfo mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>(map.text);
            int mapInfoLevel = mapInfo.level;
            mapDictionary.Add(mapInfoLevel, map);
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
        TextAsset mapTextAsset;
        MapInfo mapInfo;

        if (mapDictionary.TryGetValue(level, out mapTextAsset))
        {
            string mapText = mapTextAsset.text;
            mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>(mapText);
        }
        else
        {
            throw new System.Exception("Mapa no reconocido");
        }

        if (mapInfo != null)
        {
            boardView.SetupBoard(mapInfo);
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
        Destroy(gameObject);
        SceneManager.LoadScene(ENDGAME);
    }
}
