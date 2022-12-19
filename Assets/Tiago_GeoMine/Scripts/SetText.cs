using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using SimpleJSON;
using System;
using LoLSDK;

namespace Tiago_GeoMine
{
    public class SetText : MonoBehaviour
    {      
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
        public TextMeshProUGUI openSubject;
        public TextMeshProUGUI closeSubject;
        public TextMeshProUGUI leaveLab;

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

        [Space(3)]
        public string Igneous_Text;
        public string Sedimentary_Text;
        public string Metamorphic_Text;
        public string Other_Text_01;
        public string Other_Text_02;
        public string Other_Text_03;
        public string Other_Text_04;
        public string Other_Text_05;

        [Space(3)]
        public string Igneous_Pic;
        public string Sedimentary_Pic;
        public string Metamorphic_Pic;
        public string Other_Pic_01;
        public string Other_Pic_02;
        public string Other_Pic_03;
        public string Other_Pic_04;
        public string Other_Pic_05;

        #endregion

        #region Language variables

        #region Game Text
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
            public string Button_OpenSubject;
            public string Button_CloseSubject;
            public string Button_LeaveLab;
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

            public string Igneous_Text;
            public string Sedimentary_Text;
            public string Metamorphic_Text;
            public string Other_Text_01;
            public string Other_Text_02;
            public string Other_Text_03;
            public string Other_Text_04;
            public string Other_Text_05;
        }
        #endregion

        public GameText currentLanguage = new GameText();

        [System.Flags]
        enum LoLDataType
        {
            LANGUAGE = 1 << 0
        }

        #endregion

        public GameManager gameManager;
        public Lab labScript;

        void Awake()
        {        
            labScript = GameObject.FindGameObjectWithTag("Lab").GetComponent<Lab>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            ChangeLanguage();

            #region Get Text for variables

            labText = labScript.Speech01;
            labText02 = labScript.Speech02;
            labText03 = labScript.Speech03;

            Igneous_Text = labScript.igneousText;
            Sedimentary_Text = labScript.sedimentaryText;
            Metamorphic_Text = labScript.metamorphicText;
            Other_Text_01 = labScript.otherRocksText[0];
            Other_Text_02 = labScript.otherRocksText[1];
            Other_Text_03 = labScript.otherRocksText[2];
            Other_Text_04 = labScript.otherRocksText[3];
            Other_Text_05 = labScript.otherRocksText[4];

            #endregion                
        }

        private void Start()
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

            openSubject.text = currentLanguage.Button_OpenSubject;
            closeSubject.text = currentLanguage.Button_CloseSubject;
            leaveLab.text = currentLanguage.Button_LeaveLab;

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

            #region Subjects Text

            labScript.igneousText = currentLanguage.Igneous_Text;
            labScript.sedimentaryText = currentLanguage.Sedimentary_Text;
            labScript.metamorphicText = currentLanguage.Metamorphic_Text;

            #region Other Rocks

            if (labScript.otherRocksText.Length == 5)
            {
                labScript.otherRocksText[0] = currentLanguage.Other_Text_01;
                labScript.otherRocksText[1] = currentLanguage.Other_Text_02;
                labScript.otherRocksText[2] = currentLanguage.Other_Text_03;
                labScript.otherRocksText[3] = currentLanguage.Other_Text_04;
                labScript.otherRocksText[4] = currentLanguage.Other_Text_05;
            }

            else if (labScript.otherRocksText.Length == 4)
            {
                if (labScript.otherRocksText[0].Equals("What does the rock cycle involve?") || labScript.otherRocksText[0].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[0].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[0].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[0].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[0].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[0].Equals("What is a change of state?") || labScript.otherRocksText[0].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[0].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[0].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[1].Equals("What does the rock cycle involve?") || labScript.otherRocksText[1].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[1].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[1].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[1].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[1].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[1].Equals("What is a change of state?") || labScript.otherRocksText[1].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[1].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[1].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[2].Equals("What does the rock cycle involve?") || labScript.otherRocksText[2].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[2].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[2].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[2].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[2].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[2].Equals("What is a change of state?") || labScript.otherRocksText[2].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[2].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[2].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[3].Equals("What does the rock cycle involve?") || labScript.otherRocksText[3].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[3] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[3].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[3].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[3] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[3].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[3].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[3] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[3].Equals("What is a change of state?") || labScript.otherRocksText[3].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[3] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[3].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[3].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                { 
                    labScript.otherRocksText[3] = currentLanguage.Other_Text_05;
                }
            }

            else if (labScript.otherRocksText.Length == 3)
            {
                if (labScript.otherRocksText[0].Equals("What does the rock cycle involve?") || labScript.otherRocksText[0].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[0].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[0].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[0].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[0].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[0].Equals("What is a change of state?") || labScript.otherRocksText[0].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[0].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[0].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[1].Equals("What does the rock cycle involve?") || labScript.otherRocksText[1].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[1].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[1].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[1].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[1].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[1].Equals("What is a change of state?") || labScript.otherRocksText[1].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[1].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[1].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[2].Equals("What does the rock cycle involve?") || labScript.otherRocksText[2].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[2].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[2].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[2].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[2].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[2].Equals("What is a change of state?") || labScript.otherRocksText[2].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[2].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[2].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[2] = currentLanguage.Other_Text_05;
                }
            }


            else if (labScript.otherRocksText.Length == 2)
            {
                if (labScript.otherRocksText[0].Equals("What does the rock cycle involve?") || labScript.otherRocksText[0].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[0].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[0].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[0].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[0].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[0].Equals("What is a change of state?") || labScript.otherRocksText[0].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[0].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[0].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_05;
                }
                //
                if (labScript.otherRocksText[1].Equals("What does the rock cycle involve?") || labScript.otherRocksText[1].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[1].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[1].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[1].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[1].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[1].Equals("What is a change of state?") || labScript.otherRocksText[1].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[1].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[1].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[1] = currentLanguage.Other_Text_05;
                }
            }

            else if (labScript.otherRocksText.Length == 1)
            {
                if (labScript.otherRocksText[0].Equals("What does the rock cycle involve?") || labScript.otherRocksText[0].Equals("¿En qué consiste el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_01;
                }
                else if (labScript.otherRocksText[0].Equals("What are the processes of the rock cycle?") || labScript.otherRocksText[0].Equals("¿Cuáles son los procesos del ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_02;
                }
                else if (labScript.otherRocksText[0].Equals("What is the rock cycle dependent on?") || labScript.otherRocksText[0].Equals("¿De qué depende el ciclo de las rocas?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_03;
                }
                else if (labScript.otherRocksText[0].Equals("What is a change of state?") || labScript.otherRocksText[0].Equals("¿Qué es un cambio de estado?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_04;
                }
                else if (labScript.otherRocksText[0].Equals("Why are folding, faulting, and breaking geologic processes?") || labScript.otherRocksText[0].Equals("¿Por qué son procesos geológicos el plegamiento, la falla y la rotura?"))
                {
                    labScript.otherRocksText[0] = currentLanguage.Other_Text_05;
                }
            }

            #endregion

            #endregion

            #endregion
        }

        // Use language to populate UI
        void ChangeLanguage()
        {
            #region Set Text
#if UNITY_EDITOR
            //currentLanguage = JsonUtility.FromJson<GameText>(langDefs[langCode].ToString());

            currentLanguage.Button_Entrance = gameManager.langDefs[gameManager.langCode]["Button_Entrance"];
            currentLanguage.Button_Shop = gameManager.langDefs[gameManager.langCode]["Button_Shop"]; 
            currentLanguage.Button_Lab = gameManager.langDefs[gameManager.langCode]["Button_Lab"];
            currentLanguage.Button_Backpack = gameManager.langDefs[gameManager.langCode]["Button_Backpack"];
            currentLanguage.Button_CloseBackpack = gameManager.langDefs[gameManager.langCode]["Button_CloseBackpack"];
            currentLanguage.Button_Tutorial = gameManager.langDefs[gameManager.langCode]["Button_Tutorial"];
            currentLanguage.Button_CloseTutorial = gameManager.langDefs[gameManager.langCode]["Button_CloseTutorial"];
            currentLanguage.Button_Buy = gameManager.langDefs[gameManager.langCode]["Button_Buy"];
            currentLanguage.Button_Sell = gameManager.langDefs[gameManager.langCode]["Button_Sell"];
            currentLanguage.Button_OpenShop = gameManager.langDefs[gameManager.langCode]["Button_OpenShop"];
            currentLanguage.Button_CloseShop = gameManager.langDefs[gameManager.langCode]["Button_CloseShop"];
            currentLanguage.Button_LeaveShop = gameManager.langDefs[gameManager.langCode]["Button_LeaveShop"];
            currentLanguage.Button_OpenSubject = gameManager.langDefs[gameManager.langCode]["Button_OpenQuestions"];
            currentLanguage.Button_CloseSubject = gameManager.langDefs[gameManager.langCode]["Button_CloseQuestions"];
            currentLanguage.Button_LeaveLab = gameManager.langDefs[gameManager.langCode]["Button_LeaveLab"];
            currentLanguage.Button_WakeUp = gameManager.langDefs[gameManager.langCode]["Button_WakeUp"];

            currentLanguage.Tutorial_Title = gameManager.langDefs[gameManager.langCode]["Tutorial_Title"];
            currentLanguage.Tutorial_Text01 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text01"];
            currentLanguage.Tutorial_Text02 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text02"];
            currentLanguage.Tutorial_Text03 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text03"];
            currentLanguage.Tutorial_Text04 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text04"];
            currentLanguage.Tutorial_Text05 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text05"];
            currentLanguage.Tutorial_Text06 = gameManager.langDefs[gameManager.langCode]["Tutorial_Text06"];

            currentLanguage.Silicon = gameManager.langDefs[gameManager.langCode]["Silicon"];
            currentLanguage.Iron = gameManager.langDefs[gameManager.langCode]["Iron"];
            currentLanguage.Aluminium = gameManager.langDefs[gameManager.langCode]["Aluminium"];
            currentLanguage.Calcium = gameManager.langDefs[gameManager.langCode]["Calcium"];
            currentLanguage.Igneous = gameManager.langDefs[gameManager.langCode]["Igneous"];
            currentLanguage.Sedimentary = gameManager.langDefs[gameManager.langCode]["Sedimentary"];
            currentLanguage.Metamorphic = gameManager.langDefs[gameManager.langCode]["Metamorphic"];

            currentLanguage.Upgrades = gameManager.langDefs[gameManager.langCode]["Upgrades"];
            currentLanguage.RocksAndMinerals = gameManager.langDefs[gameManager.langCode]["RocksAndMinerals"];

            currentLanguage.Shop_TextBubble = gameManager.langDefs[gameManager.langCode]["Shop_TextBubble"];
            currentLanguage.Lab_TextBubble_01 = gameManager.langDefs[gameManager.langCode]["Lab_TextBubble_01"];
            currentLanguage.Lab_TextBubble_02 = gameManager.langDefs[gameManager.langCode]["Lab_TextBubble_02"];
            currentLanguage.Lab_TextBubble_03 = gameManager.langDefs[gameManager.langCode]["Lab_TextBubble_03"];

            currentLanguage.Player_Down = gameManager.langDefs[gameManager.langCode]["Player_Down"];

            currentLanguage.Igneous_Text = gameManager.langDefs[gameManager.langCode]["Igneous_Question"];
            currentLanguage.Sedimentary_Text = gameManager.langDefs[gameManager.langCode]["Sedimentary_Question"];
            currentLanguage.Metamorphic_Text = gameManager.langDefs[gameManager.langCode]["Metamorphic_Question_01"];
            currentLanguage.Other_Text_01 = gameManager.langDefs[gameManager.langCode]["Other_Question_01"];
            currentLanguage.Other_Text_02 = gameManager.langDefs[gameManager.langCode]["Other_Question_02"];
            currentLanguage.Other_Text_03 = gameManager.langDefs[gameManager.langCode]["Other_Question_03"];
            currentLanguage.Other_Text_04 = gameManager.langDefs[gameManager.langCode]["Other_Question_04"];
            currentLanguage.Other_Text_05 = gameManager.langDefs[gameManager.langCode]["Other_Question_05"];
#elif UNITY_WEBGL
            //currentLanguage = JsonUtility.FromJson<GameText>(langDefs.ToString());

            currentLanguage.Button_Entrance = gameManager.langDefs["Button_Entrance"];
            currentLanguage.Button_Shop = gameManager.langDefs["Button_Shop"]; 
            currentLanguage.Button_Lab = gameManager.langDefs["Button_Lab"];
            currentLanguage.Button_Backpack = gameManager.langDefs["Button_Backpack"];
            currentLanguage.Button_CloseBackpack = gameManager.langDefs["Button_CloseBackpack"];
            currentLanguage.Button_Tutorial = gameManager.langDefs["Button_Tutorial"];
            currentLanguage.Button_CloseTutorial = gameManager.langDefs["Button_CloseTutorial"];
            currentLanguage.Button_Buy = gameManager.langDefs["Button_Buy"];
            currentLanguage.Button_Sell = gameManager.langDefs["Button_Sell"];
            currentLanguage.Button_OpenShop = gameManager.langDefs["Button_OpenShop"];
            currentLanguage.Button_CloseShop = gameManager.langDefs["Button_CloseShop"];
            currentLanguage.Button_LeaveShop = gameManager.langDefs["Button_LeaveShop"];
            currentLanguage.Button_OpenSubject = gameManager.langDefs["Button_OpenQuestions"];
            currentLanguage.Button_CloseSubject = gameManager.langDefs["Button_CloseQuestions"];
            currentLanguage.Button_LeaveLab = gameManager.langDefs["Button_LeaveLab"];
            currentLanguage.Button_WakeUp = gameManager.langDefs["Button_WakeUp"];

            currentLanguage.Tutorial_Title = gameManager.langDefs["Tutorial_Title"];
            currentLanguage.Tutorial_Text01 = gameManager.langDefs["Tutorial_Text01"];
            currentLanguage.Tutorial_Text02 = gameManager.langDefs["Tutorial_Text02"];
            currentLanguage.Tutorial_Text03 = gameManager.langDefs["Tutorial_Text03"];
            currentLanguage.Tutorial_Text04 = gameManager.langDefs["Tutorial_Text04"];
            currentLanguage.Tutorial_Text05 = gameManager.langDefs["Tutorial_Text05"];
            currentLanguage.Tutorial_Text06 = gameManager.langDefs["Tutorial_Text06"];

            currentLanguage.Silicon = gameManager.langDefs["Silicon"];
            currentLanguage.Iron = gameManager.langDefs["Iron"];
            currentLanguage.Aluminium = gameManager.langDefs["Aluminium"];
            currentLanguage.Calcium = gameManager.langDefs["Calcium"];
            currentLanguage.Igneous = gameManager.langDefs["Igneous"];
            currentLanguage.Sedimentary = gameManager.langDefs["Sedimentary"];
            currentLanguage.Metamorphic = gameManager.langDefs["Metamorphic"];

            currentLanguage.Upgrades = gameManager.langDefs["Upgrades"];
            currentLanguage.RocksAndMinerals = gameManager.langDefs["RocksAndMinerals"];

            currentLanguage.Shop_TextBubble = gameManager.langDefs["Shop_TextBubble"];
            currentLanguage.Lab_TextBubble_01 = gameManager.langDefs["Lab_TextBubble_01"];
            currentLanguage.Lab_TextBubble_02 = gameManager.langDefs["Lab_TextBubble_02"];
            currentLanguage.Lab_TextBubble_03 = gameManager.langDefs["Lab_TextBubble_03"];

            currentLanguage.Player_Down = gameManager.langDefs["Player_Down"];

            currentLanguage.Igneous_Text = gameManager.langDefs["Igneous_Question"];
            currentLanguage.Sedimentary_Text = gameManager.langDefs["Sedimentary_Question"];
            currentLanguage.Metamorphic_Text = gameManager.langDefs["Metamorphic_Question_01"];
            currentLanguage.Other_Text_01 = gameManager.langDefs["Other_Question_01"];
            currentLanguage.Other_Text_02 = gameManager.langDefs["Other_Question_02"];
            currentLanguage.Other_Text_03 = gameManager.langDefs["Other_Question_03"];
            currentLanguage.Other_Text_04 = gameManager.langDefs["Other_Question_04"];
            currentLanguage.Other_Text_05 = gameManager.langDefs["Other_Question_05"];
#endif
            #endregion
        }
    }
}
