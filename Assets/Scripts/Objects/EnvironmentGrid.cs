using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGrid : GridContainer
{
    [SerializeField]
    Card cardPrefab;
    [SerializeField]
    GridCell gridCellPrefab;

    Transform cardsContainer;
    Transform cellsContainer;
    Card[,] cards;
    GridCell[,] gridCells;
    bool initialized;
    public Biome biome { get; private set; }

    void Awake() {
        cardsContainer = new GameObject("Cards").transform;
        cardsContainer.SetParent(transform, false);
        cardsContainer.localPosition = new Vector3Int(0, 0, -1);

        cellsContainer = new GameObject("Cells").transform;
        cellsContainer.SetParent(transform, false);

        cards = new Card[gridSize.x, gridSize.y];
        gridCells = new GridCell[gridSize.x, gridSize.y];
    }

    protected override void Start()
    {
        base.Start();
        InitializeGrid();
    }

    void Update()
    {
        
    }

    void InitializeGrid() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                var position = GetLocalPosition(new Vector2Int(x, y));

                GridCell cell = Instantiate(gridCellPrefab, position, Quaternion.identity);
                cell.transform.SetParent(cellsContainer, false);
                cell.transform.localScale = new Vector3(cellSize.x, cellSize.y, 1);
                cell.name = $"cell ({x}, {y})";

                if (biome)
                    cell.SetColor(biome.color);
                else 
                    cell.SetColor(Color.gray);

                gridCells[x, y] = cell;
            }
        }

        initialized = true;
    }

    public override void AddCard(Card card) {
        var position = GetEmptyPosition();
        card.transform.SetParent(cardsContainer);
        SetCard(card, position);
    }

    public override void AddCard(Card card, Vector2Int gridPosition) {
        if (!IsValidGridPosition(gridPosition)) return;

        var cardAlreadyInPlace = cards[gridPosition.x, gridPosition.y];
        if (cardAlreadyInPlace) {
            CombineCards(card, cardAlreadyInPlace);
            return;
        } else {
            card.transform.SetParent(cardsContainer);
            SetCard(card, gridPosition);
        }
    }

    public void SetCard(Card card, Vector2Int gridPosition) {
        cards[gridPosition.x, gridPosition.y] = card;
        card.SetGridPosition(gridPosition, this);
    }

    public override void RemoveCard(Vector2Int gridPosition, Card card) {
        if (cards[gridPosition.x, gridPosition.y] == card)
            cards[gridPosition.x, gridPosition.y] = null;
    }

    Vector2Int GetEmptyPosition() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                if (!cards[x, y]) {
                    return new Vector2Int(x, y);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public override void MoveCard(Vector2Int startGridPosition, Vector2Int endGridPosition) {
        if (!IsValidGridPosition(endGridPosition)) return;

        var card1 = cards[startGridPosition.x, startGridPosition.y];
        var card2 = cards[endGridPosition.x, endGridPosition.y];

        if (card2 && card2 != card1) {
            CombineCards(card1, card2);
            return;
        }

        cards[startGridPosition.x, startGridPosition.y] = null;
        cards[endGridPosition.x, endGridPosition.y] = card1;

        card1.SetGridPosition(endGridPosition, this);
    }

    void CombineCards(Card movingCard, Card staticCard) {
        movingCard.Combine(staticCard);

    }

    public void SetBiome(Biome biome) {
        this.biome = biome;

        if (initialized) {
            for (int x = 0; x < gridSize.x; x++) {
                for (int y = 0; y < gridSize.y; y++) {
                    Debug.Log(gridCells[x, y]);
                    gridCells[x, y].SetColor(biome.color);
                }
            }
        }
    }
}
