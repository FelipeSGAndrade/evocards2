using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    Recipe[] recipes;

    public static RecipeManager instance;

    public Dictionary<CardData, Dictionary<CardData, Recipe>> combinations;

    void Awake() {
        if (instance)
            throw new Exception("Cant have more than one Recipe Manager in Scene");

        instance = this;

        combinations = new Dictionary<CardData, Dictionary<CardData, Recipe>>();

        recipes = Resources.LoadAll<Recipe>("Recipes");
    }

    void Start()
    {
        foreach (Recipe recipe in recipes) {
            AddCombination(recipe.usedCard, recipe.destinationCard, recipe);

            if(recipe.bothWays && recipe.usedCard != recipe.destinationCard) {
                AddCombination(recipe.destinationCard, recipe.usedCard, recipe);
            }
        }
    }

    void AddCombination(CardData card1, CardData card2, Recipe recipe) {
        if (!combinations.ContainsKey(card1)) {
            combinations.Add(card1, new Dictionary<CardData, Recipe>());
        }

        if (!combinations[card1].ContainsKey(card2)) {
            combinations[card1].Add(card2, recipe);
        }
    }

    public Recipe GetCombinationRecipe(CardData card1, CardData card2) {
        if (combinations.ContainsKey(card1)) {
            if (combinations[card1].ContainsKey(card2)) {
                return combinations[card1][card2];
            }
        }

        return null;
    }
}
