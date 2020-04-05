using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GUIManager : MonoBehaviour
{
    public static _GUIManager _instance = null;

    #region Public Editor properties

    #region Numbers GUI
    [Header("Numbers Mode Values")]
    public Text NumberToFind;
    public Text ShowResult;
    public Text PlayerScoreValue;
    public Text Level;
    public Text TimeLeftText;
    public Text GameModeText;
    public Text BonusTime;
    public List<Button> NumericAnswers;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (_instance == null)
            _instance = this;
        else
            if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
