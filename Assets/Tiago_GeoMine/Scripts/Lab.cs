using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LoLSDK;
using UnityEngine.UI;

namespace Tiago_GeoMine
{
    public class Lab : MonoBehaviour
    {
        #region Variables

        [Space(10)]
        [Header("UI and Player")]

        public GameObject labUI;
        public TextMeshProUGUI scientistSpeech;
        private PlayerController player;
        public GetInventoryValues backpack;
        private GameManager gameManager;

        public TextMeshProUGUI subjectBtn;
        public string currentRock = ""; // Value to Save
        public string Speech01;
        public string Speech02;
        public string Speech03;

        public int queue;

        // Rocks discovered
        [Space(10)]
        [Header("Discovered Rocks")]

        public bool hasDiscoveredIron; // Value to Save
        public bool hasDiscoveredSilicon; // Value to Save
        public bool hasDiscoveredAluminium; // Value to Save
        public bool hasDiscoveredCalcium; // Value to Save
        public bool hasDiscoveredIgneous; // Value to Save
        public bool hasDiscoveredSedimentary; // Value to Save
        public bool hasDiscoveredMetamorphic; // Value to Save

        // Rocks
        [Space(10)]
        [Header("Backpack Rocks Text")] 

        public TextMeshProUGUI[] silicon;
        public TextMeshProUGUI[] iron;
        public TextMeshProUGUI[] aluminium;
        public TextMeshProUGUI[] calcium;
        public TextMeshProUGUI[] igneous;
        public TextMeshProUGUI[] sedimentary;
        public TextMeshProUGUI[] metamorphic;

        // Text and Images
        [Space(10)]
        [Header("Text and Images")]

        public TextMeshProUGUI subjectText;
        public GameObject subjectImg;

        // All Questions
        [Space(10)]
        [Header("Teach Subjects")]

        public string igneousText; // Value to Save
        public string sedimentaryText; // Value to Save
        public string metamorphicText; // Value to Save
        public string[] otherRocksText; // Value to Save

        // All Answers
        [Space(10)]
        [Header("Pictures")]

        public Sprite igneousPic; // Value to Save
        public Sprite sedimentaryPic; // Value to Save
        public Sprite metamorphicPic; // Value to Save
        public Sprite[] otherRocksPics; // Value to Save

        public SetText setText;
        private SaveGame save;

        #endregion     

        void Start()
        {
            // Text
            setText = GameObject.FindGameObjectWithTag("Text").GetComponent<SetText>();

            // Save
            save = GameObject.FindGameObjectWithTag("Save").GetComponent<SaveGame>();

            // GameManager
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // Get Player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                

            // All GameObjects
            labUI.SetActive(false);

        }
      
        #region Open and Close

        public void OpenLabUI()
        {
            /*
            if (hasDiscoveredIron == true
            && hasDiscoveredSilicon == true
            && hasDiscoveredAluminium == true
            && hasDiscoveredCalcium == true
            && hasDiscoveredIgneous == true
            && hasDiscoveredSedimentary == true
            && hasDiscoveredMetamorphic == true)
            {
                scientistSpeech.text = Speech03;
                LOLSDK.Instance.SpeakText("Lab_TextBubble_03");
            }*/

            if (queue == 0)
            {
                if (player.silicon > 0 && silicon[0].text == "????")
                {
                    queue++;
                }
                if (player.iron > 0 && iron[0].text == "????")
                {
                    queue++;
                }
                if (player.aluminium > 0 && aluminium[0].text == "????")
                {
                    queue++;
                }
                if (player.calcium > 0 && calcium[0].text == "????")
                {
                    queue++;
                }
                if (player.igneous > 0 && igneous[0].text == "????")
                {
                    queue++;
                }
                if (player.sedimentary > 0 && sedimentary[0].text == "????")
                {
                    queue++;
                }
                if (player.metamorphic > 0 && metamorphic[0].text == "????")
                {
                    queue++;
                }            
            }

            // If the player has mined
            if (player.totalRocks > 0)
            {
                #region Choose Questions

                // Determine what rocks you have and what question to get
                if (player.silicon > 0 && silicon[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("silicon");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.iron > 0 && iron[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("iron");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.aluminium > 0 && aluminium[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("aluminium");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.calcium > 0 && calcium[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("calcium");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.igneous > 0 && igneous[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("igneous");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.sedimentary > 0 && sedimentary[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("sedimentary");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else if (player.metamorphic > 0 && metamorphic[0].text == "????")
                {
                    labUI.SetActive(true);
                    ChooseSubject("metamorphic");

                    subjectBtn.text = gameManager.langDefs["Button_NextSubject"];

                    queue--;
                }
                else
                {
                    scientistSpeech.text = Speech02;
                    LOLSDK.Instance.SpeakText("Lab_TextBubble_02");

                    CloseLabUI();
                }

                #endregion
            }
            else
            {
                scientistSpeech.text = Speech02;
                LOLSDK.Instance.SpeakText("Lab_TextBubble_02");

                CloseLabUI();
            }

            if (queue == 0)
            {
                subjectBtn.text = gameManager.langDefs["Button_CloseSubject"];
            }

            Submit();
        }

        public void CloseLabUI()
        {
            labUI.SetActive(false);
            queue = 0;
            Submit();
        }

        public void ExitLab()
        {
            this.gameObject.SetActive(false);
            labUI.SetActive(false);
            player.agent.isStopped = false;
            player.agent.SetDestination(player.transform.position);
        }

#endregion

        public void Submit()
        {
#region Other Rocks

            if (currentRock == "silicon" || currentRock == "iron" || currentRock == "aluminium" || currentRock == "calcium")
            {
                // Mark rock as discovered
                if (currentRock == "silicon" && hasDiscoveredSilicon == false)
                {
                    hasDiscoveredSilicon = true;
                    gameManager.currentProgress++;
                    gameManager.UpdateProgress();
                    save.ForceSave();

                    for (int i = 0; i < silicon.Length; i++)
                        silicon[i].text = setText.currentLanguage.Silicon;
                }
                if (currentRock == "iron" && hasDiscoveredIron == false)
                {
                    hasDiscoveredIron = true;
                    gameManager.currentProgress++;
                    gameManager.UpdateProgress();
                    save.ForceSave();

                    for (int i = 0; i < iron.Length; i++)
                        iron[i].text = setText.currentLanguage.Iron;
                }
                if (currentRock == "aluminium" && hasDiscoveredAluminium == false)
                {
                    hasDiscoveredAluminium = true;
                    gameManager.currentProgress++;
                    gameManager.UpdateProgress();
                    save.ForceSave();

                    for (int i = 0; i < aluminium.Length; i++)
                        aluminium[i].text = setText.currentLanguage.Aluminium;
                }
                if (currentRock == "calcium" && hasDiscoveredCalcium == false)
                {
                    hasDiscoveredCalcium = true;
                    gameManager.currentProgress++;
                    gameManager.UpdateProgress();
                    save.ForceSave();

                    for (int i = 0; i < calcium.Length; i++)
                        calcium[i].text = setText.currentLanguage.Calcium;
                }
            }

#endregion

#region Igneous

            if (currentRock == "igneous" && hasDiscoveredIgneous == false)
            {
                // Mark rock as discovered
                hasDiscoveredIgneous = true;
                gameManager.currentProgress++;
                gameManager.UpdateProgress();
                save.ForceSave();

                for (int i = 0; i < igneous.Length; i++)
                    igneous[i].text = setText.currentLanguage.Igneous;
            }

#endregion

#region Sedimentary

            if (currentRock == "sedimentary" && hasDiscoveredSedimentary == false)
            {
                // Mark rock as discovered
                hasDiscoveredSedimentary = true;
                gameManager.currentProgress++;
                gameManager.UpdateProgress();
                save.ForceSave();

                for (int i = 0; i < sedimentary.Length; i++)
                    sedimentary[i].text = setText.currentLanguage.Sedimentary;
            }

#endregion

#region Metamorphic

            if (currentRock == "metamorphic" && hasDiscoveredMetamorphic == false)
            {
                // Mark rock as discovered
                hasDiscoveredMetamorphic = true;
                gameManager.currentProgress++;
                gameManager.UpdateProgress();
                save.ForceSave();

                for (int i = 0; i < metamorphic.Length; i++)
                    metamorphic[i].text = setText.currentLanguage.Metamorphic;
            }

            #endregion
          
        }

#region Subjects

        void ChooseSubject(string rock)
        {          
            if(rock == "silicon")
            {
                subjectText.text = otherRocksText[2];
                subjectImg.GetComponent<Image>().sprite = (otherRocksPics[2]);
                currentRock = "silicon";
            }
            if (rock == "iron")
            {
                subjectText.text = otherRocksText[1];
                subjectImg.GetComponent<Image>().sprite = (otherRocksPics[1]);
                currentRock = "iron";
            }
            if (rock == "aluminium")
            {
                subjectText.text = otherRocksText[0];
                subjectImg.GetComponent<Image>().sprite = (otherRocksPics[0]);
                currentRock = "aluminium";
            }
            if (rock == "calcium")
            {
                subjectText.text = otherRocksText[3];
                subjectImg.GetComponent<Image>().sprite = (otherRocksPics[3]);
                currentRock = "calcium";
            }
            if (rock == "igneous")
            {
                subjectText.text = igneousText;
                subjectImg.GetComponent<Image>().sprite = igneousPic;
                currentRock = "igneous";
            }
            if (rock == "sedimentary")
            {
                subjectText.text = sedimentaryText;
                subjectImg.GetComponent<Image>().sprite = sedimentaryPic;
                currentRock = "sedimentary";
            }
            if (rock == "metamorphic")
            {
                subjectText.text = metamorphicText;
                subjectImg.GetComponent<Image>().sprite = metamorphicPic;
                currentRock = "metamorphic";
            }

            // Text-to-speech
            if(subjectText.text == otherRocksText[0])
                LOLSDK.Instance.SpeakText("Other_Text_01");
            if (subjectText.text == otherRocksText[1])
                LOLSDK.Instance.SpeakText("Other_Text_02");
            if (subjectText.text == otherRocksText[2])
                LOLSDK.Instance.SpeakText("Other_Text_03");
            if (subjectText.text == otherRocksText[3])
                LOLSDK.Instance.SpeakText("Other_Text_04");
            if (subjectText.text == igneousText)
                LOLSDK.Instance.SpeakText("Igneous_Text");
            if (subjectText.text == sedimentaryText)
                LOLSDK.Instance.SpeakText("Sedimentary_Text");
            if (subjectText.text == metamorphicText)
                LOLSDK.Instance.SpeakText("Metamorphic_Text");
        }

#endregion
    }
}
