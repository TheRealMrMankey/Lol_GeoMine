using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using SimpleJSON;
using LoLSDK;
using UnityEngine.SceneManagement;

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

        public string igneousText;
        public string sedimentaryText;
        public string metamorphicText;
        public string[] otherRocksText;

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

        public string igneousText;
        public string sedimentaryText;
        public string metamorphicText;
        public string[] otherRocksText;

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
        [SerializeField, Header("Default State Data")] DefaultSaveData defaultData;
        [SerializeField, Header("Initial State Data")] public CurrentSaveData saveData;

        #region Language

        [Serializable]
        public class Language
        {
            public string NewGame;
            public string Continue;
        }

        public string langCode;
        public Language currentLanguage = new Language();

        public JSONNode langDefs;

        // Relative to Assets /StreamingAssets/
        private const string languageJSONFilePath = "language_Game.json";

        #endregion

        [System.Flags]
        enum LoLDataType
        {
            START = 0,
            LANGUAGE = 1 << 0
        }

        private PlayerController player;
        private Lab lab;
        private SetText text;

        public int maxProgress;
        public int currentProgress;

        private GameObject tutorialHelper;
        public bool isNewGame;

        private void OnLevelWasLoaded(int level)
        {
            if (level == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                lab = GameObject.FindGameObjectWithTag("Lab").GetComponent<Lab>();
                text = GameObject.FindGameObjectWithTag("Text").GetComponent<SetText>();   
                
                tutorialHelper = GameObject.FindGameObjectWithTag("TutorialHelper");

                if (isNewGame == true)
                    tutorialHelper.SetActive(true);
                else
                    tutorialHelper.SetActive(false);

                SetValues();
            }
        }

        void Awake()
        {
            // Create the WebGL (or mock) object
#if UNITY_EDITOR
            ILOLSDK webGL = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
        ILOLSDK webGL = new LoLSDK.WebGL();
#endif
            // Initialize the object, passing in the WebGL
            LOLSDK.Init(webGL, "50a8efef-c8fe-4188-bed4-83d67c6bcd25");         
            
            // Register event handlers
            LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(HandleStartGame);
            LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(HandleLanguageDefs);

            // Mock the platform-to-game messages when in the Unity editor.
#if UNITY_EDITOR
            LoadMockData();
#endif
            
            // Then, tell the platform the game is ready.
            LOLSDK.Instance.GameIsReady();  
        }

        void Start()
        {         
            DontDestroyOnLoad(this.gameObject);
        }

        public void UpdateProgress()
        {
            LOLSDK.Instance.SubmitProgress(currentProgress, currentProgress, maxProgress);

            // Complete the game
            if (currentProgress >= maxProgress)
                LOLSDK.Instance.CompleteGame();
        }

        // Start the game here
        void HandleStartGame(string json)
        {
            SharedState.StartGameData = JSON.Parse(json);
        }

        // Use language to populate UI
        void HandleLanguageDefs(string json)
        {
            langDefs = JSON.Parse(json);

#if UNITY_EDITOR         
            currentLanguage.NewGame = langDefs[langCode]["NewGame"];
            currentLanguage.Continue = langDefs[langCode]["Continue"];
#elif UNITY_WEBGL          
            currentLanguage.NewGame = langDefs["NewGame"];
            currentLanguage.Continue = langDefs["Continue"];      
#endif
            SharedState.LanguageDefs = langDefs;
        }

        private void LoadMockData()
        {
#if UNITY_EDITOR
            // Load Dev Language File from StreamingAssets
            string langFilePath = Path.Combine(Application.streamingAssetsPath, languageJSONFilePath);
            if (File.Exists(langFilePath))
            {
                string langDataAsJson = File.ReadAllText(langFilePath);
                
                HandleLanguageDefs(langDataAsJson);
            }
#endif
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

            saveData.igneousText = defaultData.igneousText;
            saveData.sedimentaryText = defaultData.sedimentaryText;
            saveData.metamorphicText = defaultData.metamorphicText;
            saveData.otherRocksText = defaultData.otherRocksText;

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
            
            currentProgress = 0;
            UpdateProgress();

            Debug.Log("Data reverted to default");

#endregion
        }

        public void SetValues()
        {
            #region Set Values

            if (saveData.hasDiscoveredSilicon == true)
                for (int i = 0; i < text.silicon.Length; i++)
                {
                    text.silicon[i].text = text.currentLanguage.Silicon;
                }
            //
            if (saveData.hasDiscoveredIron == true)
                for (int i = 0; i < text.iron.Length; i++)
                {
                    text.iron[i].text = text.currentLanguage.Iron;
                }
            //
            if (saveData.hasDiscoveredAluminium == true)
                for (int i = 0; i < text.aluminium.Length; i++)
                {
                    text.aluminium[i].text = text.currentLanguage.Aluminium;
                }
            //
            if (saveData.hasDiscoveredCalcium == true)
                for (int i = 0; i < text.calcium.Length; i++)
                {
                    text.calcium[i].text = text.currentLanguage.Calcium;
                }

            //
            if (saveData.hasDiscoveredIgneous == true)
                for (int i = 0; i < text.igneous.Length; i++)
                {
                    text.igneous[i].text = text.currentLanguage.Igneous;
                }
            //
            if (saveData.hasDiscoveredSedimentary == true)
                for (int i = 0; i < text.sedimentary.Length; i++)
                {
                    text.sedimentary[i].text = text.currentLanguage.Sedimentary;
                }
            //
            if (saveData.hasDiscoveredMetamorphic == true)
                for (int i = 0; i < text.metamorphic.Length; i++)
                {
                    text.metamorphic[i].text = text.currentLanguage.Metamorphic;
                }

            // Lab
            lab.currentRock = saveData.currentRock;
            lab.hasDiscoveredIron = saveData.hasDiscoveredIron;
            lab.hasDiscoveredSilicon = saveData.hasDiscoveredSilicon;
            lab.hasDiscoveredAluminium = saveData.hasDiscoveredAluminium;
            lab.hasDiscoveredCalcium = saveData.hasDiscoveredCalcium;
            lab.hasDiscoveredIgneous = saveData.hasDiscoveredIgneous;
            lab.hasDiscoveredSedimentary = saveData.hasDiscoveredSedimentary;
            lab.hasDiscoveredMetamorphic = saveData.hasDiscoveredMetamorphic;
            lab.igneousText = saveData.igneousText;
            lab.sedimentaryText = saveData.sedimentaryText;
            lab.metamorphicText = saveData.metamorphicText;
            lab.otherRocksText = saveData.otherRocksText;

            // Player
            player.money = saveData.money;
            player.pickaxeLvl = saveData.pickaxeLvl;
            player.lanternLvl = saveData.lanternLvl;
            player.armourLvl = saveData.armourLvl;
            player.totalRocks = saveData.totalRocks;
            player.silicon = saveData.silicon;
            player.iron = saveData.iron;
            player.aluminium = saveData.aluminium;
            player.calcium = saveData.calcium;
            player.igneous = saveData.igneous;
            player.sedimentary = saveData.sedimentary;
            player.metamorphic = saveData.metamorphic;

            #endregion
        }

        #region Loading saves

        public void StartLoading()
        {
            LoadLastSave<CurrentSaveData>(GetLastSave);
        }

        public void GetLastSave(CurrentSaveData lastSave)
        {      
            saveData = lastSave;

            SetValues();
        }

        public static void LoadLastSave<T>(System.Action<T> callback)
        {
            LOLSDK.Instance.LoadState<T>(state =>
            {
                callback(state.data);
            });         
        }

        #endregion

        #region Save Data

        public void Save
            ///
            (
            // Lab
            string currentRock,
            bool hasDiscoveredIron, bool hasDiscoveredSilicon, bool hasDiscoveredAluminium, bool hasDiscoveredCalcium,
            bool hasDiscoveredIgneous, bool hasDiscoveredSedimentary, bool hasDiscoveredMetamorphic,
            string igneousText, string sedimentaryText, string metamorphicText, string[] otherRocksText,

            // Player
            int money, int pickaxeLvl, int lanternLvl, int armourLvl,
            int totalRocks, int silicon, int iron, int aluminium, int calcium, int igneous, int sedimentary, int metamorphic
            )
            ///
        {          
            // Lab
            saveData.currentRock = currentRock;
            saveData.hasDiscoveredIron = hasDiscoveredIron;
            saveData.hasDiscoveredSilicon = hasDiscoveredSilicon;
            saveData.hasDiscoveredAluminium = hasDiscoveredAluminium;
            saveData.hasDiscoveredCalcium = hasDiscoveredCalcium;
            saveData.hasDiscoveredIgneous = hasDiscoveredIgneous;
            saveData.hasDiscoveredSedimentary = hasDiscoveredSedimentary;
            saveData.hasDiscoveredMetamorphic = hasDiscoveredMetamorphic;

            saveData.igneousText = igneousText;
            saveData.sedimentaryText = sedimentaryText;
            saveData.metamorphicText = metamorphicText;
            saveData.otherRocksText = otherRocksText;

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

            MakeSave();

            Debug.Log("Data Saved");         
        }


        #endregion

        public void MakeSave()
        {
            UpdateProgress();
            LOLSDK.Instance.SaveState(saveData);
        }

        #endregion
    }
}
