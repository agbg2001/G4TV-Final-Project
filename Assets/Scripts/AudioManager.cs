using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

        if (instance == null){
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        //makes sure there is only one of these in each scene
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (SceneManager.GetActiveScene().name == "Menu" && !IsPlaying("titleTheme")){
            Stop("bgm");
            Stop("ending");
            Play("titleTheme"); //plays title theme on main menu
        }
        else if (SceneManager.GetActiveScene().name == "Ending" && !IsPlaying("ending")) {
            Stop("bgm");
            Stop("titleTheme");
            Play("ending");
        }
        else if (!(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Ending") && !IsPlaying("bgm")){
            Stop("titleTheme");
            Stop("ending");
            Play("bgm");    //automatically plays bgm in levels
        }
    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void Stop (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        }
        else {
            s.source.Stop();
        }
    }

    public bool IsPlaying (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return false;
        return s.source.isPlaying;
    }
}
