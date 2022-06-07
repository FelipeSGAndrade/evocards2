using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public int Amount { get; private set; }

    public Modifier(int amount) {
        Amount = amount;
    }
}
