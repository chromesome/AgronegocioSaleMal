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

    int columns;
    int rows;

    float tileWidth = 1f;
    float tileHeight = 1f;
    float gap = 0.0f;

    Vector3 startPosition;

    void Start()
    {
        MapInfo mapInfo = jsonReader.GetMapinfoFromJSON<MapInfo>();
        columns = mapInfo.tileMap.Count;
        rows = mapInfo.tileMap[0].tiles.Count;

        board.GenerateMap(mapInfo);
        cells = board.GetGridCellsData();

        AddGap();
        GetStartPosition();
        InstantiateElements(mapInfo);
    }

    void AddGap()
    {
        tileWidth += tileWidth * gap;
        tileHeight += tileHeight * gap;
    }

    void GetStartPosition()
    {
        // setear start position
        float x = 0;
        float y = (tileHeight /2) * (rows / 2);

        startPosition = new Vector3(x, y, 0);
    }

    private Vector3 CalculateWorldPosition(Vector2 gridPosition)
    {
        // TODO: Explicar este calculo de forma clara, ahora no me da
        float x = startPosition.x - gridPosition.x * tileWidth /2 + gridPosition.y * tileWidth / 2;
        float y = startPosition.y - gridPosition.y * tileHeight * 0.25f - gridPosition.x * tileHeight * 0.25f;

        return new Vector3(x, y, 0);
    }

    void InstantiateElements(MapInfo mapInfo)
    {
        for (int i = 0; i < mapInfo.tileMap.Count; i++)
        {
            MapRow mapRow = mapInfo.tileMap[i];


            for (int j = 0; j < mapRow.tiles.Count; j++)
            {
                MapTile mapTile = mapRow.tiles[j];

                GridCell cell = cells[i][j];

                cell.tile = tileFactory.CreateNewTile(mapTile.tileType);

                // TODO: Esto lo necesitamos?
                cell.xPos = i;
                cell.yPos = j;
                // -----------

                Vector2 gridPosition = new Vector2(i, j);
                Vector3 worldPosition = CalculateWorldPosition(gridPosition);
                cell.tile.transform.position = worldPosition;
                cell.tile.name = "tile" + i + "|" + j;

                // If has actor
                if (mapTile.actorType >= 0)
                {
                    cell.actor = actorFactory.CreateNewActor(mapTile.actorType);
                    cell.actor.transform.position = cell.tile.spawnPoint.transform.position;
                    cell.actor.name = "actor" + i + "|" + j;

                    cell.tile.actor = cell.actor;
                }
            }
        }

        SetNeighbors(cells);
    }

    // Lo más cabeza, habra una forma mejor?
    private void SetNeighbors(GridCell[][] t)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Tile tile = t[x][y].tile;
                tile.neighbors = new List<Tile>();

                // look left
                if (x > 0)
                    tile.neighbors.Add(t[x - 1][y].tile);

                // look right
                if (x < columns-1)
                    tile.neighbors.Add(t[x + 1][y].tile);

                // look up
                if (y > 0)
                    tile.neighbors.Add(t[x][y - 1].tile);

                // look down
                if (y < rows-1)
                    tile.neighbors.Add(t[x][y + 1].tile);
            }
        }
    }

}
