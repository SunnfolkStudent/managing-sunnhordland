using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Volume")] 
        [Range(0, 1)] 
        public float masterVolume = 1;
        [Range(0, 1)] 
        public float musicVolume = 1;
        [Range(0, 1)] 
        public float ambienceVolume = 1;
        [Range(0, 1)] 
        public float SFXVolume = 1;

        private Bus _masterBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        private Bus _ambienceBus;
        
        
        private List<EventInstance> _eventInstances;
        private List<StudioEventEmitter> _eventEmitters;

        private EventInstance _ambientEventInstance;
        private EventInstance _musicEventInstance;
        
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one AudioManager in the Scene.");
            }

            Instance = this;

            _eventInstances = new List<EventInstance>();
            _eventEmitters = new List<StudioEventEmitter>();

            _masterBus = RuntimeManager.GetBus("bus:/");
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        }

        private void Start()
        {
            InitializeAmbience(FMODEvents.Instance.Ambience);
            InitializeMusic(FMODEvents.Instance.Music);
        }

        private void Update()
        {
            _masterBus.setVolume(masterVolume);
            _musicBus.setVolume(musicVolume);
            _sfxBus.setVolume(SFXVolume);
            _ambienceBus.setVolume(ambienceVolume);
        }

        private void InitializeAmbience(EventReference ambienceEventReference)
        {
            _ambientEventInstance = CreateEventInstance(ambienceEventReference);
            _ambientEventInstance.start();
        }
        
        private void InitializeMusic(EventReference musicEventReference)
        {
            _musicEventInstance = CreateEventInstance(musicEventReference);
            _musicEventInstance.start();
        }

        public void SetAmbienceParameter(string parameterName, float parameterValue)
        {
            _ambientEventInstance.setParameterByName(parameterName, parameterValue);
        }

        public void SetMusicArea(MusicArea area)
        {
            _musicEventInstance.setParameterByName("area", (float)area);
        }
        
        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            _eventInstances.Add(eventInstance);
            return eventInstance;
        }

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
        {
            StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = eventReference;
            _eventEmitters.Add(emitter);
            return emitter;
        }

        private void CleanUp()
        {
            foreach (EventInstance eventInstance in _eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }

            foreach (StudioEventEmitter emitter in _eventEmitters)
            {
                emitter.Stop();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}