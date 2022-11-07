using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class MuteAudio : MonoBehaviour
    {
        public GameObject muteImg;
        public GameObject unmuteImg;

        private float oldVolumeAmount;
        public bool isMuted; // Value to Save

        private CurrentSaveData currentSave;

        void Start()
        {
            currentSave = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CurrentSaveData>();
            isMuted = currentSave.isMuted;

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
