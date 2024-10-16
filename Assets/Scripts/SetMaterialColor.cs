using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SetMaterialColor : MonoBehaviour
{
    [SerializeField] private int _materialIndex;
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color col)
    {
        _renderer.materials[_materialIndex].color = col;
    }
}
