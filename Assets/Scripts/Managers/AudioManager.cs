using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> dayBackgroundMusics;
    [SerializeField] private List<AudioClip> nightBackgroundMusics;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip volumeMusic;

    private AudioSource source;

    private int nightIndex = 0, dayIndex = 0;

    private AudioMixer mixer;

    private void Awake()
    {

    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        mixer = Resources.Load("AudioMixer") as AudioMixer;

        print(mixer);
        // Assign Master to Audio Source
        var group = mixer.FindMatchingGroups("Master");
        source.outputAudioMixerGroup = group[0];
    }

    public void PlayNext() //TODO when has gameplay
    {
        Stop();

        // if night
        nightIndex++;

        // if day
        dayIndex++;


    }

    private void ChangeTrack(string sceneName)
    {
        Stop();

        switch(sceneName)
        {
            case SceneDirector.SceneNames.MAIN_MENU_SCENE:
                source.clip = mainMenuMusic;
                break;
        }
    }

    public void Play(string sceneName)
    {
        ChangeTrack(sceneName);

        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
