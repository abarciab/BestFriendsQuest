using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOption : MonoBehaviour
{
    [SerializeField] private Image _preview;
    private FeatureData _data;

    public FeatureType Type => _data.Type;

    public void Initialize(FeatureData data)
    {
        _preview.sprite = data.Icon;
        _data = data;
    }

    public void Select()
    {
        GetComponentInParent<LayersMenuController>().AddFeature(_data);
    }
}
