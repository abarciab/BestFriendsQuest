using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMenuController : MonoBehaviour
{
    [SerializeField, OverrideLabel("feature controller")] private GameObject _controllerObj;
    private IFeatureController _controller;
    [SerializeField] private Slider _horiSlider;
    [SerializeField] private Slider _vertSlider;
    [SerializeField] private Slider _sizeSlider;
    [SerializeField] private Slider _angleSlider;
    [SerializeField] private List<SelectableItem> _mirrorOptions = new List<SelectableItem>();
    private FeatureObj _current;

    private void OnEnable()
    {
        _controller ??= _controllerObj.GetComponent<IFeatureController>(); 
        _current = _controller.GetCurrent();

        var currentValues = _current.GetValues();
        _horiSlider.value = currentValues.x;
        _vertSlider.value = currentValues.y;
        _sizeSlider.value = currentValues.z;
        _angleSlider.value = currentValues.w;

        _mirrorOptions[(int)_current.GetData().Mirror].Select();
    }

    public void SetVert() => _current.SetVert(_vertSlider.value);
    public void SetHori() => _current.SetHori(_horiSlider.value);
    public void SetSize() => _current.SetSize(_sizeSlider.value);
    public void SetAngle() => _current.SetAngle(_angleSlider.value);
    public void SetMirror(int type) => _current.SetMirror((MirrorType) type);
}
