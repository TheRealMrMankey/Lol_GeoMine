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

        public TextMeshProUGUI pickaxeLvl;
        public TextMeshProUGUI lanternLvl;
        public TextMeshProUGUI armourLvl;

        // Rocks and Minerals
        [Space(10)]
        [Header("Rocks and Minerals")]

        public TextMeshProUGUI silicon;
        public TextMeshProUGUI iron;
        public TextMeshProUGUI aluminium;
        public TextMeshProUGUI calcium;
        public TextMeshProUGUI igneous;
        public TextMeshProUGUI sedimentary;
        public TextMeshProUGUI metamorphic;

        #endregion

        void Start()
        {
            // Grab Player script
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            // Get all upgrades and rocks values
            UpdateBackpack();
        }

        public void UpdateBackpack()
        {
            // Upgrades    
            pickaxeLvl.text = playerController.pickaxeLvl.ToString();
            lanternLvl.text = playerController.lanternLvl.ToString();
            armourLvl.text = playerController.armourLvl.ToString();

            // Rocks and Minerals
            silicon.text = playerController.silicon.ToString();
            iron.text = playerController.iron.ToString();
            aluminium.text = playerController.aluminium.ToString();
            calcium.text = playerController.calcium.ToString();
            igneous.text = playerController.igneous.ToString();
            sedimentary.text = playerController.sedimentary.ToString();
            metamorphic.text = playerController.metamorphic.ToString();
        }
    }
}
