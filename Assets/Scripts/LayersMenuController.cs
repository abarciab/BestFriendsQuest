using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _add;

    private void OnEnable()
    {
        _main.SetActive(true);
        _add.SetActive(false);
    }
}
