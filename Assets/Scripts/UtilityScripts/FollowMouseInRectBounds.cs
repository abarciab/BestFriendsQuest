using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseInRectBounds : MonoBehaviour
{
    [SerializeField] private RectTransform _bounds;
    private RectTransform _rTransform;

    private void OnEnable()
    {
        if (!_rTransform) _rTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void LateUpdate()
    {
        var pos = _rTransform.anchoredPosition;
        float width = _bounds.sizeDelta.x / 2;
        float height = _bounds.sizeDelta.y / 2;
        pos.x = Mathf.Clamp(pos.x, -width, width);
        pos.y = Mathf.Clamp(pos.y, -height, height);
        _rTransform.anchoredPosition = pos;
    }

    public Vector2 GetNormalizedPositionFromCenter()
    {
        float x = _rTransform.anchoredPosition.x / _bounds.sizeDelta.x;
        float y = _rTransform.anchoredPosition.y / _bounds.sizeDelta.y;

        return new Vector2(x + 0.5f, y + 0.5f);
    }

    public void SetPosition(Vector2 pos)
    {
        pos.x -= 0.5f;
        pos.y -= 0.5f;

        var x = pos.x * _bounds.sizeDelta.x;
        var y = pos.y * _bounds.sizeDelta.y;

        _rTransform.anchoredPosition = new Vector2(x, y);
    }
}
