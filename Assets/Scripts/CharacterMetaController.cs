using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMetaController : MonoBehaviour
{
    [SerializeField] private SetMaterialColor _skin;
    [SerializeField] private Color _skinColor;
    [SerializeField] private EarController _ears;

    private void Start()
    {
        SetSkinColor(_skinColor);
    }

    public void SetSkinColor(Color color)
    {
        _skinColor = color;
        _skin.SetColor(_skinColor);
        _ears.SetColor(_skinColor);
    }

}
