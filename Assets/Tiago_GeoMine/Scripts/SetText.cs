using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using SimpleJSON;
using System;

namespace Tiago_GeoMine
{
    public class SetText : MonoBehaviour
    {
        private GameManager gameManager;

        #region Buttons

        [Space(5), Header("Fast Travel")]
        public TextMeshProUGUI goToEntrance;
        public TextMeshProUGUI goToShop;
        public TextMeshProUGUI goToLab;

        [Space(5), Header("Backpack")]
        public TextMeshProUGUI openBackpack;
        public TextMeshProUGUI closeBackpack;

        [Space(5), Header("Tutorial")]
        public TextMeshProUGUI openTutorial;
        public TextMeshProUGUI closeTutorial;

        [Space(5), Header("Shop")]
        public TextMeshProUGUI[] buy;
        public TextMeshProUGUI[] sell;
        public TextMeshProUGUI openShop;
        public TextMeshProUGUI closeShop;
        public TextMeshProUGUI leaveShop;

        [Space(5), Header("Lab")]
        public TextMeshProUGUI openQuestions;
        public TextMeshProUGUI closeQuestions;
        public TextMeshProUGUI leaveLab;
        public TextMeshProUGUI submit;

        [Space(5), Header("Death")]
        public TextMeshProUGUI wakeUp;

        #endregion

        #region Text

        [Space(5), Header("Tutorial")]
        public TextMeshProUGUI tutorialTitle;
        public TextMeshProUGUI tutorialText01;
        public TextMeshProUGUI tutorialText02;
        public TextMeshProUGUI tutorialText03;
        public TextMeshProUGUI tutorialText04;
        public TextMeshProUGUI tutorialText05;
        public TextMeshProUGUI tutorialtext06;

        [Space(5), Header("Backpack and Shop")]
        public TextMeshProUGUI[] silicon;
        public TextMeshProUGUI[] iron;
        public TextMeshProUGUI[] aluminium;
        public TextMeshProUGUI[] calcium;
        public TextMeshProUGUI[] igneous;
        public TextMeshProUGUI[] sedimentary;
        public TextMeshProUGUI[] metamorphic;
        public TextMeshProUGUI[] upgrades;
        public TextMeshProUGUI[] rocksMinerals;

        [Space(5), Header("Text Bubbles")]
        public TextMeshProUGUI shopText;
        public TextMeshProUGUI labText;

        [Space(5), Header("Death")]
        public TextMeshProUGUI playerDown;

        [Space(5), Header("Response")]
        public TextMeshProUGUI correctMsg;
        public TextMeshProUGUI failMsg;

        [Space(5), Header("Question and Hints")]
        public TextMeshProUGUI questionTitle;
        public TextMeshProUGUI hintTitle;
        public TextMeshProUGUI inputfield;

        [Space(3)]
        public TextMeshProUGUI question;

        [Space(3)]
        public TextMeshProUGUI hint;

        #endregion

        #region Language variables

        [Serializable]
        public class GameText
        {
            public string Button_Entrance;
            public string Button_Shop;
            public string Button_Lab;
            public string Button_Backpack;
            public string Button_CloseBackpack;
            public string Button_Tutorial;
            public string Button_CloseTutorial;
            public string Button_Buy;
            public string Button_Sell;
            public string Button_OpenShop;
            public string Button_CloseShop;
            public string Button_LeaveShop;
            public string Button_OpenQuestions;
            public string Button_CloseQuestions;
            public string Button_LeaveLab;
            public string Button_Submit;
            public string Button_WakeUp;

            public string Tutorial_Title;
            public string Tutorial_Text01;
            public string Tutorial_Text02;
            public string Tutorial_Text03;
            public string Tutorial_Text04;
            public string Tutorial_Text05;
            public string Tutorial_Text06;

            public string Silicon;
            public string Iron;
            public string Aluminium;
            public string Calcium;
            public string Igneous;
            public string Sedimentary;
            public string Metamorphic;

            public string Upgrades;
            public string RocksAndMinerals;

            public string Shop_TextBubble;
            public string Lab_TextBubble_01;
            public string Lab_TextBubble_02;
            public string Lab_TextBubble_03;

            public string Player_Down;

            public string Correct_Message;
            public string Failed_Message;

            public string Question_Title;
            public string Hint_Title;
            public string InputField_Text;

            public string Igneous_Question;
            public string Sedimentary_Question;
            public string Metamorphic_Question_01;
            public string Metamorphic_Question_02;
            public string Other_Question_01;
            public string Other_Question_02;
            public string Other_Question_03;
            public string Other_Question_04;
            public string Other_Question_05;

            public string Igneous_Hint;
            public string Sedimentary_Hint;
            public string Metamorphic_Hint_01;
            public string Metamorphic_Hint_02;
            public string Other_Hint_01;
            public string Other_Hint_02;
            public string Other_Hint_03;
            public string Other_Hint_04;
            public string Other_Hint_05;
        }

        public string langCode;

        public GameText currentLanguage = new GameText();

        private string languageFile;
        private string fileText;

        #endregion

        void Awake()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // Get Language file path
            languageFile = Application.dataPath + "/Tiago_GeoMine/Language/language_Game.json";
            ReadFile();

            // Get json values from file and set them as variables values of the class
            JSONNode startGamePlayload = JSON.Parse(fileText);
            currentLanguage = JsonUtility.FromJson<GameText>(startGamePlayload[langCode].ToString());
        }

        #region Read File

        public void ReadFile()
        {
            if (File.Exists(languageFile))
            {
                Debug.Log("File located");

                fileText = File.ReadAllText(languageFile);
            }
            else
                Debug.Log("File not located");
        }

        #endregion
    }
}
