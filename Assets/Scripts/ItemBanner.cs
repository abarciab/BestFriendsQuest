using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBanner : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text itemName;
    public TMP_Text itemCount;

   

    public void UpdateName(string newName)
    {
        itemName.text = newName;
    }

    public void UpdateCount(int newCount)
    {
        itemCount.text = newCount.ToString();
    }
}
