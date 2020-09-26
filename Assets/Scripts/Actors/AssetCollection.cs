using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetCollection : MonoBehaviour
{

    [SerializeField]
    GameObject tile_groundAsset;
    [SerializeField]
    GameObject tile_waterlandAsset;

    [SerializeField]
    GameObject actor_noneAsset;
    [SerializeField]
    GameObject actor_baseAsset;
    [SerializeField]
    GameObject actor_factoryAsset;
    [SerializeField]
    GameObject actor_treeAsset;
    [SerializeField]
    GameObject actor_fireAsset;

    public Dictionary<int, GameObject> tileDictionary;
    public Dictionary<int, GameObject> actorDictionary;

    void Awake()
    {
        tileDictionary = new Dictionary<int, GameObject>();
        tileDictionary.Add(0, tile_groundAsset);
        tileDictionary.Add(1, tile_waterlandAsset);

        actorDictionary = new Dictionary<int, GameObject>();
        actorDictionary.Add(0, actor_noneAsset);
        actorDictionary.Add(1, actor_baseAsset);
        actorDictionary.Add(2, actor_factoryAsset);
        actorDictionary.Add(3, actor_treeAsset);
        actorDictionary.Add(4, actor_fireAsset);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetGameObjectForTile(int id)
    {
        return new GameObject();
    }
}
