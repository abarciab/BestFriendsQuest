using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
        Data.MatchColor = match;
    }

    protected override void SpawnMirror()
    {
        base.SpawnMirror();
        _mirroredFeature = MirroredFeature.As<HairPiece>();
        _mirroredFeature.Initialize(_controller);
    }

    protected override void UpdateDisplay()
    {
        AlignWithTarget();
        AlignToHeadNormal();
        _modelParent.transform.localScale = Vector3.one * Mathf.Lerp(Data.SizeLimits.x, Data.SizeLimits.y, Size);
        UpdateColors(); 

        base.UpdateDisplay();

        if (!IsMirroredVersion) {
            _modelParent.GetChild(0).transform.localEulerAngles = Vector3.up * Mathf.Lerp(-180, 180, Angle);

            if (_mirroredFeature) _mirroredFeature.SetMirrorRot(_modelParent);
        }
    }

    private void UpdateColors()
    {
        var renderers = _modelParent.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers) r.material.color = Color;
    }

    public void SetMirrorRot(Transform _otherModelParent)
    {
        _modelParent.GetChild(0).transform.localEulerAngles = Vector3.up * Mathf.Lerp(-180, 180, Angle);
        //_modelParent.rotation = _otherModelParent.rotation;
    }

    private void AlignWithTarget()
    {
        var targetPos =  _controller.GetTargetPosition(Hori, Vert);
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

        if (!IsMirroredVersion) _modelParent.up = hitData.normal;
        _modelParent.position = hitData.point;
    }

    [ButtonMethod]
    private void Save()
    {
        var controller = GetComponentInParent<HairController>();
        controller.Save(Data);
    }
}
