using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;

    private void Start()
    {
        //GridCell gridCellInfo = JsonConvert.DeserializeObject<GridCell>(jsonFile.text);
        

        //Debug.Log(gridCellInfo);
        MapInfo mapInfo = GetMapinfoFromJSON<MapInfo>();

    }

    public MapInfo GetMapinfoFromJSON<MapInfo>()
    {
        return JsonConvert.DeserializeObject<MapInfo>(jsonFile.text);
    }

}


public class MapInfo
{
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
