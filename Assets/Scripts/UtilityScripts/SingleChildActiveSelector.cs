using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleChildActiveSelector : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSelect;

    public void HideAll()
    {
        foreach (Transform child in transform) child.gameObject.SetActive(false);
    }

    public void Select(int index)
    {
        HideAll();
        transform.GetChild(index).gameObject.SetActive(true);
        OnSelect.Invoke();
    }
}
