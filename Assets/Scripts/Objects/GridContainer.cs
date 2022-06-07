using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridContainer : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int gridSize;

    protected Vector2 cellSize = new Vector2(6, 4);
    protected Vector2 cellSpacing = new Vector2(.1f, .1f);

    protected virtual void Start() {
        GridsManager.instance.AddGrid(this);
    }

    void Update() {
        
    }

    public abstract void MoveCard(Vector2Int startGridPosition, Vector2Int endGridPosition);
    public abstract void AddCard(Card card);
    public abstract void AddCard(Card card, Vector2Int gridPosition);
    public abstract void RemoveCard(Vector2Int gridPosition, Card card);

    public void Enable() {
        GridsManager.instance.AddGrid(this);
        gameObject.SetActive(true);
    }

    public void Disable() {
        GridsManager.instance.RemoveGrid(this);
        gameObject.SetActive(false);
    }

    public Vector2Int GetGridPosition(Vector2 worldPosition) {
        var totalCellSize = GetTotalCellSize();
        var x = (int)Math.Round((worldPosition.x - transform.position.x) / totalCellSize.x, 0);
        var y = (int)Math.Round((worldPosition.y - transform.position.y) / totalCellSize.y, 0);

        return new Vector2Int(x, y);
    }

    public Vector3 GetLocalPosition(Vector2Int gridPosition) {
        var x = gridPosition.x * (cellSize.x + cellSpacing.x);
        var y = gridPosition.y * (cellSize.y + cellSpacing.y);

        return new Vector3(x, y, 0);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition) {
        var totalCellSize = GetTotalCellSize();
        var x = gridPosition.x * totalCellSize.x + transform.position.x;
        var y = gridPosition.y * totalCellSize.y + transform.position.y;

        return new Vector3(x, y, 0);
    }

    public bool IsValidGridPosition(Vector2Int gridPosition) {
        return gridPosition.x < gridSize.x && gridPosition.x >= 0 && gridPosition.y < gridSize.y && gridPosition.y >= 0;
    }

    public bool IsInsideGrid(Vector3 worldPosition) {
        var gridPosition = GetGridPosition(worldPosition);
        return gridPosition.x < gridSize.x && gridPosition.x >= 0 && gridPosition.y < gridSize.y && gridPosition.y >= 0;
    }

    Vector2 GetTotalCellSize() {
        var sizeX = (cellSize.x * transform.lossyScale.x) + cellSpacing.x;
        var sizeY = (cellSize.y * transform.lossyScale.y) + cellSpacing.y;
        return new Vector2(sizeX, sizeY);
    }
}
