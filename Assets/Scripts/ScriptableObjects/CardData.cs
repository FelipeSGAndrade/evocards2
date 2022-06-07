using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardData : ScriptableObject
{
    public string title;
    public Sprite sprite;

    public CardData handRemove;
    public CardData handStay;

    public int knowledgePoints;
    public int foodProduction;
}
