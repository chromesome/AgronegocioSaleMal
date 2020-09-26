using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Tile tile;
    public Actor actor;
    public int xPos;
    public int yPos;

    public GridCell()
    {
    }

    public Tile GetGridCellTileType()
    {
        return tile;
    }

    public Actor GetGridCellActorType()
    {
        return actor;
    }
}