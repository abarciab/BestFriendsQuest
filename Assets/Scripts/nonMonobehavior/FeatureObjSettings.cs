using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[System.Serializable]
public class FeatureObjSettings
{
    public MirrorType Mirror;
    public bool MatchColor = true;
    [Range(0, 1)] public float Vert = 0.5f;
    [Range(0, 1)] public float Hori = 0.5f;
    [Range(0, 1)] public float Size = 0.5f;
    [Range(0, 1)] public float Angle = 0.5f;
    public Color Color;

    public FeatureObjSettings() { }

    public FeatureObjSettings(FeatureObjSettings o)
    {
        Mirror = o.Mirror;
        MatchColor = o.MatchColor;
        Hori = o.Hori;
        Vert = o.Vert;
        Size = o.Size;
        Angle = o.Angle;
        Color = o.Color;
    }
}
