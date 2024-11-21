using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMetaController : MonoBehaviour
{
    [SerializeField] private SetMaterialColor _skin;
    [SerializeField] private Color _skinColor;
    [SerializeField] private FaceFeatureController _face;
    [SerializeField] private HairController _hair;
    [SerializeField] private EarController _ears;

    public Color SkinColor => _skinColor;

    private void Start()
    {
        SetSkinColor(_skinColor);
    }

    public void LoadFromString(string input)
    {
        input = input.Replace("\n", "");
        var parts = input.Split('|');
        _face.LoadFromString(parts[0]);
        _hair.LoadFromString(parts[1]);
        _ears.LoadFromString(parts[2]);

        ColorUtility.TryParseHtmlString(parts[3], out _skinColor);
        SetSkinColor(_skinColor);
    }

    public string GetSaveString()
    {
        var result = "";
        result += _face.GetSaveString();
        result += _hair.GetSaveString();
        result += _ears.GetSaveString();
        result += _skinColor.ToHex() + "\n";
        return result;
    }

    public void SetSkinColor(Color color)
    {
        _skinColor = color;
        _skin.SetColor(_skinColor);
        _ears.SetColor(_skinColor);
    }

}
