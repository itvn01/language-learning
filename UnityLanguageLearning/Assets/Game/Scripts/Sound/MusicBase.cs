using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;


    /// <summary>
    /// Music manager
    /// </summary>
    public class MusicBase : MonoBehaviour
    {
        private static MusicBase _instance;
        public static MusicBase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.Instantiate(Resources.Load<GameObject>("Singletons/MusicBase")).GetComponent<MusicBase>();
                return _instance;
            }
        }

        public AudioClip[] normalStageBgm;
        private AudioSource audioSource;
        public AudioMixer audioMixer;
        public AudioClip bonusGameBGM;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioMixer = audioSource.outputAudioMixerGroup.audioMixer;
            audioSource.loop = true;
            if (_instance == null || _instance == this)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);
        }

        public bool Mute
        {
            set
            {
                if (value == false)
                {
                    //Mute == false ? then restore the music volume to what it should be based on player prefs
                    RefreshMusicState();
                }
                else
                {   
                    //temporary mute:
                    audioMixer.SetFloat("MusicVolume", -80);
                }
            }
        }

        void Start()
        {
            RefreshMusicState();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        void RefreshMusicState()
        {
            bool musicOn = PlayerPrefs.GetInt(M1PetGame.PetGameConstants.SETTINGS_MUSIC_KEY, 1) >= 1;
            audioMixer.SetFloat("MusicVolume", musicOn ? 1 : -80);
        }

        public void PlaySettingsBGM()
        {
            //todo

        }

        public void PlayStageBGM()
        {
            audioSource.clip = normalStageBgm.Length > 0 ? normalStageBgm[Random.Range(0, normalStageBgm.Length)] : null;

            if (audioSource.clip == null)
                return;

            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayBGM(AudioClip audioClip)
        {
            audioSource.clip = audioClip;

            if (audioSource.clip == null)
                return;

            audioSource.loop = true;
            audioSource.Play();
        }

        public void StopBGM()
        {
            audioSource.Stop();
        }
    }

