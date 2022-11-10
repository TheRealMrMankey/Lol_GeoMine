using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using LoLSDK;

namespace Tiago_GeoMine
{
    #region Save Data

    [System.Serializable]
    public class DefaultSaveData
    {
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
    }

    [System.Serializable]
    public class CurrentSaveData
    {
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
    }

    #endregion

    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("Initial State Data")] DefaultSaveData defaultData;
        [SerializeField, Header("Initial State Data")] public CurrentSaveData saveData;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

#if UNITY_EDITOR
            //ILOLSDK sdk = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
           // ILOLSDK sdk = new LoLSDK.WebGL();
#endif

            //LOLSDK.Init(sdk, "com.legends-of-learning.unity.sdk.v5.3.example-cooking-game");
        }

#region Saves

        public void ResetGameData()
        {
#region Reset Save Data

            // Lab
            saveData.currentRock = defaultData.currentRock;
            saveData.hasDiscoveredIron = defaultData.hasDiscoveredIron;
            saveData.hasDiscoveredSilicon = defaultData.hasDiscoveredSilicon;
            saveData.hasDiscoveredAluminium = defaultData.hasDiscoveredAluminium;
            saveData.hasDiscoveredCalcium = defaultData.hasDiscoveredCalcium;
            saveData.hasDiscoveredIgneous = defaultData.hasDiscoveredIgneous;
            saveData.hasDiscoveredSedimentary = defaultData.hasDiscoveredSedimentary;
            saveData.hasDiscoveredMetamorphic = defaultData.hasDiscoveredMetamorphic;

            saveData.igneousQuestions = defaultData.igneousQuestions;
            saveData.sedimentaryQuestions = defaultData.sedimentaryQuestions;
            saveData.metamorphicQuestions = defaultData.metamorphicQuestions;
            saveData.otherRocksQuestions = defaultData.otherRocksQuestions;

            saveData.igneousAnswers = defaultData.igneousAnswers;
            saveData.sedimentaryAnswers = defaultData.sedimentaryAnswers;
            saveData.metamorphicAnswers = defaultData.metamorphicAnswers;
            saveData.otherRocksAnswers = defaultData.otherRocksAnswers;

            saveData.igneousHint = defaultData.igneousHint;
            saveData.sedimentaryHint = defaultData.sedimentaryHint;
            saveData.metamorphicHint = defaultData.metamorphicHint;
            saveData.otherRocksHint = defaultData.otherRocksHint;

            // Player
            saveData.money = defaultData.money;
            saveData.pickaxeLvl = defaultData.pickaxeLvl;
            saveData.lanternLvl = defaultData.lanternLvl;
            saveData.armourLvl = defaultData.armourLvl;

            saveData.totalRocks = defaultData.totalRocks;
            saveData.silicon = defaultData.silicon;
            saveData.iron = defaultData.iron;
            saveData.aluminium = defaultData.aluminium;
            saveData.calcium = defaultData.calcium;
            saveData.igneous = defaultData.igneous;
            saveData.sedimentary = defaultData.sedimentary;
            saveData.metamorphic = defaultData.metamorphic;

            Debug.Log("Data reverted to default");

#endregion
        }

        public void Save
            ///
            (
            // Lab
            string currentRock, 
            bool hasDiscoveredIron, bool hasDiscoveredSilicon, bool hasDiscoveredAluminium, bool hasDiscoveredCalcium,
            bool hasDiscoveredIgneous, bool hasDiscoveredSedimentary, bool hasDiscoveredMetamorphic,
            string[] igneousQuestions, string[] sedimentaryQuestions, string[] metamorphicQuestions,
            string[] otherRocksQuestions, string[] igneousAnswers, string[] sedimentaryAnswers,
            string[] metamorphicAnswers, string[] otherRocksAnswers, string[] igneousHint,
            string[] sedimentaryHint, string[] metamorphicHint, string[] otherRocksHint,
            // Player
            int money, int pickaxeLvl, int lanternLvl, int armourLvl,
            int totalRocks, int silicon, int iron, int aluminium, int calcium, int igneous, int sedimentary,int metamorphic
            )
            ///
        {
#region Save Data

            // Lab
            saveData.currentRock = currentRock;
            saveData.hasDiscoveredIron = hasDiscoveredIron;
            saveData.hasDiscoveredSilicon = hasDiscoveredSilicon;
            saveData.hasDiscoveredAluminium = hasDiscoveredAluminium;
            saveData.hasDiscoveredCalcium = hasDiscoveredCalcium;
            saveData.hasDiscoveredIgneous = hasDiscoveredIgneous;
            saveData.hasDiscoveredSedimentary = hasDiscoveredSedimentary;
            saveData.hasDiscoveredMetamorphic = hasDiscoveredMetamorphic;

            saveData.igneousQuestions = igneousQuestions;
            saveData.sedimentaryQuestions = sedimentaryQuestions;
            saveData.metamorphicQuestions = metamorphicQuestions;
            saveData.otherRocksQuestions = otherRocksQuestions;

            saveData.igneousAnswers = igneousAnswers;
            saveData.sedimentaryAnswers = sedimentaryAnswers;
            saveData.metamorphicAnswers = metamorphicAnswers;
            saveData.otherRocksAnswers = otherRocksAnswers;

            saveData.igneousHint = igneousHint;
            saveData.sedimentaryHint = sedimentaryHint;
            saveData.metamorphicHint = metamorphicHint;
            saveData.otherRocksHint = otherRocksHint;

            // Player
            saveData.money = money;
            saveData.pickaxeLvl = pickaxeLvl;
            saveData.lanternLvl = lanternLvl;
            saveData.armourLvl = armourLvl;

            saveData.totalRocks = totalRocks;
            saveData.silicon = silicon;
            saveData.iron = iron;
            saveData.aluminium = aluminium;
            saveData.calcium = calcium;
            saveData.igneous = igneous;
            saveData.sedimentary = sedimentary;
            saveData.metamorphic = metamorphic;

            //LoLSDK.

            Debug.Log("Data Saved");

#endregion
        }

#endregion
    }
}
