using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public int tileType = 0;
    public int actorType = 0;
    public double tileResist = 0f;

    public GridCell(int n_tileType, int n_actorTyle, double n_tileResist)
    {
        tileType = n_tileType;
        actorType = n_actorTyle;
        tileResist = n_tileResist;
    }

    public int GetGridCellTileType()
    {
        return tileType;
    }

    public int GetGridCellActorType()
    {
        return actorType;
    }

    public double GetGridCellTileResist()
    {
        return tileResist;
    }
}