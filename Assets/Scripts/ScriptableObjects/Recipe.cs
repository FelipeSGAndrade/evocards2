using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public CardData usedCard;
    public CardData destinationCard;
    public CardData result;
    public bool bothWays = false;
    public bool keepsUsedCard = false;
    public bool keepsDestinationCard = false;

    public bool ShouldKeepCard(CardData cardData) {
        return (keepsUsedCard && cardData == usedCard) || (keepsDestinationCard && cardData == destinationCard);
    }
}
