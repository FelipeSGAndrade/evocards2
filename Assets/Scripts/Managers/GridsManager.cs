using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridsManager : MonoBehaviour
{
    public static GridsManager instance;

    List<GridContainer> gridsInScene = new List<GridContainer>();

    void Awake() {
        if (instance)
            throw new Exception("Cant have more than one Grid Manager in Scene");
        
        instance = this;
    }

    public void AddGrid(GridContainer grid) {
        if (!gridsInScene.Contains(grid))
            gridsInScene.Add(grid);
    }

    public void RemoveGrid(GridContainer grid) {
        gridsInScene.Remove(grid);
    }

    public GridContainer[] FindGridContainers(Vector3 worldPosition) {
        List<GridContainer> foundGrids = new List<GridContainer>();

        foreach (GridContainer grid in gridsInScene) {
            if (grid.IsInsideGrid(worldPosition))
                foundGrids.Add(grid);
        }

        return foundGrids.ToArray();
    }
}
