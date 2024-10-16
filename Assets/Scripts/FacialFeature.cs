using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class FacialFeature : MonoBehaviour
{
    [SerializeField] private FeatureData _data;
    [SerializeField] private bool _manualSet;
    [SerializeField, ConditionalField(nameof(_manualSet)), Range(0, 1)] private float _horiPos = 0.5f; 
    [SerializeField, ConditionalField(nameof(_manualSet)), Range(0, 1)] private float _vertPos = 0.5f; 
    [SerializeField, ConditionalField(nameof(_manualSet)), Range(0, 1)] private float _size = 0.5f; 
    [SerializeField, ConditionalField(nameof(_manualSet)), Range(0, 1)] private float _angle = 0.5f;
    [SerializeField, ConditionalField(nameof(_manualSet))] private Color _color;


    [Header("Misc")]
    [SerializeField] private Material _refMaterial;

    [HideInInspector] public bool IsMirror;

    private DecalProjector _projector;
    private FacialFeature _mirroredFeature;

    public FeatureData GetData() => new FeatureData(_data);
    public Vector4 GetValues() => new Vector4(_horiPos, _vertPos, _size, _angle);

    private void OnValidate()
    {
        if (_projector == null) _projector = GetComponent<DecalProjector>();
        if (_data.Texture == null) {
            _projector.material = null;
            return;
        }
        if (_projector.material == null) MakeNewMaterial();
        
        _projector.material.SetTexture("Base_Map", _data.Texture);

        if (_manualSet) UpdateDisplay();
        if (_mirroredFeature && !_mirroredFeature.gameObject.name.Contains("Mirror")) _mirroredFeature.gameObject.name += " Mirror";
    }

    [ButtonMethod]
    private void MakeNewMaterial()
    {
        if (!_projector) _projector = GetComponent<DecalProjector>();
        var mat = new Material(_refMaterial);
        _projector.material = mat;
        mat.name = _data.Texture.name + " (virtual)" + (IsMirror ? "(mirror)" : "");
        gameObject.name = _data.Texture.name;
        UpdateMaterial();
    }

    private void Start()
    {
        _projector = GetComponent<DecalProjector>();
    }

    public void CopyTo(FacialFeature feature)
    {
        feature.SetAll(_horiPos, _vertPos, _size, _angle, _color);
    }

    public void SetColor(Color color)
    {
        if (_mirroredFeature != null) _mirroredFeature.SetColor(color);
        color.a = 1;
        _color = color;
        UpdateDisplay();
    }

    public void SetMirror(bool mirror)
    {
        print("set mirror: " + mirror);
        _data.Mirror = mirror;
        UpdateDisplay();
    }

    public void Set(FeatureData data)
    {
        Reset();
        _data = data;
        OnValidate();
        Center();
    }

    private void UpdateDisplay()
    {
        if ( _projector == null) _projector = GetComponent<DecalProjector>();
        UpdatePos();
        UpdateAngle();

        if (_data.Texture == null) return;
        UpdateScale();
        UpdateMaterial();
        UpdateMirror();
    }

    private void UpdateMirror()
    {
        if (!Application.isPlaying) return;
        if (!_data.Mirror && _mirroredFeature) {
            _mirroredFeature.gameObject.SetActive(false);
            return;
        }
        if (_data.Mirror && _mirroredFeature == null) SpawnMirror();
        if (_mirroredFeature) _mirroredFeature.MirroredSet(_horiPos, _vertPos, _size, _angle);
    }

    private void UpdateAngle()
    {
        var angle = Mathf.Lerp(-180, 180, _angle);
        var euler = new Vector3(0, 0, angle);
        transform.localEulerAngles = euler;
    }

    private void UpdateScale()
    {
        var z = _projector.size.z;
        var newSize = Vector3.one * Mathf.Lerp(_data.SizeLimits.x, _data.SizeLimits.y, _size);
        newSize.z = z;
        _projector.size = newSize;
    }

    private void UpdatePos()
    {
        var pos = transform.localPosition;
        pos.x = Mathf.Lerp(_data.HoriLimits.x, _data.HoriLimits.y, _horiPos);
        pos.y = Mathf.Lerp(_data.VertLimits.x, _data.VertLimits.y, _vertPos);
        transform.localPosition = pos;
    }

    private void UpdateMaterial()
    {
        if (_projector.material == null) MakeNewMaterial();
        _projector.material.SetTexture("_Base_Map", _data.Texture);
        _projector.material.SetTexture("_colorMap", _data.ColorMask);
        _projector.material.SetColor("_tint", _color);
        _projector.material.SetInt("_hasColor", _data.ColorMask == null ? 0 : 1);
    }

    private void OnDestroy()
    {
        if (_mirroredFeature) Destroy(_mirroredFeature.gameObject);
    }

    private void SpawnMirror()
    {
        _mirroredFeature = Instantiate(gameObject, transform.parent).GetComponent<FacialFeature>();
        _mirroredFeature.SetAsMirror();
    }

    public void SetAsMirror()
    {
        IsMirror = true;
        MakeNewMaterial();
    }

    public void MirroredSet(float h, float v, float s, float a)
    {
        if (_projector == null) _projector = GetComponent<DecalProjector>();

        gameObject.SetActive(true);
        _mirroredFeature = null;
        _data.Mirror = false;
        _projector.uvScale = new Vector2(-1, 1);
        _projector.uvBias = new Vector2(1, 0);
        _angle = 1 - a;
        _horiPos = 1 - h;
        _vertPos = v;
        _size = s;
        UpdateDisplay();
    }

    public void SetAll(float hori, float vert, float size, float angle, Color color)
    {
        _horiPos = hori;
        _vertPos = vert;
        _size = size;
        _angle = angle;
        _color = color;

        UpdateDisplay();
    }

    public void SetHori(float hori)
    {
        _horiPos = hori;
        UpdateDisplay();
    }

    public void SetVert(float vert)
    {
        _vertPos = vert;
        UpdateDisplay();
    }

    public void SetSize(float size)
    {
        _size = size;
        UpdateDisplay();
    }

    public void SetAngle(float angle)
    {
        _angle = angle;
        UpdateDisplay();
    }

    [ButtonMethod]
    private void Reset()
    {
        _data = new FeatureData();
        OnValidate();
        Center();
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void SetBottomLeft()
    {
        _data.HoriLimits.x = transform.localPosition.x;
        _data.VertLimits.x = transform.localPosition.y;
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void SetTopRight()
    {
        _data.HoriLimits.y = transform.localPosition.x;
        _data.VertLimits.y = transform.localPosition.y;
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void Center()
    {
        SetAll(0.5f, 0.5f, 0.5f, 0.5f, Color.grey);
        Utils.SetDirty(transform);
    }

    [ButtonMethod]
    private void SaveSettings()
    {
        var controller = GetComponentInParent<FaceFeatureController>();
        controller.SaveFeature(_data);
    }
}
