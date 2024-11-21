using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(ColorMenuController))]
public class HairColorMenu : MonoBehaviour
{
    private ColorMenuController _controller;
    [SerializeField] private HairController _hairController;
    [SerializeField] private CheckBox _matchCheckBox;

    private bool _matching;

    private void OnEnable()
    {
        if (!_controller) _controller = GetComponent<ColorMenuController>();
        _controller.SetFromHexCode(_hairController.Current.GetSettings().Color.ToHex());
        _matching = _hairController.Current.GetSettings().MatchColor;
        _matchCheckBox.SetCheckedVisual(_matching);

    }

    public void SetMatch(bool match)
    {
        _hairController.Current.As<HairPiece>().SetMatch(match);
        if (match) _controller.SetFromHexCode(_hairController.HairColor.ToHex());
        _matching = match;
    }

    public void SetColor(Color color)
    {
        if (_matching) _hairController.SetHairColor(color);
        else _hairController.SetCurrentColor(color);
    }
}
