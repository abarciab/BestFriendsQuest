using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FaceFeatureController : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<FacialFeature> _features = new List<FacialFeature>();
    [SerializeField] private int _selected;

    public FacialFeature GetCurrent() => _features[_selected];

    private void Start()
    {
        _features.Clear();
        var features = GetComponentsInChildren<FacialFeature>();
        foreach (var feature in features) _features.Add(feature);
    }

    public void Select(int index)
    {
        _selected = index;
        _selected = Mathf.Clamp(_selected, 0, _features.Count-1);
    }
}
