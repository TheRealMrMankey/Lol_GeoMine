using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tiago_GeoMine
{
    public class GetInventoryValues : MonoBehaviour
    {
        #region Variables

        // Player Script
        PlayerController playerController;

        // Upgrades    
        [Space(10)]
        [Header("Upgrades")]

        public TextMeshProUGUI[] pickaxeLvl; 
        public TextMeshProUGUI[] lanternLvl;
        public TextMeshProUGUI[] armourLvl;

        // Rocks and Minerals
        [Space(10)]
        [Header("Rocks and Minerals")]

        public TextMeshProUGUI[] silicon;
        public TextMeshProUGUI[] iron;
        public TextMeshProUGUI[] aluminium;
        public TextMeshProUGUI[] calcium;
        public TextMeshProUGUI[] igneous;
        public TextMeshProUGUI[] sedimentary;
        public TextMeshProUGUI[] metamorphic;

        #endregion

        void Start()
        {
            // Grab Player script
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            // Get all upgrades and rocks values
            UpdateBackpack();
        }

        public void UpdateBackpack() // And Shop
        {
            for (int i = 0; i < 2; i++)
            {
                // Upgrades    
                pickaxeLvl[i].text = playerController.pickaxeLvl.ToString();
                lanternLvl[i].text = playerController.lanternLvl.ToString();
                armourLvl[i].text = playerController.armourLvl.ToString();

                // Rocks and Minerals
                silicon[i].text = playerController.silicon.ToString();
                iron[i].text = playerController.iron.ToString();
                aluminium[i].text = playerController.aluminium.ToString();
                calcium[i].text = playerController.calcium.ToString();
                igneous[i].text = playerController.igneous.ToString();
                sedimentary[i].text = playerController.sedimentary.ToString();
                metamorphic[i].text = playerController.metamorphic.ToString();
            }
        }
    }
}
