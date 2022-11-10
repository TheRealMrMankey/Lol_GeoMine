using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiago_GeoMine;
using System;

namespace Tiago_GeoMine
{
    public class MuteAudio : MonoBehaviour
    {
        public GameObject muteImg;
        public GameObject unmuteImg;

        private float oldVolumeAmount;
        public bool isMuted;

        private void Awake()
        {
        }

        void Start()
        {          
            oldVolumeAmount = AudioListener.volume;
            
            if (isMuted == false)
            {
                muteImg.SetActive(false);
                unmuteImg.SetActive(true);
            }
            else if (isMuted == true)
            {
                muteImg.SetActive(true);
                unmuteImg.SetActive(false);
            }         
        }

        public void Mute()
        {
            if (isMuted == false)
            {
                AudioListener.volume = 0;
                isMuted = true;
                muteImg.SetActive(true);
                unmuteImg.SetActive(false);
            }
            else if(isMuted == true)
            {
                AudioListener.volume = oldVolumeAmount;
                isMuted = false;
                muteImg.SetActive(false);
                unmuteImg.SetActive(true);
            }
        }
    }
}
