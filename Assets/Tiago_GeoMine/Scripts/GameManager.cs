using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using SimpleJSON;
using LoLSDK;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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

        // Tilemap
        //public TileBase[] tiles;
        public Vector3Int[] tilesLoc;
        public string[] tilesType;
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

        // Tilemap
        //public TileBase[] tiles;
        public Vector3Int[] tilesLoc;
        public string[] tilesType;
    }

#endregion

    public class GameManager : MonoBehaviour
    {
        [SerializeField, Header("Default State Data")] DefaultSaveData defaultData;
        [SerializeField, Header("Initial State Data")] public CurrentSaveData saveData;

        public TileBase[] tiles_;

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

        public TileBase[] allTiles;

        [System.Flags]
        enum LoLDataType
        {
            START = 0,
            LANGUAGE = 1 << 0
        }

        private PlayerController player;
        private Lab lab;

        private void OnLevelWasLoaded(int level)
        {
            if(level == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                lab = GameObject.FindGameObjectWithTag("Lab").GetComponent<Lab>();

                // Tilemap
                //player.tilemap.SetTilesBlock(player.tilemap.cellBounds, saveData.tiles);
                UpdateTiles();
                player.tilemap.SetTiles(saveData.tilesLoc, tiles_);
                player.GetTiles();
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

        public void UpdateTiles()
        {
            for (int i = 0; i < saveData.tilesType.Length; i++)
            {
                if (saveData.tilesType[i] == "Silicon")
                {
                    tiles_[i] = allTiles[6];
                    Debug.Log("Silicon");
                }
                if (saveData.tilesType[i] == "Iron")
                {
                    tiles_[i] = allTiles[3];
                    Debug.Log("Iron");
                }
                if (saveData.tilesType[i] == "Aluminium")
                {
                    tiles_[i] = allTiles[0];
                    Debug.Log("Aluminium");
                }
                if (saveData.tilesType[i] == "Calcium")
                {
                    tiles_[i] = allTiles[1];
                    Debug.Log("Calcium");
                }
                if (saveData.tilesType[i] == "Igneous")
                {
                    tiles_[i] = allTiles[2];
                    Debug.Log("Igneous");
                }
                if (saveData.tilesType[i] == "Sedimentary")
                {
                    tiles_[i] = allTiles[5];
                    Debug.Log("Sedimentary");
                }
                if (saveData.tilesType[i] == "Metamorphic")
                {
                    tiles_[i] = allTiles[4];
                    Debug.Log("Metamorphic");
                }
            }
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

            // Tilemap
            //saveData.tiles = defaultData.tiles;
            saveData.tilesLoc = defaultData.tilesLoc;
            saveData.tilesType = defaultData.tilesType;

            Debug.Log("Data reverted to default");

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

            // Lab
            lab.currentRock = saveData.currentRock;
            lab.hasDiscoveredIron = saveData.hasDiscoveredIron;
            lab.hasDiscoveredSilicon = saveData.hasDiscoveredSilicon;
            lab.hasDiscoveredAluminium = saveData.hasDiscoveredAluminium;
            lab.hasDiscoveredCalcium = saveData.hasDiscoveredCalcium;
            lab.hasDiscoveredIgneous = saveData.hasDiscoveredIgneous;
            lab.hasDiscoveredSedimentary = saveData.hasDiscoveredSedimentary;
            lab.hasDiscoveredMetamorphic = saveData.hasDiscoveredMetamorphic;
            lab.igneousQuestions = saveData.igneousQuestions;
            lab.sedimentaryQuestions = saveData.sedimentaryQuestions;
            lab.metamorphicQuestions = saveData.metamorphicQuestions;
            lab.otherRocksQuestions = saveData.otherRocksQuestions;
            lab.igneousAnswers = saveData.igneousAnswers;
            lab.sedimentaryAnswers = saveData.sedimentaryAnswers;
            lab.metamorphicAnswers = saveData.metamorphicAnswers;
            lab.otherRocksAnswers = saveData.otherRocksAnswers;
            lab.igneousHint = saveData.igneousHint;
            lab.sedimentaryHint = saveData.sedimentaryHint;
            lab.metamorphicHint = saveData.metamorphicHint;
            lab.otherRocksHint = saveData.otherRocksHint;

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

            // Tilemap
            //player.tilemap.SetTilesBlock(player.tilemap.cellBounds, saveData.tiles);
            player.tilemap.SetTiles(saveData.tilesLoc, tiles_);
        }

        public static void LoadLastSave<T>(System.Action<T> callback)
        {
            LOLSDK.Instance.LoadState<T>(state =>
            {
                callback(state.data);
                
                // Broadcast saved progress back to the teacher app.
                //LOLSDK.Instance.SubmitProgress(state.score, state.currentProgress, state.maximumProgress);
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
            string[] igneousQuestions, string[] sedimentaryQuestions, string[] metamorphicQuestions,
            string[] otherRocksQuestions, string[] igneousAnswers, string[] sedimentaryAnswers,
            string[] metamorphicAnswers, string[] otherRocksAnswers, string[] igneousHint,
            string[] sedimentaryHint, string[] metamorphicHint, string[] otherRocksHint,
            // Player
            int money, int pickaxeLvl, int lanternLvl, int armourLvl,
            int totalRocks, int silicon, int iron, int aluminium, int calcium, int igneous, int sedimentary, int metamorphic,
            // Tilemap
            /*TileBase[] tiles*/ Vector3Int[] tilesLoc, string[] tilesType
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

            // Tilemap
            //saveData.tiles = tiles;

            saveData.tilesLoc = null;
            saveData.tilesType = null;

            saveData.tilesLoc = tilesLoc;
            saveData.tilesType = tilesType;

            //UpdateTiles();

            MakeSave();

            Debug.Log("Data Saved");         
        }


        #endregion

        public void MakeSave()
        {
            LOLSDK.Instance.SaveState(saveData);
        }

#endregion
    }
}
