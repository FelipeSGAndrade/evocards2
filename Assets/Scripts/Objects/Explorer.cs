using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explorer : GridContainer
{
    [SerializeField]
    GridCell pileCell;
    [SerializeField]
    GridCell spawnCell;
    [SerializeField]
    Card cardPrefab;

    Biome biome;
    Card cardInSpawn;

    protected override void Start() {
        base.Start();
        gridSize = new Vector2Int(2, 1);
    }

    public void PileClicked() {
        DrawCard();
    }

    public void SetBiome(Biome biome) {
        this.biome = biome;
        pileCell.SetColor(biome.color);
        spawnCell.SetColor(biome.color);
        ClearSpawn();
    }

    void ClearSpawn() {
        if (cardInSpawn) cardInSpawn.RemoveFromGrid(this);
    }

    void DrawCard() {
        var spentStamina = Tracker.instance.SpendStamina(1);
        if (!spentStamina) return;

        var cardDataIndex = Random.Range(0, biome.deck.Length);
        var cardData = biome.deck[cardDataIndex];

        ClearSpawn();

        cardInSpawn = Instantiate(cardPrefab, pileCell.transform.position, Quaternion.identity);
        cardInSpawn.transform.SetParent(transform);
        cardInSpawn.SetCardData(cardData);
        cardInSpawn.SetGridPosition(new Vector2Int(1, 0), this);
    }

    public override void RemoveCard(Vector2Int gridPosition, Card card) {
        if (card == cardInSpawn) cardInSpawn = null;
    }

    public override void MoveCard(Vector2Int startGridPosition, Vector2Int endGridPosition) {}
    public override void AddCard(Card card, Vector2Int gridPosition) {}
    public override void AddCard(Card card) {}

}
