using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _LevelManager : MonoBehaviour
{
    AudioSource[] audios;

    [HideInInspector]
    AudioSource ButtonSelected;

    bool showPlayPanel = false;
    bool showDifficultyPanel = false;

    public GameObject PlayModesPanel;
    public GameObject DifficultySettingsPanel;

    // Start is called before the first frame update
    void Awake()
    {
        audios = GetComponents<AudioSource>();

        ButtonSelected = audios[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNumbersMode()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButtonSelected()
    {
        ButtonSelected.Play();
        
        showPlayPanel = !showPlayPanel;
        showDifficultyPanel = false;
        DifficultySettingsPanel.SetActive(showDifficultyPanel);

        PlayModesPanel.SetActive(showPlayPanel);
    }

    public void OptionsPanelSelected()
    {
        ButtonSelected.Play();

        showDifficultyPanel = !showDifficultyPanel;
        showPlayPanel = false;
        PlayModesPanel.SetActive(showPlayPanel);
        DifficultySettingsPanel.SetActive(showDifficultyPanel);
    }

    public void NumbersGameModeSelected()
    {
        ButtonSelected.Play();
        _GameManager._instance.currentGameMode = _GameManager.GameMode.Numbers;
        Invoke("LoadNumbersMode", 0.5f);

    }

    public void DifficultySelection(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();

        switch (buttonText.text)
        {
            case "Easy":
                _GameManager._instance.CurrentDiffulty = _GameManager.GameDifficulty.Easy;                
                break;
            case "Normal":
                _GameManager._instance.CurrentDiffulty = _GameManager.GameDifficulty.Normal;
                break;
            case "Hard":
                _GameManager._instance.CurrentDiffulty = _GameManager.GameDifficulty.Hard;                
                break;
            default:
                break;
        }
    }

}
