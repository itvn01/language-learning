using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using M1Game;
using UMExtensions;
/// <summary>
/// Sound manager
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundBase : MonoBehaviour
{
    private static SoundBase _instance;
    public static SoundBase Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Instantiate(Resources.Load<GameObject>("Singletons/SoundBase")).GetComponent<SoundBase>();
            return _instance;
        }
    }

    [Header("Audio Clips")]
    [SerializeField] public AudioClip click;

    [Header("Loop Sounds")]
    [SerializeField] List<SoundItem> _listSfxLoopSounds;

    [Header("AudioMixer")]
    [SerializeField] public AudioMixer audioMixer;

    private AudioSource _audioSource;
    List<AudioClip> clipsPlaying = new List<AudioClip>();
    private Dictionary<string, SoundItem> _sfxLoopSoundsDict = new Dictionary<string, SoundItem>();

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (audioMixer == null)
            audioMixer = _audioSource.outputAudioMixerGroup.audioMixer;
        if (_instance == null || _instance == this)
            _instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_listSfxLoopSounds.Count > 0)
        {
            //spawn loop sounds object
            var loopSoundsObj = new GameObject("LoopSounds", typeof(GameObject));
            loopSoundsObj.transform.parent = transform;

            _listSfxLoopSounds.ForEach(petSound =>
            {
                petSound.audioSource = loopSoundsObj.AddComponent<AudioSource>();
                petSound.audioSource.playOnAwake = false;
                petSound.audioSource.clip = petSound.clip;
                petSound.audioSource.loop = true;
                // sound.audioSource.mute = !isSoundOn;
                _sfxLoopSoundsDict.AddOrUpdate(petSound.name, petSound);
            });
        }
    }

    private void Start()
    {
        bool soundOn = PlayerPrefs.GetInt(M1Game.GameConstants.SETTINGS_SOUND_KEY, 1) >= 1;
        audioMixer.SetFloat("SoundVolume", soundOn ? 1 : -80);
    }

    public void RegisterSoundBase()
    {
        //Warning: please dont remove this function
        //SoundBase need initialized from the resource when enter the game, 
        //so it causes a delay when playing the sound.
        //So you need to call this function when entering gameplay to get initialized first.
        Debug.Log("RegisterSoundBase");
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        //Debug.Log("Play One Shot " + audioClip.name);
        _audioSource.PlayOneShot(audioClip);
    }

    public void PlaySoundsRandom(AudioClip[] clip)
    {
        if (clip.Length > 0)
            PlayOneShot(clip[Random.Range(0, clip.Length)]);
    }

    public void PlayLimitSound(AudioClip clip)
    {
        //Debug.Log("Play Limit Sound " + clip.name);
        if (clipsPlaying.IndexOf(clip) < 0)
        {
            clipsPlaying.Add(clip);
            PlayOneShot(clip);
            StartCoroutine(WaitForCompleteSound(clip));
        }
    }

    IEnumerator WaitForCompleteSound(AudioClip clip)
    {
        yield return new WaitForSeconds(0.2f);
        clipsPlaying.Remove(clipsPlaying.Find(x => clip));
    }

    //for loop sfx
    public void PlayPetSFX(string name, bool isLoop = true)
    {
        var petSound = _sfxLoopSoundsDict.GetOrDefault(name, null);
        if (petSound == null)
        {
            Debug.Log($"SFX: {name} not found");
            return;
        }

        petSound.audioSource.loop = isLoop;
        petSound.audioSource.Play();
    }

    public void StopPetSFX(string name)
    {
        var petSound = _sfxLoopSoundsDict.GetOrDefault(name, null);
        if (petSound == null)
        {
            Debug.Log($"SFX: {name} not found");
            return;
        }

        petSound.audioSource.Stop();
    }

    public void StopAllPetSounds()
    {
        _audioSource.Stop();
        _listSfxLoopSounds.ForEach(petSound =>
        {
            petSound.audioSource.Stop();
        });
    }

}

