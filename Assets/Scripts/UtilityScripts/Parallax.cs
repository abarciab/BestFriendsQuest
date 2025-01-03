using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _parallaxAmount = 0.05f;
    private Transform _cam;
    private Vector3 _oldPos;

    private void Start()
    {
        _cam = GameManager.i.Camera;
        Invoke(nameof(SetPos), 0.01f);
    }

    private void SetPos() => _oldPos = _cam.position;

    private void Update()
    {
        var difference = _cam.position - _oldPos;
        var delta = Vector2.Lerp(Vector2.zero, (Vector2) difference, _parallaxAmount);
        transform.position += (Vector3)delta;
        _oldPos = _cam.position;
    }
}
