using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    Card cardPrefab;

    public static CardManager instance;

    void Awake() {
        if (instance)
            throw new Exception("Cant have more than one Card Manager in Scene");

        instance = this;
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public Card SplitCard(Card card) {
        var cardData = card.GetCardData();
        if (!cardData.handRemove) return null;

        var newCard = Instantiate(cardPrefab, card.transform.position, Quaternion.identity);
        newCard.SetCardData(cardData.handRemove);

        if (cardData.handStay)
            card.SetCardData(cardData.handStay);
        else
            card.RemoveFromGrid(card.currentGrid);

        return newCard.GetComponent<Card>();
    }
}
