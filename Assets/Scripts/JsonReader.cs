using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;

    private void Start()
    {
        //JsonUtility.FromJson<Ground>(jsonFile.text);
        Ground ground = JsonConvert.DeserializeObject<Ground>(jsonFile.text);

        Debug.Log("ground size " + ground.tileMap != null ? "success" : "fail");
    }

}


public class Ground
{
    public List<GroundRow> tileMap;
}

public class GroundRow
{
    public int row;
    public List<GroundTile> tiles;
}

public class GroundTile
{
    public int tileType;
    public double tileResist;
    public int actorType;
}
