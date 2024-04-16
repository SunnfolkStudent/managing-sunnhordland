/*
using System;
using UnityEngine;

namespace Audio
{
    public class AmbienceChangeTrigger : MonoBehaviour
    {
        [Header("Parameter Change")] 
        [SerializeField] private string parameterName;
        [SerializeField] private float parameterValue;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                AudioManager.Instance.SetAmbienceParameter(parameterName, parameterValue);
            }
        }
    }
}
*/
