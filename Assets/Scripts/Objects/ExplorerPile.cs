using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplorerPile : MonoBehaviour
{
    public UnityEvent OnPileClicked = new UnityEvent();

    void Start() {
        
    }

    void Update() {
        
    }

    void OnMouseDown() {
        OnPileClicked.Invoke();
    }
}
