using System.Collections.Generic;
using UnityEngine;

public class Board
{
    //public int columns = 5;
    //public int rows = 5;

    private GridCell[][] gridCells;

    public Board()
    {

    }

    public void GenerateMap(MapInfo mapInfo)
    {
        gridCells = SetupGridCellsArray(gridCells, mapInfo);

        //Debug.Log(Debug_PrintMap());
    }

    GridCell[][] SetupGridCellsArray(GridCell[][] t, MapInfo mapInfo)
    {
        int columns = mapInfo.tileMap.Count;
        int rows = mapInfo.tileMap[0].tiles.Count;
        t = new GridCell[columns][];

        /*for (int i = 0; i < t.Length; i++)
        {
            t[i] = new GridCell[rows];
            for (int j = 0; j < t[i].Length; j++)
            {
                t[i][j] = new GridCell(0, 0, 100);
            }
        }
        foreach (MapRow mapRow  in mapInfo.tileMap)
        {
            foreach (MapTile mapTile in mapRow.tiles)
            {

            }
        }*/

        for (int x = 0; x < mapInfo.tileMap.Count; x++)
        {
            MapRow mapRow = mapInfo.tileMap[x];
            t[x] = new GridCell[rows];
            for (int y = 0; y < mapRow.tiles.Count; y++)
            {
                MapTile mapTile = mapRow.tiles[y];

                int tileType = mapTile.tileType;
                int actorType = mapTile.actorType;
                double tileResist = mapTile.tileResist;
                t[x][y] = new GridCell(tileType, actorType, tileResist);
            }
        }
        return t;
    }

    public GridCell[][] GetGridCellsData()
    {
        return gridCells;
    }

    /*public string Debug_PrintMap()
    {
        string map = "";
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                map += Debug_InstantiateChar(i, j);
            }
            map += "\n";
        }
        return map;
    }

    string Debug_InstantiateChar(int x, int y)
    {
        if (gridCells[x][y].GetGridCellTileType() == 0)
            return "0";
        return "#";
    }*/
}
