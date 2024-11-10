using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFeatureController
{
    public void CopySettingsToCurrent(object original);
    public IFeatureObj AddFeature(FeatureData data);
    public void Delete(object feature);
    public List<IFeatureObj> GetAllFeatures();
    public void Select(object feature);
    public List<FeatureData> GetAllOptions();
}

public interface IFeatureObj
{
    public Sprite GetIcon();
    public bool IsMirror();
    public void SetMirror(bool mirror);
    public FeatureData GetData();
}
