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
        public string labText;
        public string labText02;
        public string labText03;

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
        public string Igneous_Question;
        public string Sedimentary_Question;
        public string Metamorphic_Question_01;
        public string Metamorphic_Question_02;
        public string Other_Question_01;
        public string Other_Question_02;
        public string Other_Question_03;
        public string Other_Question_04;
        public string Other_Question_05;

        [Space(3)]
        public string Igneous_Hint;
        public string Sedimentary_Hint;
        public string Metamorphic_Hint_01;
        public string Metamorphic_Hint_02;
        public string Other_Hint_01;
        public string Other_Hint_02;
        public string Other_Hint_03;
        public string Other_Hint_04;
        public string Other_Hint_05;

        [Space(3)]
        public string Igneous_Answer;
        public string Sedimentary_Answer;
        public string Metamorphic_Answer_01;
        public string Metamorphic_Answer_02;
        public string Other_Answer_01;
        public string Other_Answer_02;
        public string Other_Answer_03;
        public string Other_Answer_04;
        public string Other_Answer_05;

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

            public string Igneous_Answer;
            public string Sedimentary_Answer;
            public string Metamorphic_Answer_01;
            public string Metamorphic_Answer_02;
            public string Other_Answer_01;
            public string Other_Answer_02;
            public string Other_Answer_03;
            public string Other_Answer_04;
            public string Other_Answer_05;
        }

        public GameText currentLanguage = new GameText();

        private string languageFile;
        private string fileText;

        #endregion

        public Lab labScript;

        void Awake()
        {         
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // Get Language file path
            languageFile = Application.dataPath + "/Tiago_GeoMine/Language/language_Game.json";
            ReadFile();

            // Get json values from file and set them as variables values of the class
            JSONNode startGamePlayload = JSON.Parse(fileText);
            currentLanguage = JsonUtility.FromJson<GameText>(startGamePlayload[gameManager.langCode].ToString());

            labText = labScript.Speech01;
            labText02 = labScript.Speech02;
            labText03 = labScript.Speech03;

            Igneous_Question = labScript.igneousQuestions[0];
            Sedimentary_Question = labScript.sedimentaryQuestions[0];
            Metamorphic_Question_01 = labScript.metamorphicQuestions[0];
            Metamorphic_Question_02 = labScript.metamorphicQuestions[1];
            Other_Question_01 = labScript.otherRocksQuestions[0];
            Other_Question_02 = labScript.otherRocksQuestions[1];
            Other_Question_03 = labScript.otherRocksQuestions[2];
            Other_Question_04 = labScript.otherRocksQuestions[3];
            Other_Question_05 = labScript.otherRocksQuestions[4];

            Igneous_Hint = labScript.igneousHint[0];
            Sedimentary_Hint = labScript.sedimentaryHint[0]; 
            Metamorphic_Hint_01 = labScript.metamorphicHint[0]; 
            Metamorphic_Hint_02 = labScript.metamorphicHint[1]; 
            Other_Hint_01 = labScript.otherRocksHint[0]; 
            Other_Hint_02 = labScript.otherRocksHint[1]; 
            Other_Hint_03 = labScript.otherRocksHint[2]; 
            Other_Hint_04 = labScript.otherRocksHint[3]; 
            Other_Hint_05 = labScript.otherRocksHint[4];

            Igneous_Answer = labScript.igneousAnswers[0];
            Sedimentary_Answer = labScript.sedimentaryAnswers[0];
            Metamorphic_Answer_01 = labScript.metamorphicAnswers[0];
            Metamorphic_Answer_02 = labScript.metamorphicAnswers[1];
            Other_Answer_01 = labScript.otherRocksAnswers[0];
            Other_Answer_02 = labScript.otherRocksAnswers[1];
            Other_Answer_03 = labScript.otherRocksAnswers[2];
            Other_Answer_04 = labScript.otherRocksAnswers[3];
            Other_Answer_05 = labScript.otherRocksAnswers[4];
    }

        void Start()
        {
            #region Set Text

            goToEntrance.text = currentLanguage.Button_Entrance;
            goToShop.text = currentLanguage.Button_Shop;
            goToLab.text = currentLanguage.Button_Lab;


            openBackpack.text = currentLanguage.Button_Backpack;
            closeBackpack.text = currentLanguage.Button_CloseBackpack;

            openTutorial.text = currentLanguage.Button_Tutorial;
            closeTutorial.text = currentLanguage.Button_CloseTutorial;

            for (int i = 0; i < buy.Length; i++)
            {
                buy[i].text = currentLanguage.Button_Buy;
            }

            for (int i = 0; i < sell.Length; i++)
            {
                sell[i].text = currentLanguage.Button_Sell;
            }

            openShop.text = currentLanguage.Button_OpenShop;
            closeShop.text = currentLanguage.Button_CloseShop;
            leaveShop.text = currentLanguage.Button_LeaveShop;

            openQuestions.text = currentLanguage.Button_OpenQuestions;
            closeQuestions.text = currentLanguage.Button_CloseQuestions;
            leaveLab.text = currentLanguage.Button_LeaveLab;
            submit.text = currentLanguage.Button_Submit;

            wakeUp.text = currentLanguage.Button_WakeUp;

            tutorialTitle.text = currentLanguage.Tutorial_Title;
            tutorialText01.text = currentLanguage.Tutorial_Text01;
            tutorialText02.text = currentLanguage.Tutorial_Text02;
            tutorialText03.text = currentLanguage.Tutorial_Text03;
            tutorialText04.text = currentLanguage.Tutorial_Text04;
            tutorialText05.text = currentLanguage.Tutorial_Text05;
            tutorialtext06.text = currentLanguage.Tutorial_Text06;

            for (int i = 0; i < 2; i++)
            {              
                upgrades[i].text = currentLanguage.Upgrades;
                rocksMinerals[i].text = currentLanguage.RocksAndMinerals;
            }

            shopText.text = currentLanguage.Shop_TextBubble;
            labScript.Speech01 = currentLanguage.Lab_TextBubble_01;
            labScript.Speech02 = currentLanguage.Lab_TextBubble_02;
            labScript.Speech03 = currentLanguage.Lab_TextBubble_03;

            playerDown.text = currentLanguage.Player_Down;

            correctMsg.text = currentLanguage.Correct_Message;
            failMsg.text = currentLanguage.Failed_Message;

            questionTitle.text = currentLanguage.Question_Title;
            hintTitle.text = currentLanguage.Hint_Title;
            inputfield.text = currentLanguage.InputField_Text;

            labScript.igneousQuestions[0] = currentLanguage.Igneous_Question;
            labScript.sedimentaryQuestions[0] = currentLanguage.Sedimentary_Question;
            labScript.metamorphicQuestions[0] = currentLanguage.Metamorphic_Question_01;
            labScript.metamorphicQuestions[1] = currentLanguage.Metamorphic_Question_02;
            labScript.otherRocksQuestions[0] = currentLanguage.Other_Question_01;
            labScript.otherRocksQuestions[1] = currentLanguage.Other_Question_02;
            labScript.otherRocksQuestions[2] = currentLanguage.Other_Question_03;
            labScript.otherRocksQuestions[3] = currentLanguage.Other_Question_04;
            labScript.otherRocksQuestions[4] = currentLanguage.Other_Question_05;

            labScript.igneousHint[0] = currentLanguage.Igneous_Hint;
            labScript.sedimentaryHint[0] = currentLanguage.Sedimentary_Hint;
            labScript.metamorphicHint[0] = currentLanguage.Metamorphic_Hint_01;
            labScript.metamorphicHint[1] = currentLanguage.Metamorphic_Hint_02;
            labScript.otherRocksHint[0] = currentLanguage.Other_Hint_01;
            labScript.otherRocksHint[1] = currentLanguage.Other_Hint_02;
            labScript.otherRocksHint[2] = currentLanguage.Other_Hint_03;
            labScript.otherRocksHint[3] = currentLanguage.Other_Hint_04;
            labScript.otherRocksHint[4] = currentLanguage.Other_Hint_05;

            labScript.igneousAnswers[0] = currentLanguage.Igneous_Answer;
            labScript.sedimentaryAnswers[0] = currentLanguage.Sedimentary_Answer;
            labScript.metamorphicAnswers[0] = currentLanguage.Metamorphic_Answer_01;
            labScript.metamorphicAnswers[1] = currentLanguage.Metamorphic_Answer_02;
            labScript.otherRocksAnswers[0] = currentLanguage.Other_Answer_01;
            labScript.otherRocksAnswers[1] = currentLanguage.Other_Answer_02;
            labScript.otherRocksAnswers[2] = currentLanguage.Other_Answer_03;
            labScript.otherRocksAnswers[3] = currentLanguage.Other_Answer_04;
            labScript.otherRocksAnswers[4] = currentLanguage.Other_Answer_05;

            #endregion
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
