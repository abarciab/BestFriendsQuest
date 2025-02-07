using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsManager : MonoBehaviour
{
    public ItemBanner recordItem;
    //public GameObject recordContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearRecords()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

    }

    public void CreateRecordItem(string itemName, int itemCount)
    {
       ItemBanner newBanner = Instantiate(recordItem, this.transform);
       newBanner.UpdateName(itemName);
       newBanner.UpdateCount(itemCount);
    }


}
