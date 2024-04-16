using System;
using System.Collections.Generic;
using UnityEngine;
// using FMODUnity;
// using FMOD.Studio;

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

        /*private Bus masterBus;
        private Bus musicBus;
        private Bus SFXBus;
        private Bus ambienceBus;
        
        
        private List<EventInstance> eventInstances;
        private List<StudioEventEmitter> eventEmitters;

        private EventInstance ambientEventInstance;
        private EventInstance musicEventInstance;*/
        
        public static AudioManager Instance { get; private set; }

        /*private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one AudioManager in the Scene.");
            }

            Instance = this;

            eventInstances = new List<EventInstance>();
            eventEmitters = new List<StudioEventEmitter>();

            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
            SFXBus = RuntimeManager.GetBus("bus:/SFX");
        }

        private void Start()
        {
            InitializeAmbience(FMODEvents.Instance.ambience);
            InitializeMusic(FMODEvents.Instance.music);
        }

        private void Update()
        {
            masterBus.setVolume(masterVolume);
            musicBus.setVolume(musicVolume);
            SFXBus.setVolume(SFXVolume);
            ambienceBus.setVolume(ambienceVolume);
        }

        private void InitializeAmbience(EventReference ambienceEventReference)
        {
            ambientEventInstance = CreateEventInstance(ambienceEventReference);
            ambientEventInstance.start();
        }
        
        private void InitializeMusic(EventReference musicEventReference)
        {
            musicEventInstance = CreateEventInstance(musicEventReference);
            musicEventInstance.start();
        }

        public void SetAmbienceParameter(string parameterName, float parameterValue)
        {
            ambientEventInstance.setParameterByName(parameterName, parameterValue);
        }

        public void SetMusicArea(MusicArea area)
        {
            musicEventInstance.setParameterByName("area", (float)area);
        }
        
        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstances.Add(eventInstance);
            return eventInstance;
        }

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
        {
            StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = eventReference;
            eventEmitters.Add(emitter);
            return emitter;
        }

        private void CleanUp()
        {
            foreach (EventInstance eventInstance in eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }

            foreach (StudioEventEmitter emitter in eventEmitters)
            {
                emitter.Stop();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }*/
    }
}

