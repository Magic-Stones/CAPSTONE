using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    public static AudioAssets Instance { get; private set; }

    public AudioSource audioSFX;

    public AudioClip sfxPickUpItem;
    public AudioClip sfxOpenEntryway;
    public AudioClip sfxChallengeEnemy;
    public AudioClip sfxCorrectAnswer;
    public AudioClip sfxWrongAnswer;
    public AudioClip sfxGhostDefeat;
    public AudioClip sfxWinGame;
    public AudioClip sfxLoseGame;

    void Awake()
    {
        Instance = this;

        audioSFX = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
