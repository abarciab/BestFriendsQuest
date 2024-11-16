using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLevelMenuController : MonoBehaviour
{
    [SerializeField] private List<SelectableItem> _tabButtons = new List<SelectableItem>();
    [SerializeField] private List<GameObject> _menus = new List<GameObject>();
    [SerializeField] private LayersMenuController _layersMenu;

    private void OnEnable()
    {
        _tabButtons[0].Select();
        if (_layersMenu) _layersMenu.Initialize();
    }

    public void SelectTab(int tab)
    {
        if (tab > 0 && _layersMenu && !_layersMenu.HasCurrent) {
            _tabButtons[tab].Deselect();
            return;
        }

        for (int i = 0; i < _tabButtons.Count; i++) {
            if (i != tab) _tabButtons[i].Deselect();
            _menus[i].SetActive(i == tab);
        }
    }
}
