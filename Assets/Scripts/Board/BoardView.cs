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
    public Board board;

    void Start()
    {
        MapInfo mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>();

        board.GenerateMap(mapInfo);
        cells = board.GetGridCellsData();

        InstantiateElements(mapInfo);
    }

    void InstantiateElements(MapInfo mapInfo)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            MapRow mapRow = mapInfo.tileMap[i];
            for (int j = 0; j < cells[i].Length; j++)
            {
                MapTile mapTile = mapRow.tiles[j];

                cells[i][j].tile = tileFactory.CreateNewTile(mapTile.tileType);
                cells[i][j].actor = actorFactory.CreateNewActor(mapTile.actorType);
                cells[i][j].xPos = i;
                cells[i][j].yPos = j;

                cells[i][j].tile.transform.position = new Vector3((float)i, (float)j, 0f);
                cells[i][j].actor.transform.position = new Vector3((float)i, (float)j, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
