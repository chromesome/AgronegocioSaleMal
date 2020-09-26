using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GridCell[][] gridCells;

    public void GenerateMap(MapInfo mapInfo)
    {
        gridCells = SetupGridCellsArray(gridCells, mapInfo);
    }

    GridCell[][] SetupGridCellsArray(GridCell[][] t, MapInfo mapInfo)
    {
        int columns = mapInfo.tileMap.Count;
        int rows = mapInfo.tileMap[0].tiles.Count;
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
