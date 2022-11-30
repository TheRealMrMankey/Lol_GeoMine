using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiago_GeoMine
{
    public class SaveGame : MonoBehaviour
    {
        #region Temp

        // Lab
        public string currentRock;
        public bool hasDiscoveredIron;
        public bool hasDiscoveredSilicon;
        public bool hasDiscoveredAluminium;
        public bool hasDiscoveredCalcium;
        public bool hasDiscoveredIgneous;
        public bool hasDiscoveredSedimentary;
        public bool hasDiscoveredMetamorphic;
        public string[] igneousQuestions;
        public string[] sedimentaryQuestions;
        public string[] metamorphicQuestions;
        public string[] otherRocksQuestions;
        public string[] igneousAnswers;
        public string[] sedimentaryAnswers;
        public string[] metamorphicAnswers;
        public string[] otherRocksAnswers;
        public string[] igneousHint;
        public string[] sedimentaryHint;
        public string[] metamorphicHint;
        public string[] otherRocksHint;
        
        // Player
        public int money;
        public int pickaxeLvl;
        public int lanternLvl;
        public int armourLvl;
        public int totalRocks;
        public int silicon;
        public int iron;
        public int aluminium;
        public int calcium;
        public int igneous;
        public int sedimentary;
        public int metamorphic;
        
        // Tilemap              
        public Vector3Int[]  tilesLoc;
        public string[]  tilesType;

        #endregion

        private GameManager gameManager;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            //StartCoroutine(SaveProgress());
        }

        private IEnumerator SaveProgress()
        {
            while (true)
            {
                yield return new WaitForSeconds(35);

                gameManager.Save(
                // Lab
                    currentRock,
                    hasDiscoveredIron, hasDiscoveredSilicon, hasDiscoveredAluminium, hasDiscoveredCalcium,
                    hasDiscoveredIgneous, hasDiscoveredSedimentary, hasDiscoveredMetamorphic,
                    igneousQuestions, sedimentaryQuestions, metamorphicQuestions,
                    otherRocksQuestions, igneousAnswers, sedimentaryAnswers,
                    metamorphicAnswers, otherRocksAnswers, igneousHint,
                    sedimentaryHint, metamorphicHint, otherRocksHint,
                // Player
                    money, pickaxeLvl, lanternLvl, armourLvl,
                    totalRocks, silicon, iron, aluminium,
                    calcium, igneous, sedimentary, metamorphic,
                // Tilemap              
                    tilesLoc, tilesType
                );          
            }
        }
    }
}
