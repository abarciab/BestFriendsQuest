using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ear : FeatureObj
{
    private GameObject _model;

    private void Start()
    {
        foreach (Transform child in transform) if (child.gameObject != _model) Destroy(child.gameObject);
    }

    public override void SetAsMirroredVersion()
    {
        base.SetAsMirroredVersion();
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        initilize();
    }

    public void initilize()
    {
        _model = Instantiate(Data.EarPrefab, transform);
        UpdateDisplay();
    }

    protected override void UpdateDisplay()
    {
        base.UpdateDisplay();
        if (!_model) return;
        _model.GetComponentInChildren<MeshRenderer>().materials[0].color = Color;
        _model.transform.localScale = Vector3.one * Mathf.Lerp(Data.SizeLimits.x, Data.SizeLimits.y, Size);

        var euler = _model.transform.localEulerAngles;
        if (IsMirroredVersion) euler.x = Mathf.Lerp(Data.AngleLimits.y, Data.AngleLimits.x, Angle);
        else euler.x = Mathf.Lerp(Data.AngleLimits.x, Data.AngleLimits.y, Angle);
        _model.transform.localEulerAngles = euler;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var pos = _model.transform.localPosition;

        if (IsMirroredVersion) pos.x = Mathf.Lerp(Data.HoriLimits.y, Data.HoriLimits.x, Hori);
        else pos.x = Mathf.Lerp(Data.HoriLimits.x, Data.HoriLimits.y, Hori);

        pos.y = Mathf.Lerp(Data.VertLimits.x, Data.VertLimits.y, Vert);

        _model.transform.localPosition = pos;
    }

    public void SetData(FeatureData data)
    {
        Data = data;
        Destroy(_model);
        initilize();
    }

    [ButtonMethod]
    private void Save()
    {
        GetComponentInParent<EarController>().Save(Data);
    }
}
