using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public enum FeatureSubType { BROWS, EYES, NOSE, LIPS, MISC, BANGS, BIG, STRANDS}
public enum FeatureType { FACE, HAIR, EAR }
public enum MirrorType { LEFT, BOTH, RIGHT}

[System.Serializable]
public class FeatureData
{
    [HideInInspector] public string Name;
    public FeatureType Type;
    [ConditionalField(nameof(Type), true, FeatureType.EAR)]public FeatureSubType SubType;

    public Sprite Icon;
    [ConditionalField(nameof(Type), false, FeatureType.HAIR)] public Mesh Mesh;
    [ConditionalField(nameof(Type), false, FeatureType.HAIR)] public bool MatchColor = true;

    [ConditionalField(nameof(Type), false, FeatureType.EAR)] public GameObject EarPrefab;
    [ConditionalField(nameof(Type), false, FeatureType.EAR)] public Vector2 AngleLimits;

    [ConditionalField(nameof(Type), false, FeatureType.FACE)] public Texture2D Texture;
    [ConditionalField(nameof(Type), false, FeatureType.FACE)] public Texture2D ColorMask;

    [ConditionalField(nameof(Type), true, FeatureType.HAIR)] public Vector2 HoriLimits;
    [ConditionalField(nameof(Type), true, FeatureType.HAIR)] public Vector2 VertLimits;
    public Vector2 SizeLimits;
    public MirrorType Mirror;

    public FeatureData() { }
    public FeatureData(FeatureData o)
    {
        Name = o.Name;
        Icon = o.Icon;

        Type = o.Type;
        SubType = o.SubType;

        Mesh = o.Mesh;
        Texture = o.Texture;
        ColorMask = o.ColorMask;

        HoriLimits = o.HoriLimits;
        VertLimits = o.VertLimits;
        SizeLimits = o.SizeLimits;
        Mirror = o.Mirror;
    }
}