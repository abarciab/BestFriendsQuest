using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private LayersMenuController _controller;

    private void Start()
    {
        _controller = GetComponentInParent<LayersMenuController>();
    }

    public void Select()
    {
        _controller.Select(transform.GetSiblingIndex());
    }
}
