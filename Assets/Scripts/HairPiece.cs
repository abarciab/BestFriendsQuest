using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HairPiece : FeatureObj
{
    [SerializeField] private Transform _modelParent;
    private HairController _controller;
    private HairPiece _mirroredFeature;

    public void Initialize(FeatureData newData, HairController controller)
    {
        Data = newData;
        Initialize(controller);
    }

    public void Initialize(HairController controller)
    {
        _controller = controller;
        _modelParent.GetComponentInChildren<MeshFilter>().mesh = Data.Mesh;
        UpdateDisplay();
    }

    public void SetMatch(bool match)
    {
        Settings.MatchColor = match;
    }

    protected override void SpawnMirror()
    {
        base.SpawnMirror();
        _mirroredFeature = MirroredFeature.As<HairPiece>();
        _mirroredFeature.Initialize(_controller);
    }

    protected override void UpdateDisplay()
    {
        _modelParent.transform.localScale = Vector3.one * Mathf.Lerp(Data.SizeLimits.x, Data.SizeLimits.y, Settings.Size);
        UpdateColors();

        base.UpdateDisplay();

        if (!IsMirroredVersion) {
            AlignWithTarget();
            AlignToHeadNormal();

            _modelParent.GetChild(0).transform.localEulerAngles = Vector3.up * Mathf.Lerp(-180, 180, Settings.Angle);


            if (_mirroredFeature) _mirroredFeature.SetMirrorRot(transform, _modelParent);
        }
    }

    private void UpdateColors()
    {
        var renderers = _modelParent.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers) r.material.color = Settings.Color;
    }

    public void SetMirrorRot(Transform _nonMirroredPiece, Transform modelParent)
    {
        transform.rotation = _nonMirroredPiece.rotation;
        var eulers = transform.localEulerAngles;
        eulers.y *= -1;
        transform.localEulerAngles = eulers;

        _modelParent.localPosition = modelParent.localPosition;
        
        eulers = modelParent.localEulerAngles;
        eulers.y *= -1;
        eulers.z *= -1;
        _modelParent.localEulerAngles = eulers;

        var scale = _modelParent.localScale;
        scale.x *= -1;
        _modelParent.localScale = scale;

        _modelParent.GetChild(0).transform.localEulerAngles = modelParent.GetChild(0).transform.localEulerAngles;
    }

    private void AlignWithTarget()
    {
        var targetPos =  _controller.GetTargetPosition(Settings.Hori, Settings.Vert);
        transform.LookAt(targetPos);
    }

    private void AlignToHeadNormal()
    {
        _modelParent.localPosition = Vector3.forward * 2.5f;
        var dir = transform.forward * -1;
        bool hit = Physics.Raycast(_modelParent.position, dir, out var hitData, 100);
        if (!hit) {
            return;
        }

        _modelParent.up = hitData.normal;
        _modelParent.position = hitData.point;
    }

    [ButtonMethod]
    private void Save()
    {
        var controller = GetComponentInParent<HairController>();
        controller.Save(Data);
    }
}
