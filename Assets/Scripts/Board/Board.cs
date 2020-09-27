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

        SetNeighbors(t);
        return t;
    }

    // Lo más cabeza, habra una forma mejor?
    private void SetNeighbors(GridCell[][] t)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GridCell gridCell = t[x][y];
                gridCell.neighbors = new List<GridCell>();

                // look left
                if(x > 0)
                    gridCell.neighbors.Add(t[x - 1][y]);

                // look right
                if (x < columns-1)
                    gridCell.neighbors.Add(t[x + 1][y]);

                // look up
                if (y > 0)
                    gridCell.neighbors.Add(t[x][y - 1]);

                // look down
                if (y < rows-1)
                    gridCell.neighbors.Add(t[x][y + 1]);
            }
        }
    }

    public GridCell[][] GetGridCellsData()
    {
        return gridCells;
    }
}
