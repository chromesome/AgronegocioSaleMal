using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;

    public MapInfo GetMapinfoFromJSON<MapInfo>(string jsonFileText)
    {
        return JsonConvert.DeserializeObject<MapInfo>(jsonFileText);
    }

}


public class MapInfo
{
    public int level;
    public List<MapRow> tileMap;
}

public class MapRow
{
    public int row;
    public List<MapTile> tiles;
}

public class MapTile
{
    public int tileType;
    public double tileResist;
    public int actorType;
}
