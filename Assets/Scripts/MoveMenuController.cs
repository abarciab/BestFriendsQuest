using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is just move menu controller for face menu - rename later
public class MoveMenuController : MonoBehaviour
{
    public FaceFeatureController _faceController;
    [SerializeField] private Slider _horiSlider;
    [SerializeField] private Slider _vertSlider;
    [SerializeField] private Slider _sizeSlider;
    [SerializeField] private Slider _angleSlider;
    private FacialFeature _current;

    private void OnEnable()
    {
        _current = _faceController.Current;
        var currentValues = _current.GetValues();
        _horiSlider.value = currentValues.x;
        _vertSlider.value = currentValues.y;
        _sizeSlider.value = currentValues.z;
        _angleSlider.value = currentValues.w;
    }

    public void SetVert() => _current.SetVert(_vertSlider.value);
    public void SetHori() => _current.SetHori(_horiSlider.value);
    public void SetSize() => _current.SetSize(_sizeSlider.value);
    public void SetAngle() => _current.SetAngle(_angleSlider.value);
}
