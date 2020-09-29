using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GridCell[][] gridCells;
    private int columns;
    private int rows;

    public void GenerateMap(MapInfo mapInfo)
    {
        gridCells = SetupGridCellsArray(gridCells, mapInfo);
    }

    GridCell[][] SetupGridCellsArray(GridCell[][] t, MapInfo mapInfo)
    {
        columns = mapInfo.tileMap.Count;
        rows = mapInfo.tileMap[0].tiles.Count;
        t = new GridCell[columns][];

        for (int x = 0; x < columns; x++)
        {
            t[x] = new GridCell[rows];
            for (int y = 0; y < rows; y++)
            {
                t[x][y] = new GridCell();
            }
        }

        return t;
    }

    public GridCell[][] GetGridCellsData()
    {
        return gridCells;
    }
}
