using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairMoveMenu : MonoBehaviour
{
    [SerializeField] private HairController _hairController;
    [SerializeField] private Slider _vertSlider;
    [SerializeField] private Slider _horiSlider;
    [SerializeField] private Slider _sizeSlider;
    [SerializeField] private Slider _angleSlider;

    public void SetPos() => _hairController.SetPosition(_horiSlider.value, _vertSlider.value);
    public void SetSize() => _hairController.SetSize(_sizeSlider.value);
    public void SetRot() => _hairController.SetAngle(_angleSlider.value);
}
