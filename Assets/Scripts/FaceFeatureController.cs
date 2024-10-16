using MyBox;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureType {BROWS, EYES, NOSE, LIPS, MISC}

[System.Serializable]
public class FeatureData
{
    [HideInInspector] public string Name;
    public FeatureType Type;

    public Sprite Icon;
    public Texture2D Texture;
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
        Icon = o.Icon;
        Texture = o.Texture;
        ColorMask = o.ColorMask;
        HoriLimits = o.HoriLimits;
        VertLimits = o.VertLimits;
        SizeLimits = o.SizeLimits;
        Mirror = o.Mirror;
    }
}

public class FaceFeatureController : MonoBehaviour
{
    [SerializeField] private GameObject _featurePrefab;
    [SerializeField] private Transform _featureParent;
    public List<FacialFeature> CurrentFeatures = new List<FacialFeature>();
    public List<FeatureData> AllFeatures = new List<FeatureData>();
    [SerializeField] private int _selected;

    public FacialFeature Current => CurrentFeatures[_selected];

    private void OnValidate()
    {
        foreach (var f in AllFeatures) f.Name = f.Texture.name;
    }

    private void Start()
    { 
        CurrentFeatures = GetComponentsInChildren<FacialFeature>().Where(x => !x.IsMirror).ToList();
    }

    public void CopySettingsToCurrent(FacialFeature feature)
    {
        feature.CopyTo(Current);
    }

    public void SetCurrentColor(Color color)
    {
        Current.SetColor(color);
    }

    public FacialFeature AddFeature(FeatureData data)
    {
        var newFeature = Instantiate(_featurePrefab, _featureParent).GetComponent<FacialFeature>();
        newFeature.transform.SetAsFirstSibling();
        newFeature.Set(new FeatureData(data));
        CurrentFeatures.Add(newFeature);
        return newFeature;
    }

    public void Delete(FacialFeature feature)
    {
        CurrentFeatures.Remove(feature);
        Destroy(feature.gameObject);
    }

    public void Select(FacialFeature feature)
    {
        for (int i = 0; i < CurrentFeatures.Count; i++) {
            if (CurrentFeatures[i] == feature) _selected = i;
        }
    }

    public void SaveFeature(FeatureData data)
    {
        data.Name = data.Texture.name;

        bool found = false;
        for (int i = 0; i < AllFeatures.Count; i++) {
            if (AllFeatures[i].Texture == data.Texture) {
                AllFeatures[i] = data;
                found = true;
            }
        }
        if (!found) AllFeatures.Add(data);
        Utils.SetDirty(this);
    }
}
