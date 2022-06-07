using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBiome", menuName = "Biome")]
public class Biome : ScriptableObject
{
    public CardData[] deck;
    public Color color;
}
