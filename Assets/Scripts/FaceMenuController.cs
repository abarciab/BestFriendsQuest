using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMenuController : MonoBehaviour
{
    [SerializeField] private List<SelectableItem> _tabButtons = new List<SelectableItem>();
    [SerializeField] private List<GameObject> _menus = new List<GameObject>();

    private void OnEnable()
    {
        _tabButtons[0].Select();
    }

    public void SelectTab(int tab)
    {
        for (int i = 0; i < _tabButtons.Count; i++) {
            if (i != tab) _tabButtons[i].Deselect();
            _menus[i].SetActive(i == tab);
        }
    }
}
