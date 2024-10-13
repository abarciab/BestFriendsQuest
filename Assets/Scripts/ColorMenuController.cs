using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMenuController : MonoBehaviour
{
    [SerializeField] private RawImage _satValImg;
    [SerializeField] private RawImage _hueImg;
    [SerializeField] private Image _currentColorImg;
    [SerializeField] private Slider _hueSlider;
    [SerializeField] private HexColorInputField _hexInput;
    [SerializeField, Range(0, 1)] private float _hueValue = 0.5f;

    private float _currentHue = 0.5f;
    private float _currentSat = 0.5f;
    private float _currentVal = 0.5f;

    private bool _inputingHex;

    private Color _currentColor => Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

    [SerializeField] private FollowMouseInRectBounds _selector;

    private Texture2D _hueTex;
    private Texture2D _satValTex;

    private void Start()
    {
        CreateTextures();
        _selector.enabled = false;
        UpdateHue();
    }

    private void Update()
    {
        if (_selector.enabled) {
            if (Input.GetMouseButtonUp(0)) StopSelecting();
            else UpdateCurrentColor();
        } 
    }

    public void SetFromHexCode(string hex)
    {
        _inputingHex = true;
        ColorUtility.TryParseHtmlString(hex, out var rgb);
        Color.RGBToHSV(rgb, out _currentHue, out _currentVal, out _currentSat);
        _hueSlider.value = _currentHue;
        _selector.SetPosition(new Vector2(_currentSat, _currentVal));
        UpdateCurrentColor();
    }

    public void UpdateHue()
    {
        _currentHue = _hueSlider.value;
        UpdateSatVal();
        UpdateCurrentColor();
    }

    private void UpdateCurrentColor()
    {
        var pos = _selector.GetNormalizedPositionFromCenter();
        _currentSat = pos.x;
        _currentVal = pos.y;
        _currentColorImg.color = _currentColor;
        print("hex: " + _currentColor.ToHex());
        if (!_inputingHex) _hexInput.UpdateText(_currentColor.ToHex());
    }

    private void StopSelecting()
    {
        _selector.enabled = false;
    }

    public void StartSelecting()
    {
        _inputingHex = false;
        _selector.enabled = true;   
    }

    private void CreateTextures()
    {
        CreateHueImg();
        CreateSatValTex();
    }

    private void CreateSatValTex()
    {
        int height = 16;
        _satValTex = new Texture2D(height, height);
        _satValTex.wrapMode = TextureWrapMode.Clamp;
        _satValTex.name = "SatValTex";
    }

    private void CreateHueImg()
    {
        float height = 16;
        _hueTex = new Texture2D(1, (int)height);
        _hueTex.wrapMode = TextureWrapMode.Clamp;
        _hueTex.name = "HueText";

        for (int i = 0; i < height; i++) {
            var col = Color.HSVToRGB(i / height, 1, _hueValue);
            _hueTex.SetPixel(0, i, col);
        }

        _hueTex.Apply();
        _hueImg.texture = _hueTex;
    }

    [ButtonMethod]
    private void UpdateSatVal()
    {
        if (_satValTex == null) CreateSatValTex();
        float height = _satValTex.height;
        for (int x = 0; x < _satValTex.width; x++) {
            for (int y = 0; y < _satValTex.height; y++) {
                var col = Color.HSVToRGB(_currentHue, x / height, y / height);
                _satValTex.SetPixel(x, y, col);
            }
        }
        _satValTex.Apply();
        _satValImg.texture = _satValTex;
    }
}
