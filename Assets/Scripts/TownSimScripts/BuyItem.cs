using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public TownGameManager gameManager;

    public Item item;

    public void Puchased()
    {
        if(Mathf.Abs(item.Cost) <= gameManager.currency)
        {
            gameManager.ChangeCurrency(-item.Cost);
            gameManager.AddInventory(item);
        }
        
    }
}
