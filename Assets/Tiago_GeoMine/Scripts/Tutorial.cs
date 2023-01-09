using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject tutorialScreen;
        public GameObject openBtn;
        public GameObject playerUI;

        private PlayerController player;

        void Start()
        {
            // Get Player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        public void Open()
        {
            tutorialScreen.SetActive(true);
            openBtn.SetActive(false);
            playerUI.SetActive(false);

            // Make sure the player is stopped
            player.agent.isStopped = true;
            player.agent.SetDestination(player.transform.position);
        }

        public void Close()
        {
            tutorialScreen.SetActive(false);
            openBtn.SetActive(true);
            playerUI.SetActive(true);

            // Make sure the player is stopped
            player.agent.isStopped = false;
            player.agent.SetDestination(player.transform.position);
        }
    }
}
