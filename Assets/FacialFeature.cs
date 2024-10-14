using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(DecalProjector))]
public class FacialFeature : MonoBehaviour
{
    [SerializeField] private Vector2 _horiLimits;
    [SerializeField] private Vector2 _vertLimits;
    [SerializeField] private Vector2 _sizeLimits;

    private DecalProjector _projector;

    private void Start()
    {
        _projector = GetComponent<DecalProjector>();
    }

    public void SetAll(float hori, float vert, float size, float angle)
    {
        SetHori(hori);
        SetVert(vert);
        SetSize(size);
        SetAngle(angle);
    }

    public void SetHori(float hori)
    {
        var pos = transform.localPosition;
        pos.x = Mathf.Lerp(_horiLimits.x, _horiLimits.y, hori);
        transform.localPosition = pos;
    }

    public void SetVert(float vert)
    {
        var pos = transform.localPosition;
        pos.y = Mathf.Lerp(_vertLimits.x, _vertLimits.y, vert);
        transform.localPosition = pos;
    }

    public void SetSize(float scale)
    {
        _projector.size = Vector3.one * Mathf.Lerp(_sizeLimits.x, _sizeLimits.y, scale);
    }

    public void SetAngle(float inputAngle)
    {
        var angle = Mathf.Lerp(-180, 180, inputAngle);
        var euler = new Vector3(0, 0, angle);
        transform.localEulerAngles = euler;
    }

    [ButtonMethod]
    private void SetMaxScale()
    {
        _sizeLimits.y = GetComponent<DecalProjector>().size.x;
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void SetMinScale()
    {
        _sizeLimits.x = GetComponent<DecalProjector>().size.x;
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void SetBottomLeft()
    {
        _horiLimits.x = transform.localPosition.x;
        _vertLimits.x = transform.localPosition.y;
        Utils.SetDirty(this);
    }

    [ButtonMethod]
    private void SetTopRight()
    {
        _horiLimits.y = transform.localPosition.x;
        _vertLimits.y = transform.localPosition.y;
        Utils.SetDirty(this);
    }
}
