using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public TownGameManager gameManager;

    public float cost;
    public string itemName;

    public void Puchased()
    {
        if(Mathf.Abs(cost) <= gameManager.currency)
        {
            gameManager.ChangeCurrency(cost);
            gameManager.AddInventory(itemName);
        }
        
    }
}
