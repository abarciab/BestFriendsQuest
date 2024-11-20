using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class SetScollbarSize : MonoBehaviour
{
    [SerializeField] private float _size = 0.415f;
    private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar = GetComponent<Scrollbar>();
    }

    void LateUpdate()
    {
        _scrollbar.size = _size;
    }
}
