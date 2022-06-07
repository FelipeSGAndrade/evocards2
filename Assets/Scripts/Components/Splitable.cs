using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card))]
[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(Collider2D))]
public class Splitable : MonoBehaviour
{
    Card card;
    Draggable draggable;

    void Awake() {
        card = GetComponent<Card>();
        draggable = GetComponent<Draggable>();
    }

    void Start() {
        
    }

    void Update() {
        
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            var newCard = CardManager.instance.SplitCard(card);
            if (!newCard) return;

            var newCardDraggable = newCard.GetComponent<Draggable>();
            newCardDraggable.StartDrag(1);
        }
    }
}
