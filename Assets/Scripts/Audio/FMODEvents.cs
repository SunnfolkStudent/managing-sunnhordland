using System;
using UnityEngine;
// using FMODUnity;

namespace Audio
{
    public class FMODEvents : MonoBehaviour
    {
        /*[field: Header("Ambience")]
        [field: SerializeField] public EventReference ambience { get; private set; }
        
        [field: Header("Ambience")]
        
        [field: SerializeField] public EventReference music { get; private set; }
        
        [field: Header("Player SFX")]
        [field: SerializeField] public EventReference playerFootsteps { get; private set; }
        
        [field: Header("Coin SFX")]
        [field: SerializeField] public EventReference coinCollected { get; private set; }
        [field: SerializeField] public EventReference coinIdle { get; private set; }*/
        public static FMODEvents Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one FMODEvents Instance in the Scene");
            }
            Instance = this;
        }
    }
}
