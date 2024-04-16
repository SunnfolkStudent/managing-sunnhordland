using System;
using UnityEngine;

namespace Audio
{
    public class MusicChangeTrigger : MonoBehaviour
    {
        [Header("Area")] 
        [SerializeField] private MusicArea area;

        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                AudioManager.Instance.SetMusicArea(area);
            }
        }*/
    }
}
