using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI populationText;

    [SerializeField]
    TextMeshProUGUI foodProductionText;

    [SerializeField]
    TextMeshProUGUI knowledgePointsText;

    [SerializeField]
    TextMeshProUGUI staminaText;

    void Start() {
        
    }

    void Update() {
        populationText.text = Tracker.instance.Population.ToString();
        foodProductionText.text = Tracker.instance.FoodProduction.ToString();
        knowledgePointsText.text = Tracker.instance.CurrentKnowledgePoints.ToString();
        staminaText.text = Tracker.instance.Stamina.ToString();
    }
}
