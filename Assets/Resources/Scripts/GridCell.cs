using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Tile tile;
    public int xPos;
    public int yPos;

    public List<GridCell> neighbors;

    public GridCell()
    {
    }

    public Tile GetGridCellTileType()
    {
        return tile;
    }
}