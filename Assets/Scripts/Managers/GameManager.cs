using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputController))]
public class GameManager : MonoBehaviour
{
    public static GameManager i;
    private void Awake() { i = this; }

    [SerializeField] GameObject _pauseMenu;
    [SerializeField] Fade _fade;
    [SerializeField] MusicPlayer _music;
    public Transform Camera;

    [Header("saving")]
    [SerializeField] private CharacterMetaController _character;
    [SerializeField] private string _saveFileName = "saves.txt";
    private string _path => Path.Combine(Application.streamingAssetsPath, _saveFileName);

    private void Update()
    {
        if (InputController.GetDown(Control.PAUSE)) TogglePause();
    }

    public void SaveCurrent()
    { 
        Directory.CreateDirectory(Path.GetDirectoryName(_path));
        File.WriteAllText(_path, _character.GetSaveString());
        print("saved sucessfully to: " + _path);
    }

    public void LoadFromSave()
    {
        var saveString = File.ReadAllText(_path);
        _character.LoadFromString(saveString);
        print("loaded sucessfully from: " + _path);
    }

    void TogglePause()
    {
        if (Time.timeScale == 0) Resume();
        else Pause();
    }


    private void Start()
    {
        _fade.Disappear();
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioManager.i.Resume();
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioManager.i.Pause();
    }

    [ButtonMethod]
    public void LoadMenu()
    {
        Resume();
        StartCoroutine(FadeThenLoadScene(0));
    }

    [ButtonMethod]
    public void EndGame()
    {
        Resume();
        StartCoroutine(FadeThenLoadScene(2));
    }

    IEnumerator FadeThenLoadScene(int num)
    {
        _fade.Appear(); 
        _music.FadeOutCurrent(_fade.FadeTime);
        yield return new WaitForSeconds(_fade.FadeTime + 0.5f);
        Destroy(AudioManager.i.gameObject);
        SceneManager.LoadScene(num);
    }

}
