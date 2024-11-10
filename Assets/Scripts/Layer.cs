using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Layer : MonoBehaviour
{
    private LayersMenuController _controller;
    [SerializeField] private SelectableItem _mirrorCheckBox;
    [SerializeField] private Image _preview;
    private IFeatureObj _feature;

    private void Start()
    {
        _controller = GetComponentInParent<LayersMenuController>();
    }

    public void Initialize(IFeatureObj feature)
    {
        _feature = feature.As<IFeatureObj>();
        _preview.sprite = _feature.GetIcon();
        if (_feature.IsMirror()) _mirrorCheckBox.Select();
    }

    public void Select()
    {
        if (!_controller) _controller = GetComponentInParent<LayersMenuController>();
        _controller.Select(transform.GetSiblingIndex(), _feature);
    }

    public void Delete()
    {
        _controller.DeleteFeature(this, _feature);
    }

    public void SetMirror(bool mirrored)
    {
        _feature.SetMirror(mirrored);
    }

    public void Duplicate()
    {
        _controller.Duplicate(_feature);
    }
}
