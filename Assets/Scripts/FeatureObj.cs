using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeatureObj : MonoBehaviour
{
    [SerializeField] protected FeatureData Data;
    [SerializeField] protected bool ManualSet;
    [SerializeField, ConditionalField(nameof(ManualSet)), Range(0, 1)] protected float Hori = 0.5f;
    [SerializeField, ConditionalField(nameof(ManualSet)), Range(0, 1)] protected float Vert = 0.5f;
    [SerializeField, ConditionalField(nameof(ManualSet)), Range(0, 1)] protected float Size = 0.5f;
    [SerializeField, ConditionalField(nameof(ManualSet)), Range(0, 1)] protected float Angle = 0.5f;
    [SerializeField] protected Color Color = Color.blue;

    [HideInInspector] public bool IsMirroredVersion;

    protected FeatureObj MirroredFeature;
    public MirrorType Mirror => Data.Mirror;
    public Sprite GetIcon() => Data.Icon;
    public FeatureData GetData() => new FeatureData(Data);
    public Vector4 GetValues() => new Vector4(Hori, Vert, Size, Angle);
    public Color GetColor() => Color;

    public void ConfigureFromString(string inputString)
    {

        var numString = inputString.Substring(1);

        int mirrorMatchNum = int.Parse(inputString[0].ToString());
        Data.MatchColor = (mirrorMatchNum / 3) == 1;
        Data.Mirror = (MirrorType)(mirrorMatchNum % 3);

        Hori = float.Parse(numString.Substring(0, 3)) / 1000;
        Vert = float.Parse(numString.Substring(3, 3)) / 1000;
        Size = float.Parse(numString.Substring(6, 3)) / 1000;
        Angle = float.Parse(numString.Substring(9, 3)) / 1000;

        ColorUtility.TryParseHtmlString("#" + numString.Substring(12, 6), out Color);
        UpdateDisplay();

        SetColor(Color);
    }

    public override string ToString()
    {
        var result = Data.Icon.name + "~";
        result += (Convert.ToInt32(Data.MatchColor) * 3 + (int)Data.Mirror);
        //result += Data.Mirror.ToString() + Data.MatchColor.ToString();
        result += Round(Hori) + Round(Vert) + Round(Size) + Round(Angle) + Color.ToHex();

        result = result.Replace("True", "T");
        result = result.Replace("False", "F");
        result = result.Replace("LEFT", "L");
        result = result.Replace("BOTH", "B");
        result = result.Replace("RIGHT", "R");
        result = result.Replace("#", "");

        return result;
    }

    private string Round(float input )
    {
        return Mathf.FloorToInt(input * 1000).ToString();
    }

    protected virtual void UpdateDisplay()
    {
        if (!IsMirroredVersion) UpdateMirror();
    }

    public virtual void SetColor(Color color)
    {
        if (MirroredFeature != null) MirroredFeature.SetColor(color);
        color.a = 1;
        Color = color;
        UpdateDisplay();
    }

    public virtual void MirroredSet(float h, float v, float s, float a)
    {
        Angle = 1 - a;
        Hori = 1 - h;
        Vert = v;
        Size = s;
        UpdateDisplay();
    }
    protected void UpdateMirror()
    {
        if (!Application.isPlaying) return;

        if (MirroredFeature == null) SpawnMirror();

        gameObject.SetActive(Data.Mirror == MirrorType.LEFT || Data.Mirror == MirrorType.BOTH);
        MirroredFeature.gameObject.SetActive(Data.Mirror == MirrorType.RIGHT || Data.Mirror == MirrorType.BOTH);

        if (MirroredFeature) MirroredFeature.MirroredSet(Hori, Vert, Size, Angle);
    }

    private void OnDestroy()
    {
        if (MirroredFeature) Destroy(MirroredFeature.gameObject);
    }

    protected virtual void SpawnMirror()
    {
        MirroredFeature = Instantiate(gameObject, transform.parent).GetComponent<FeatureObj>();
        MirroredFeature.SetAsMirroredVersion();
    }

    public virtual void CopyTo(FeatureObj feature)
    {
        feature.SetAll(Hori, Vert, Size, Angle);
    }

    public virtual void SetAsMirroredVersion()
    {
        IsMirroredVersion = true;
    }

    public void SetMirror(MirrorType mirror)
    {
        if (Data.Mirror == mirror) return;
        Data.Mirror = mirror;
        UpdateDisplay();
    }

    public void SetAll(float hori, float vert, float size, float angle)
    {
        Hori = hori;
        Vert = vert;
        Size = size;
        Angle = angle;

        UpdateDisplay();
    }

    public void SetHori(float hori)
    {
        Hori = hori;
        UpdateDisplay();
    }

    public void SetVert(float vert)
    {
        Vert = vert;
        UpdateDisplay();
    }

    public void SetSize(float size)
    {
        Size = size;
        UpdateDisplay();
    }

    public void SetAngle(float angle)
    {
        Angle = angle;
        UpdateDisplay();
    }
}
