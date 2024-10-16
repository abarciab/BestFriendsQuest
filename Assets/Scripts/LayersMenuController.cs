using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersMenuController : MonoBehaviour
{
    [SerializeField] private FaceFeatureController _faceController;
    [SerializeField] private GameObject _main;
    [SerializeField] private AddMenuController _addMenu;
    [Header("Main")]
    [SerializeField] private GameObject _layerPrefab;
    [SerializeField] private Transform _layerListParent;

    private List<Layer> _spawnedLayers = new List<Layer>();

    private void OnEnable()
    {
        _main.SetActive(true);
        _addMenu.gameObject.SetActive(false);
    }

    public void Duplicate(FacialFeature feature)
    {
        AddFeature(feature.GetData());
        _faceController.CopySettingsToCurrent(feature);
        _spawnedLayers[^1].SetMirror(feature.GetData().Mirror);
    }

    public void AddFeature(FeatureData feature)
    {
        var added = _faceController.AddFeature(feature);
        AddLayer(added);
        _spawnedLayers[^1].GetComponent<SelectableItem>().Select();


        if (_addMenu.gameObject.activeInHierarchy) {
            _main.SetActive(true);
            _addMenu.gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        BuildLayerList();
        _addMenu.BuildAddList(_faceController);
    }

    public void DeleteFeature(Layer layer, FacialFeature feature)
    {
        _spawnedLayers.Remove(layer);
        Destroy(layer.gameObject);
        _faceController.Delete(feature);
    }

    private void BuildLayerList()
    {
        foreach (var l in _spawnedLayers) Destroy(l.gameObject);
        _spawnedLayers.Clear();

        foreach (var feature in _faceController.CurrentFeatures) AddLayer(feature);
    }

    private void AddLayer(FacialFeature feature)
    {
        var newLayer = Instantiate(_layerPrefab, _layerListParent).GetComponent<Layer>();
        newLayer.transform.SetAsFirstSibling();
        newLayer.Initialize(feature);
        _spawnedLayers.Add(newLayer);
    }

    public void Select(int siblingIndex, FacialFeature feature)
    {
        foreach (var l in _spawnedLayers) l.GetComponent<SelectableItem>().SetState(l.transform.GetSiblingIndex() == siblingIndex);
        _faceController.Select(feature);
    }
}
