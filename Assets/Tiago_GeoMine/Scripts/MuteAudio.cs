using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class MuteAudio : MonoBehaviour
    {
        private float oldVolumeAmount;
        private bool isMuted;

        void Start()
        {         
            oldVolumeAmount = AudioListener.volume;
            isMuted = false;
        }

        public void Mute()
        {
            if (isMuted == false)
            {
                AudioListener.volume = 0;
                isMuted = true;
            }
            else if(isMuted == true)
            {
                AudioListener.volume = oldVolumeAmount;
                isMuted = false;
            }
        }
    }
}
