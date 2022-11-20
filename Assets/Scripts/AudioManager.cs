using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //makes sure there is only one of these in each scene
        if (instance == null){
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("bgm");    //automatically plays bgm
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    public bool IsPlaying (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return false;
        return s.source.isPlaying;
    }
}