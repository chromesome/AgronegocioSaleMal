using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    // Start is called before the first frame update
    Board map;

    private GridCell[][] cells;

    public TileFactory tileFactory;
    public ActorFactory actorFactory;

    public JsonReader jsonReader;

    void Start()
    {
        MapInfo mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>();
        map = new Board();
        map.GenerateMap(mapInfo);

        cells = map.GetGridCellsData();

        InstantiateTiles();
        InstantiateActors();
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
            {
                int tileId = cells[i][j].GetGridCellTileType();
                double tileResistance = cells[i][j].GetGridCellTileResist();
                Debug.Log("Titolog::tileId=" + tileId + "||tileResistance=" + tileResistance);
                tileFactory.CreateNewTile(tileId, tileResistance, (float)i, (float)j);
            }
        }
    }

    void InstantiateActors()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
            {
                int actorId = cells[i][j].GetGridCellActorType();
                Debug.Log("Titolog::actorId=" + actorId);
                actorFactory.CreateNewActor(actorId, (float)i, (float)j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
