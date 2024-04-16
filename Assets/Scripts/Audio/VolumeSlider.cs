using System;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeSlider : MonoBehaviour
    {
        private enum VolumeType
        {
            Master,
            Music,
            Ambience,
            Sfx
        }

        [Header("Type")] [SerializeField] private VolumeType volumeType;

        private Slider volumeSlider;

        private void Awake()
        {
            volumeSlider = GetComponentInChildren<Slider>();
        }

        private void Update()
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    volumeSlider.value = AudioManager.Instance.masterVolume;
                    break;
                case VolumeType.Music:
                    volumeSlider.value = AudioManager.Instance.musicVolume;
                    break;
                case VolumeType.Ambience:
                    volumeSlider.value = AudioManager.Instance.ambienceVolume;
                    break;
                case VolumeType.Sfx:
                    volumeSlider.value = AudioManager.Instance.SFXVolume;
                    break;
                default:
                    Debug.LogWarning("Volume Type not supported" + volumeType);
                    break;
            }
        }

        public void OnSliderValueChanged()
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    AudioManager.Instance.masterVolume = volumeSlider.value;
                    break;
                case VolumeType.Music:
                    AudioManager.Instance.musicVolume = volumeSlider.value;
                    break;
                case VolumeType.Ambience:
                    AudioManager.Instance.ambienceVolume = volumeSlider.value;
                    break;
                case VolumeType.Sfx:
                    AudioManager.Instance.SFXVolume = volumeSlider.value;
                    break;
                default:
                    Debug.LogWarning("Volume Type not supported" + volumeType);
                    break;
            }
        }
        
    }
}
