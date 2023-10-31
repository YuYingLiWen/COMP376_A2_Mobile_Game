
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip volumeMusic;

    private AudioSource source;

    private AudioMixer mixer;

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
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

    private static AudioManager instance;
    public static AudioManager Instance => instance;
}
