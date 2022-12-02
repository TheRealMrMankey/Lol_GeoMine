using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class SaveGame : MonoBehaviour
    {
        private GameManager gameManager;
        private PlayerController player;
        private Lab lab;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            lab = GameObject.FindGameObjectWithTag("Lab").GetComponent<Lab>();

            StartCoroutine(SaveProgress());
        }

        private IEnumerator SaveProgress()
        {
            while (true)
            {
                yield return new WaitForSeconds(20);

                ForceSave();         
            }
        }

        public void ForceSave()
        {
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
                    player.money, player.pickaxeLvl, player.lanternLvl, player.armourLvl,
                    player.totalRocks, player.silicon, player.iron, player.aluminium,
                    player.calcium, player.igneous, player.sedimentary, player.metamorphic
                );
        }
    }
}
