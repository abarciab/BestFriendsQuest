using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingBar : MonoBehaviour
{
    public bool isHorizontal;

    public List<TargetZone> targets = new List<TargetZone>();

    [Header("Icon")]
    public GameObject barIcon;
    public float iconPosX;
    public float iconPosY;

    [Header("Animation")]
    public float barSpeed; // or period
    public float amplitude; // one half width of bar
    public float shift; //should be the middle of the bar




    // Start is called before the first frame update
    void Start()
    {
        iconPosX = barIcon.GetComponent<RectTransform>().anchoredPosition.x;
        iconPosY = barIcon.GetComponent<RectTransform>().anchoredPosition.y;

        //var rectTransform = GetComponent<RectTransform>();
        //float width = rectTransform.sizeDelta.x;
        //float height = rectTransform.sizeDelta.y;

        if (isHorizontal)
        {
            amplitude = GetComponent<RectTransform>().sizeDelta.x /2 ;
            shift = GetComponent<RectTransform>().anchoredPosition.x;

        }

        foreach (TargetZone target in GetComponentsInChildren<TargetZone>()) { 
            targets.Add(target);
            target.SetBounds(target.GetComponent<RectTransform>().anchoredPosition.x, isHorizontal);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (isHorizontal) {
            iconPosX = (amplitude * Mathf.Sin(barSpeed * (Time.time)) + shift);
            barIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(iconPosX, iconPosY);
        }


        //if (isHorizontal) { 
           
        //}
        //else
        //{
        //    if (iconPosX < GetComponent<RectTransform>().anchoredPosition.x && flipped == false) { 
        //        //move towards firstpoint
        //    }
        //    else if( iconPosY >= GetComponent<RectTransform>().anchoredPosition.x  && flipped == false)
        //    {   
        //        //flip that thang around
        //    }
        //}

    }
}
