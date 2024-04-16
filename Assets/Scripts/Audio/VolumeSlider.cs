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

        private Slider _volumeSlider;

        private void Awake()
        {
            _volumeSlider = GetComponentInChildren<Slider>();
        }

        /*private void Update()
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    _volumeSlider.value = AudioManager.Instance.masterVolume;
                    break;
                case VolumeType.Music:
                    _volumeSlider.value = AudioManager.Instance.musicVolume;
                    break;
                case VolumeType.Ambience:
                    _volumeSlider.value = AudioManager.Instance.ambienceVolume;
                    break;
                case VolumeType.Sfx:
                    _volumeSlider.value = AudioManager.Instance.SFXVolume;
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
                    AudioManager.Instance.masterVolume = _volumeSlider.value;
                    break;
                case VolumeType.Music:
                    AudioManager.Instance.musicVolume = _volumeSlider.value;
                    break;
                case VolumeType.Ambience:
                    AudioManager.Instance.ambienceVolume = _volumeSlider.value;
                    break;
                case VolumeType.Sfx:
                    AudioManager.Instance.SFXVolume = _volumeSlider.value;
                    break;
                default:
                    Debug.LogWarning("Volume Type not supported" + volumeType);
                    break;
            }
        }*/
        
    }
}
