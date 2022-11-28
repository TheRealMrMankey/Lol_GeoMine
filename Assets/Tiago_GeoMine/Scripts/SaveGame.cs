using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class SaveGame : MonoBehaviour
    {
        private GameManager gameManager;
        public Lab lab;
        public PlayerController playerController;
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            StartCoroutine(SaveProgress());
        }

        private IEnumerator SaveProgress()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                gameManager.Save(
                // Lab
                    lab.currentRock,
                    lab.hasDiscoveredIron, lab.hasDiscoveredSilicon, lab.hasDiscoveredAluminium, lab.hasDiscoveredCalcium,
                    lab.hasDiscoveredIgneous, lab.hasDiscoveredSedimentary, lab.hasDiscoveredMetamorphic,
                    lab.igneousQuestions, lab.sedimentaryQuestions, lab.metamorphicQuestions,
                    lab.otherRocksQuestions, lab.igneousAnswers, lab.sedimentaryAnswers,
                    lab.metamorphicAnswers, lab.otherRocksAnswers, lab.igneousHint,
                    lab.sedimentaryHint, lab.metamorphicHint, lab.otherRocksHint,
                // Player
                    playerController.money, playerController.pickaxeLvl, playerController.lanternLvl, playerController.armourLvl,
                    playerController.totalRocks, playerController.silicon, playerController.iron, playerController.aluminium,
                    playerController.calcium, playerController.igneous, playerController.sedimentary, playerController.metamorphic,
                // Tilemap              
                    playerController.tilemap.GetTilesBlock(playerController.tilemap.cellBounds)
                );          
            }
        }
    }
}
