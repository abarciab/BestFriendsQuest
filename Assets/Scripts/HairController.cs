using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HairController : MonoBehaviour, IFeatureController
{
    [SerializeField] private GameObject _featurePrefab;
    [SerializeField] private Transform _featureListParent;
    [SerializeField] private List<HairPiece> _currentPieces = new List<HairPiece>();
    [SerializeField] private List<FeatureData> _allOptions = new List<FeatureData>();
    [SerializeField] private int _currentIndex;
    [SerializeField] private Vector2 _limits;
    [SerializeField] private Vector2 _scaleLimits = new Vector2(0.2f, 1.8f);
    [SerializeField] private Transform _target;

    public Color HairColor { get; private set; }

    public bool HasCurrent() => _currentPieces.Count > 0;
    public FeatureObj Current => _currentPieces[_currentIndex];
    public FeatureObj GetCurrent() => Current;
    public List<FeatureData> GetAllOptions() => _allOptions;
    public List<FeatureObj> GetCurrentFeatures() => _currentPieces.Cast<FeatureObj>().ToList();

    private void Start()
    {
        _currentPieces = GetComponentsInChildren<HairPiece>().Where(x => !x.IsMirroredVersion).ToList();
        foreach (var c in _currentPieces) c.Initialize(this);
        foreach (var c in _currentPieces) if (c.GetData().MatchColor) c.SetColor(HairColor);
    }

    public void SetCurrentColor(Color newColor)
    {
        Current.SetColor(newColor);
    }

    public void SetHairColor(Color newColor)
    {
        foreach (var h in _currentPieces) {
            if (h.GetData().MatchColor) h.SetColor(newColor);
        }
        HairColor = newColor;
    }

    public Vector3 GetTargetPosition(float hori, float vert)
    {
        var pos = _target.localPosition;
        var x = Mathf.Lerp(-_limits.x, _limits.x, hori);
        var z = Mathf.Lerp(-_limits.y, _limits.y, vert);
        pos.x = x;
        pos.z = z;
        _target.localPosition = pos;
        return _target.position;
    }

    public void SetSize(float size)
    {
        _currentPieces[_currentIndex].transform.GetChild(0).GetChild(0).transform.localScale = Vector3.one * Mathf.Lerp(_scaleLimits.x, _scaleLimits.y, size);
    }

    public void SetAngle(float angle)
    {
        _currentPieces[_currentIndex].transform.GetChild(0).GetChild(0).localEulerAngles = Vector3.up * Mathf.Lerp(-180, 180, angle);
    }

    private void OnDrawGizmosSelected()
    {
        var center = _target.parent.position;
        center.y = _target.position.y;

        var size = 0.5f;
        var leftBottom = center + (Vector3.left * _limits.x) + (Vector3.back * _limits.y);
        Gizmos.DrawWireSphere(leftBottom, size);
        var leftTop = center + (Vector3.left * _limits.x) + (Vector3.forward * _limits.y);
        Gizmos.DrawWireSphere(leftTop, size);
        var rightBottom = center + (Vector3.right * _limits.x) + (Vector3.back * _limits.y);
        Gizmos.DrawWireSphere(rightBottom, size);
        var rightTop = center + (Vector3.right * _limits.x) + (Vector3.forward * _limits.y);
        Gizmos.DrawWireSphere(rightTop, size);
    }

    public void CopySettingsToCurrent(FeatureObj original)
    {
        original.CopyTo(Current);
    }

    public FeatureObj AddFeature(FeatureData data)
    {
        var newFeature = Instantiate(_featurePrefab, _featureListParent).GetComponent<HairPiece>();
        newFeature.Initialize(data, this);
        _currentPieces.Add(newFeature);
        _currentIndex = _currentPieces.Count - 1;
        if (newFeature.GetData().MatchColor) newFeature.SetColor(HairColor);
        return newFeature;
    }

    public void Delete(FeatureObj feature)
    {
        if (Current == feature) _currentIndex = Mathf.Max(0, _currentIndex - 1);
        _currentPieces.Remove((HairPiece)feature);
        Destroy(feature.gameObject);
    }

    public void Select(FeatureObj feature)
    {
        for (int i = 0; i < _currentPieces.Count; i++) {
            if (feature == _currentPieces[i]) _currentIndex = i;
        }
    }

    public void Save(FeatureData data)
    {
        for (int i = 0; i < _allOptions.Count; i++) {
            if (data.Mesh == _allOptions[i].Mesh) {
                _allOptions[i] = data;
                return;
            }
        }
        _allOptions.Add(data);
        Utils.SetDirty(this);
    }

}
