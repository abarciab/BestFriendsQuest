using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HairController : MonoBehaviour
{
    [SerializeField] private List<HairPiece> _pieces = new List<HairPiece>();
    [SerializeField] private int _currentIndex;
    [SerializeField] private Vector2 _limits;
    [SerializeField] private Vector2 _scaleLimits = new Vector2(0.2f, 1.8f);
    [SerializeField] private Transform _target;

    public void SetPosition(float hori, float vert)
    {
        var pos = _target.localPosition;
        var x = Mathf.Lerp(-_limits.x, _limits.x, hori);
        var z = Mathf.Lerp(-_limits.y, _limits.y, vert);
        pos.x = x;
        pos.z = z;
        _target.localPosition = pos;

        _pieces[_currentIndex].transform.LookAt(_target);
        _pieces[_currentIndex].UpdatePosition();
    }

    public void SetSize(float size)
    {
        _pieces[_currentIndex].transform.GetChild(0).GetChild(0).transform.localScale = Vector3.one * Mathf.Lerp(_scaleLimits.x, _scaleLimits.y, size);
    }

    public void SetAngle(float angle)
    {
        _pieces[_currentIndex].transform.GetChild(0).GetChild(0).localEulerAngles = Vector3.up * Mathf.Lerp(-180, 180, angle);
    }

    private void OnDrawGizmosSelected()
    {
        var center = _target.parent.position;
        center.y = _target.position.y;

        var size = 0.5f;
        var leftBottom = center + (Vector3.left * _limits.x) + (Vector3.back * _limits.y);
        Gizmos.DrawWireSphere(leftBottom, size);
        var leftTop = center + (Vector3.left * _limits.x) + (Vector3.forward * _limits.y);
        Gizmos.DrawWireSphere(leftTop, size);
        var rightBottom = center + (Vector3.right * _limits.x) + (Vector3.back * _limits.y);
        Gizmos.DrawWireSphere(rightBottom, size);
        var rightTop = center + (Vector3.right * _limits.x) + (Vector3.forward * _limits.y);
        Gizmos.DrawWireSphere(rightTop, size);
    }
}
