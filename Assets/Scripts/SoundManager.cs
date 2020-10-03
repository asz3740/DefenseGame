using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private AudioSource myAudio;

    public AudioClip sndAttack1;
    public AudioClip sndAttack2;
    public AudioClip sndAttack3;
    public AudioClip sndAttack4;
    public AudioClip sndBuild;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void Attack1()
    {
        myAudio.PlayOneShot(sndAttack1);
    }

    public void Attack2()
    {
        myAudio.PlayOneShot(sndAttack2);
    }

    public void Attack3()
    {
        myAudio.PlayOneShot(sndAttack3);
    }

    public void Build()
    {
        myAudio.PlayOneShot(sndBuild);
    }
}
