using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class TownGameManager : MonoBehaviour
{
    [Header ("Inventory")]
    public float currency;
    
    public RecordsManager recordsManager;
    
    public List<string> itemNames= new List<string> (); 
    public List<int> itemCounts = new List<int> ();
    public Dictionary<string, int> items = new Dictionary<string, int> ();

    [Header ("UI Lists")]

    public List<GameObject> sceneList = new List<GameObject>();
    public List<GameObject> sceneUIList = new List<GameObject>();
    public List<TMP_Text> currencyDisplays = new List<TMP_Text>();

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerCurrency"))
        {
            currency = PlayerPrefs.GetFloat("PlayerCurrency");
            ChangeCurrency(0);
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerCurrency", currency);
            ChangeCurrency(100);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(GameObject newScene, GameObject newSceneUI)
    {
        //iterates over all environments and UIs, disabling. then, enables selected environment and UI
        foreach (GameObject i in sceneList)
        {
            i.SetActive(false);
            ViewRecords();
            

        }
        foreach (GameObject j in sceneUIList)
        {
            j.SetActive(false);
        }

        newScene.SetActive(true);
        newSceneUI.SetActive(true);
    }

    public void ChangeCurrency(float curChange)
    {
        currency += curChange;
        foreach (TMP_Text i in currencyDisplays)
        {
            i.text =  "$" + currency.ToString("F2");
        }

        PlayerPrefs.SetFloat("PlayerCurrency", currency);

    }

    public void AddInventory(string inventoryName)
    {
        if (items.ContainsKey(inventoryName))
        {
            items[inventoryName] += 1;
        }
        else{
            items.Add(inventoryName, 1);
        }

        //items.Add(inventoryName);

        itemNames.Clear();

        foreach (var i in items.Keys)
        {
            itemNames.Add(i.ToString());
        }

        itemCounts.Clear();

        foreach(var j in items.Values)
        {
            itemCounts.Add(j);
        }
    }

    public void GiveMoney()
    {
        ChangeCurrency(100);
    }

    public void ViewRecords()
    {
        recordsManager.ClearRecords();

        foreach (var i in items)
        {
             recordsManager.CreateRecordItem(i.Key, i.Value);
        }
    }

    

}
