using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    int maxStamina = 10;

    public int Population { get; private set; }
    public int FoodProduction { get; private set; }
    public int CurrentKnowledgePoints { get; private set; }
    public int TotalKnowledgePoints { get; private set; }
    public int Stamina { get; private set; }

    List<Modifier> populationGrowthModifiers = new List<Modifier>();
    List<Modifier> foodProductionModifiers = new List<Modifier>();

    HashSet<CardData> discoveredCards = new HashSet<CardData>();

    public static Tracker instance;

    void Awake() {
        if (instance)
            throw new Exception("Cant have more than one Tracker in scene");

        instance = this;
    }

    void Start() {
        if (populationGrowthModifiers.Count == 0) {
            Population = 3;
            Stamina = maxStamina;
            populationGrowthModifiers.Add(new Modifier(10));
        }
    }

    public bool SpendStamina(int amount) {
        if (Stamina < amount) return false;

        Stamina -= amount;
        return true;
    }

    public void RecoverHalfStamina() {
        Stamina = maxStamina / 2;
    }

    public void RecoverFullStamina(int bonus) {
        Stamina = maxStamina + bonus;
    }

    public void AddDiscoveredCard(CardData card) {
        if (discoveredCards.Contains(card)) return;

        if (card.foodProduction != 0)
            AddFoodProductionModifier(new Modifier(card.foodProduction));

        if (card.knowledgePoints != 0)
            AddKnowledgePoints(card.knowledgePoints);

        discoveredCards.Add(card);
    }

    public void CalculateNewPopulation() {
        var growthRate = 0;
        foreach (var modifier in foodProductionModifiers) {
            growthRate += modifier.Amount;
        }

        Population *= growthRate;
    }

    void AddFoodProductionModifier(Modifier modifier) {
        foodProductionModifiers.Add(modifier);
        CalculateFoodProduction();
    }

    void RemoveFoodProductionModifier(Modifier modifier) {
        if (!foodProductionModifiers.Contains(modifier)) return;

        foodProductionModifiers.Remove(modifier);
        CalculateFoodProduction();
    }

    void CalculateFoodProduction() {
        int production = 0;
        foreach (var modifier in foodProductionModifiers) {
            production += modifier.Amount;
        }

        FoodProduction = production;
    }

    void AddKnowledgePoints(int amount) {
        CurrentKnowledgePoints += amount;
        TotalKnowledgePoints += amount;
    }

    void RemoveKnowledgePoints(int amount) {
        CurrentKnowledgePoints -= amount;
    }

    void AddPopulationGrowthModifier(Modifier modifier) {
        populationGrowthModifiers.Add(modifier);
    }

    void RemovePopulationGrowthModifier(Modifier modifier) {
        if (!populationGrowthModifiers.Contains(modifier)) return;

        populationGrowthModifiers.Remove(modifier);
    }
}
