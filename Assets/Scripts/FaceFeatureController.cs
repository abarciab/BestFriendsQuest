using MyBox;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum FaceFeatureType {BROWS, EYES, NOSE, LIPS, MISC}
public enum FeatureType { FACE, HAIR}

[System.Serializable]
public class FeatureData
{
    [HideInInspector] public string Name;
    public FeatureType Type;
    [ConditionalField(nameof(Type), false, FeatureType.FACE)]public FaceFeatureType SubType;

    public Sprite Icon;
    [ConditionalField(nameof(Type), false, FeatureType.FACE)] public Texture2D Texture;
    public Texture2D ColorMask;

    public Vector2 HoriLimits;
    public Vector2 VertLimits;
    public Vector2 SizeLimits;
    public bool Mirror;

    public FeatureData() {}
    public FeatureData(FeatureData o)
    {
        Name = o.Name;
        Type = o.Type;
        SubType = o.SubType;
        Icon = o.Icon;
        Texture = o.Texture;
        ColorMask = o.ColorMask;
        HoriLimits = o.HoriLimits;
        VertLimits = o.VertLimits;
        SizeLimits = o.SizeLimits;
        Mirror = o.Mirror;
    }
}

public class FaceFeatureController : MonoBehaviour, IFeatureController
{
    [SerializeField] private GameObject _featurePrefab;
    [SerializeField] private Transform _featureParent;
    public List<FacialFeature> CurrentFeatures = new List<FacialFeature>();
    [SerializeField] private List<FeatureData> _allFeatures = new List<FeatureData>();
    [SerializeField] private int _selected;

    public FacialFeature Current => CurrentFeatures[_selected];
    public List<IFeatureObj> GetAllFeatures() => CurrentFeatures.Cast<IFeatureObj>().ToList();
    public List<FeatureData> GetAllOptions() => _allFeatures;
    public void CopySettingsToCurrent(object original) => original.As<FacialFeature>().CopyTo(Current);

    private void OnValidate()
    {
        foreach (var f in _allFeatures) f.Name = f.Texture.name;
    }

    private void Start()
    { 
        CurrentFeatures = GetComponentsInChildren<FacialFeature>().Where(x => !x.IsMirror).ToList();
    }


    public void SetCurrentColor(Color color)
    {
        Current.SetColor(color);
    }

    public IFeatureObj AddFeature(FeatureData data)
    {
        var newFeature = Instantiate(_featurePrefab, _featureParent).GetComponent<FacialFeature>();
        newFeature.transform.SetAsFirstSibling();
        newFeature.Set(new FeatureData(data));
        CurrentFeatures.Add(newFeature);
        return newFeature;
    }

    public void Delete(object featureGeneric)
    {
        var feature = featureGeneric.As<FacialFeature>();
        CurrentFeatures.Remove(feature);
        Destroy(feature.gameObject);
    }

    public void Select(object featureGeneric)
    {
        var feature = featureGeneric.As<FacialFeature>();
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
