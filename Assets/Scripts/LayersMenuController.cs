using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersMenuController : MonoBehaviour
{
    [SerializeField, OverrideLabel("Feature Controller")] private GameObject _featureControllerMB;
    [SerializeField] private GameObject _main;
    [SerializeField] private AddMenuController _addMenu;
    [Header("Main")]
    [SerializeField] private GameObject _layerPrefab;
    [SerializeField] private Transform _layerListParent;

    private List<Layer> _spawnedLayers = new List<Layer>();

    private IFeatureController _featureController;
    
    private void OnEnable()
    {
        _main.SetActive(true);
        _addMenu.gameObject.SetActive(false);
    }

    public void Duplicate(IFeatureObj original)
    {
        AddFeature(original.GetData());
        _featureController.CopySettingsToCurrent(original);
        _spawnedLayers[^1].SetMirror(original.GetData().Mirror);
    }

    public void AddFeature(FeatureData data)
    {
        var added = _featureController.AddFeature(data);
        AddLayer(added);
        _spawnedLayers[^1].GetComponent<SelectableItem>().Select();

        if (_addMenu.gameObject.activeInHierarchy) {
            _main.SetActive(true);
            _addMenu.gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        _featureController = _featureControllerMB.GetComponent<IFeatureController>();
        BuildLayerList();
        _addMenu.BuildAddList(_featureController);
    }

    public void DeleteFeature(Layer layer, object feature)
    {
        _spawnedLayers.Remove(layer);
        Destroy(layer.gameObject);
        _featureController.Delete(feature);
    }

    private void BuildLayerList()
    {
        foreach (var l in _spawnedLayers) Destroy(l.gameObject);
        _spawnedLayers.Clear();

        foreach (var feature in _featureController.GetAllFeatures()) AddLayer(feature);
    }

    private void AddLayer(IFeatureObj feature)
    {
        var newLayer = Instantiate(_layerPrefab, _layerListParent).GetComponent<Layer>();
        newLayer.transform.SetAsFirstSibling();
        newLayer.Initialize(feature);
        _spawnedLayers.Add(newLayer);
    }

    public void Select(int siblingIndex, object feature)
    {
        foreach (var l in _spawnedLayers) l.GetComponent<SelectableItem>().SetState(l.transform.GetSiblingIndex() == siblingIndex);
        _featureController.Select(feature);
    }
}
