using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField]
    private List<Tile> tiles;

    private Dictionary<int, Tile> tileDictionary;
    // Start is called before the first frame update

    private void Awake()
    {
        tileDictionary = new Dictionary<int, Tile>();
        
        foreach(Tile tile in tiles)
        {
            tileDictionary.Add(tile.id, tile);
        }    
    }

    public Tile CreateNewTile(int id)
    {
        Tile newTile;

        if (tileDictionary.TryGetValue(id, out newTile))
        {
            return Instantiate(newTile);
        }
        else
        {
            throw new System.Exception("No existe el Tile");
        }
    }
}
