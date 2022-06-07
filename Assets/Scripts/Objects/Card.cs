using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    CardData cardData;

    [SerializeField]
    TextMeshPro titleText;

    [SerializeField]
    SpriteBox spriteBox;

    public Vector2Int gridPosition { get; private set; }
    Vector3 desiredLocalPosition;
    bool dragging;
    bool inPosition = false;
    float moveSpeed = 50;
    bool setInEditor;
    Vector3 fixedLocalScale;

    public GridContainer currentGrid { get; private set; }

    void Awake() {
        if (cardData) setInEditor = true;
        fixedLocalScale = transform.localScale;
    }

    void Start() {
        if (setInEditor) {
            UpdateDisplay();
            desiredLocalPosition = transform.position;
        }
    }

    void Update() {
        if (!dragging && !inPosition) UpdatePosition();
    }

    void UpdatePosition() {
        var difference = desiredLocalPosition - transform.localPosition;
        transform.localScale = fixedLocalScale;

        if (difference.magnitude < .2) {
            transform.localPosition = desiredLocalPosition;
            inPosition = true;
        } else {
            transform.Translate(difference.normalized * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetCardData(CardData cardData) {
        this.cardData = cardData;
        Tracker.instance.AddDiscoveredCard(cardData);
        UpdateDisplay();
    }

    public CardData GetCardData() {
        return cardData;
    }

    public void SetDesiredPosition(Vector3 desiredPosition) {
        this.desiredLocalPosition = desiredPosition;
        inPosition = false;
    }

    public void SetGridPosition(Vector2Int gridPosition, GridContainer grid) {
        if (currentGrid && currentGrid != grid)
            currentGrid.RemoveCard(this.gridPosition, this);

        this.gridPosition = gridPosition;
        currentGrid = grid;
        SetDesiredPosition(currentGrid.GetLocalPosition(gridPosition));
    }

    void UpdateDisplay() {
        name = $"Card ({cardData.title})";
        titleText.text = cardData.title;

        if (cardData.sprite)
            spriteBox.SetSprite(cardData.sprite);
    }

    public void DragStarted() {
        dragging = true;
    }

    public void DragEnded(bool hasPositionChanged) {
        dragging = false;
        inPosition = false;

        if (hasPositionChanged) {
            Move(MouseUtils.GetMousePosition());
            return;
        }
    }

    void Move(Vector3 destinationWorldPosition) {
        var possibleGrids = GridsManager.instance.FindGridContainers(destinationWorldPosition);

        GridContainer targetGrid = null;
        foreach (GridContainer grid in possibleGrids) {
            if (targetGrid)
                Debug.Log(targetGrid.transform.position + " " + currentGrid.transform.position);
            if (!targetGrid || targetGrid.transform.position.z > currentGrid.transform.position.z)
                targetGrid = grid;
        }

        if (targetGrid) {
            var destinationGridPosition = targetGrid.GetGridPosition(destinationWorldPosition);

            if (targetGrid == currentGrid) {
                targetGrid.MoveCard(gridPosition, destinationGridPosition);
            } else {
                targetGrid.AddCard(this, destinationGridPosition);
            }
        }
    }

    public void RemoveFromGrid(GridContainer grid) {
        if (currentGrid == grid) Destroy(gameObject);
    }

    public void Combine(Card otherCard) {
        var recipe = RecipeManager.instance.GetCombinationRecipe(cardData, otherCard.cardData);
        if (!recipe) return;

        var shouldKeepThisCard = recipe.ShouldKeepCard(cardData);
        var shouldKeepOtherCard = recipe.ShouldKeepCard(otherCard.cardData);

        if (shouldKeepOtherCard) {
            if (shouldKeepThisCard) throw new Exception("Cant keep both cards");
            if (currentGrid != otherCard.currentGrid) return;

            SetCardData(recipe.result);
        } else {
            otherCard.SetCardData(recipe.result);

            if (!shouldKeepThisCard) {
                currentGrid.RemoveCard(gridPosition, this);
                Destroy(gameObject);
            }
        }
    }
}
