using MyBox;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class FaceFeatureController : MonoBehaviour, IFeatureController
{
    [SerializeField] private GameObject _featurePrefab;
    [SerializeField] private Transform _featureParent;
    public List<FacialFeature> CurrentFeatures = new List<FacialFeature>();
    [SerializeField] private List<FeatureData> _allFeatures = new List<FeatureData>();
    [SerializeField] private int _selected;

    public bool HasCurrent() => CurrentFeatures.Count > 0;
    public FacialFeature Current => _selected < CurrentFeatures.Count ? CurrentFeatures[_selected] : CurrentFeatures[0];
    public List<FeatureObj> GetCurrentFeatures() => CurrentFeatures.Cast<FeatureObj>().ToList();
    public List<FeatureData> GetAllOptions() => _allFeatures;
    public void CopySettingsToCurrent(FeatureObj original) => original.CopyTo(Current);
    public FeatureObj GetCurrent() => Current;

    private void OnValidate()
    {
        foreach (var f in _allFeatures) f.Name = f.Texture.name;
    }

    private void Start()
    { 
        CurrentFeatures = GetComponentsInChildren<FacialFeature>().Where(x => !x.IsMirroredVersion).ToList();
    }


    public void SetCurrentColor(Color color)
    {
        Current.SetColor(color);
    }

    public FeatureObj AddFeature(FeatureData data)
    {
        var newFeature = Instantiate(_featurePrefab, _featureParent).GetComponent<FacialFeature>();
        newFeature.transform.SetAsFirstSibling();
        newFeature.Set(new FeatureData(data));
        CurrentFeatures.Add(newFeature);
        return newFeature;
    }

    public void Delete(FeatureObj featureGeneric)
    {
        var feature = (FacialFeature) featureGeneric;
        CurrentFeatures.Remove(feature);
        Destroy(feature.gameObject);
    }

    public void Select(FeatureObj featureGeneric)
    {
        var feature = (FacialFeature) featureGeneric;
        for (int i = 0; i < CurrentFeatures.Count; i++) {
            if (CurrentFeatures[i] == feature) _selected = i;
        }
    }

    public void SaveFeature(FeatureData data)
    {
        data.Name = data.Texture.name;

        bool found = false;
        for (int i = 0; i < _allFeatures.Count; i++) {
            if (_allFeatures[i].Texture == data.Texture) {
                _allFeatures[i] = data;
                found = true;
            }
        }
        if (!found) _allFeatures.Add(data);
        Utils.SetDirty(this);
    }
}
