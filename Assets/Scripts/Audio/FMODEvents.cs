using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FMODEvents : MonoBehaviour
    {
        [field: Header("Ambience")]
        [field: SerializeField] public EventReference Ambience { get; private set; }
            
        [field: Header("Ambience")]
            
        [field: SerializeField] public EventReference Music { get; private set; }
            
        [field: Header("Player SFX")]
        [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }
            
        [field: Header("Coin SFX")]
        [field: SerializeField] public EventReference CoinCollected { get; private set; }
        [field: SerializeField] public EventReference CoinIdle { get; private set; }
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
