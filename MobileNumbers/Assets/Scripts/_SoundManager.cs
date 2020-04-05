using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SoundManager : MonoBehaviour
{
    public static _SoundManager _instance;

    AudioSource[] audios;

    // Number Mode Sounds
    [HideInInspector]
    public AudioSource NumbersBackgroundMusic;

    [HideInInspector]
    public AudioSource CorrectAnswer;

    [HideInInspector]
    public AudioSource WrongAnswer;

    [HideInInspector]
    public AudioSource BubbleSound;

    // Start is called before the first frame update
    void Awake()
    {
        Singleton();

        audios = GetComponents<AudioSource>();

        // Numbers Mode
        NumbersBackgroundMusic = audios[0];
        CorrectAnswer = audios[1];
        WrongAnswer = audios[2];
        BubbleSound = audios[3];
    }

    private void Start()
    {
        //switch (_GameManager._instance.currentGameMode)
        //{
        //    case _GameManager.GameMode.Numbers:
        //        NumbersBackgroundMusic.Play();
        //        break;
        //    default:
        //        break;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
