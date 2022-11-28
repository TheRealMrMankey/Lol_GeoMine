using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LoLSDK;

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
       
        public string currentRock = ""; // Value to Save
        public string Speech01;
        public string Speech02;
        public string Speech03;

        //Player Input
        [Space(10)]

        public TMP_InputField playerResponse;

        // Rocks discovered
        [Space(10)]
        [Header("Discovered Rocks")]

        public bool hasDiscoveredIron = false; // Value to Save
        public bool hasDiscoveredSilicon = false; // Value to Save
        public bool hasDiscoveredAluminium = false; // Value to Save
        public bool hasDiscoveredCalcium = false; // Value to Save
        public bool hasDiscoveredIgneous = false; // Value to Save
        public bool hasDiscoveredSedimentary = false; // Value to Save
        public bool hasDiscoveredMetamorphic = false; // Value to Save

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

        // Quiz
        [Space(10)]
        [Header("Quiz")]

        public TextMeshProUGUI question;
        public TextMeshProUGUI hint;
        public GameObject hintGO;

        // All Questions
        [Space(10)]
        [Header("Questions")]

        public string[] igneousQuestions; // Value to Save
        public string[] sedimentaryQuestions; // Value to Save
        public string[] metamorphicQuestions; // Value to Save
        public string[] otherRocksQuestions; // Value to Save
        private int questionNr; 

        // All Answers
        [Space(10)]
        [Header("Answers")]
        public string[] igneousAnswers; // Value to Save
        public string[] sedimentaryAnswers; // Value to Save
        public string[] metamorphicAnswers; // Value to Save
        public string[] otherRocksAnswers; // Value to Save

        // All Hints
        [Space(10)]
        [Header("Hints")]
        public string[] igneousHint; // Value to Save
        public string[] sedimentaryHint; // Value to Save
        public string[] metamorphicHint; // Value to Save
        public string[] otherRocksHint; // Value to Save

        // Result
        [Space(10)]
        [Header("Result")]
        public GameObject congratsMessage;
        public GameObject failMessage;

        #endregion

        public SetText setText;

        private void Awake()
        {
            for (int i = 0; i < 2; i++)
            {
                silicon[i].text = "????";
                iron[i].text = "????";
                aluminium[i].text = "????";
                calcium[i].text = "????";
                igneous[i].text = "????";
                sedimentary[i].text = "????";
                metamorphic[i].text = "????";
            }         
        }

        void Start()
        {
            // Get Player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            // Speech
            scientistSpeech.text = Speech01;        

            // All GameObjects
            labUI.SetActive(false);
            hintGO.SetActive(false);
            congratsMessage.SetActive(false);
            failMessage.SetActive(false);
        }

        #region Open and Close

        public void OpenLabUI()
        {
            congratsMessage.SetActive(false);
            failMessage.SetActive(false);
            playerResponse.text = "";

            // If the player has discovered all the rocks
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
            }
            else 
            {
                // If the player has mined
                if (player.totalRocks > 0)
                {
                    #region Choose Questions

                    // Determine what rocks you have and what question to get
                    if (player.silicon > 0 && silicon[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("silicon");
                    }
                    else if (player.iron > 0 && iron[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("iron");
                    }
                    else if (player.aluminium > 0 && aluminium[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("aluminium");
                    }
                    else if (player.calcium > 0 && calcium[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("calcium");
                    }
                    else if (player.igneous > 0 && igneous[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("igneous");
                    }
                    else if (player.sedimentary > 0 && sedimentary[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("sedimentary");
                    }
                    else if (player.metamorphic > 0 && metamorphic[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("metamorphic");
                    }
                    else
                    {
                        scientistSpeech.text = Speech01;
                        LOLSDK.Instance.SpeakText("Lab_TextBubble_01");
                    }

                        #endregion
                }
                else
                {
                    scientistSpeech.text = Speech02;
                    LOLSDK.Instance.SpeakText("Lab_TextBubble_02");
                }
            }
        }

        public void CloseLabUI()
        {
            labUI.SetActive(false);
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
                if (playerResponse.text.Contains(otherRocksAnswers[questionNr].ToString()))
                {
                    // Mark rock as discovered
                    if (currentRock == "silicon")
                    {
                        hasDiscoveredSilicon = true;

                        for (int i = 0; i < silicon.Length; i++)
                            silicon[i].text = setText.currentLanguage.Silicon;
                    }
                    if (currentRock == "iron")
                    {
                        hasDiscoveredIron = true;

                        for (int i = 0; i < iron.Length; i++)
                            iron[i].text = setText.currentLanguage.Iron;
                    }
                    if (currentRock == "aluminium")
                    {
                        hasDiscoveredAluminium = true;

                        for (int i = 0; i < aluminium.Length; i++)
                            aluminium[i].text = setText.currentLanguage.Aluminium;
                    }
                    if (currentRock == "calcium")
                    {
                        hasDiscoveredCalcium = true;

                        for (int i = 0; i < calcium.Length; i++)
                            calcium[i].text = setText.currentLanguage.Calcium;
                    }

                    // Remove solved question from the pool
                    List<string> q = new List<string>(otherRocksQuestions);
                    q.RemoveAt(q.IndexOf(otherRocksQuestions[questionNr]));
                    otherRocksQuestions = q.ToArray();

                    // Remove solved question answer from the pool
                    List<string> a = new List<string>(otherRocksAnswers);
                    a.RemoveAt(a.IndexOf(otherRocksAnswers[questionNr]));
                    otherRocksAnswers = a.ToArray();

                    // Remove solved question hint from the pool
                    List<string> h = new List<string>(otherRocksHint);
                    h.RemoveAt(h.IndexOf(otherRocksHint[questionNr]));
                    otherRocksHint = h.ToArray();

                    // Display Message
                    congratsMessage.SetActive(true);
                    failMessage.SetActive(false);
                }
                else
                {
                    playerResponse.text = "";

                    // Enable the hint
                    ShowHint();

                    // Display Message
                    congratsMessage.SetActive(false);
                    failMessage.SetActive(true);
                }
            }

            #endregion

            #region Igneous

            if (currentRock == "igneous")
            {
                if (playerResponse.text.Contains(igneousAnswers[questionNr]))
                {
                    // Mark rock as discovered
                    hasDiscoveredIgneous = true;

                    for (int i = 0; i < igneous.Length; i++)
                        igneous[i].text = setText.currentLanguage.Igneous;

                    // Remove solved question from the pool
                    List<string> q = new List<string>(igneousQuestions);
                    q.RemoveAt(q.IndexOf(igneousQuestions[questionNr]));
                    igneousQuestions = q.ToArray();

                    // Remove solved question answer from the pool
                    List<string> a = new List<string>(igneousAnswers);
                    a.RemoveAt(a.IndexOf(igneousAnswers[questionNr]));
                    igneousAnswers = a.ToArray();

                    // Remove solved question hint from the pool
                    List<string> h = new List<string>(igneousHint);
                    h.RemoveAt(h.IndexOf(igneousHint[questionNr]));
                    igneousHint = h.ToArray();

                    // Display Message
                    congratsMessage.SetActive(true);
                    failMessage.SetActive(false);
                }
                else
                {
                    playerResponse.text = "";

                    // Enable the hint
                    ShowHint();

                    // Display Message
                    congratsMessage.SetActive(false);
                    failMessage.SetActive(true);
                }
            }

            #endregion

            #region Sedimentary

            if (currentRock == "sedimentary")
            {
                if (playerResponse.text.Contains(sedimentaryAnswers[questionNr]))
                {
                    // Mark rock as discovered
                    hasDiscoveredSedimentary = true;

                    for (int i = 0; i < sedimentary.Length; i++)
                        sedimentary[i].text = setText.currentLanguage.Sedimentary;

                    // Remove solved question from the pool
                    List<string> q = new List<string>(sedimentaryQuestions);
                    q.RemoveAt(q.IndexOf(sedimentaryQuestions[questionNr]));
                    sedimentaryQuestions = q.ToArray();

                    // Remove solved question answer from the pool
                    List<string> a = new List<string>(sedimentaryAnswers);
                    a.RemoveAt(a.IndexOf(sedimentaryAnswers[questionNr]));
                    sedimentaryAnswers = a.ToArray();

                    // Remove solved question hint from the pool
                    List<string> h = new List<string>(sedimentaryHint);
                    h.RemoveAt(h.IndexOf(sedimentaryHint[questionNr]));
                    sedimentaryHint = h.ToArray();

                    // Display Message
                    congratsMessage.SetActive(true);
                    failMessage.SetActive(false);
                }
                else
                {
                    playerResponse.text = "";

                    // Enable the hint
                    ShowHint();

                    // Display Message
                    congratsMessage.SetActive(false);
                    failMessage.SetActive(true);
                }
            }

            #endregion

            #region Metamorphic

            if (currentRock == "metamorphic")
            {
                if (playerResponse.text.Contains(metamorphicAnswers[questionNr]))
                {
                    // Mark rock as discovered
                    hasDiscoveredMetamorphic = true;

                    for (int i = 0; i < metamorphic.Length; i++)
                        metamorphic[i].text = setText.currentLanguage.Metamorphic;

                    // Remove solved question from the pool
                    List<string> q = new List<string>(metamorphicQuestions);
                    q.RemoveAt(q.IndexOf(metamorphicQuestions[questionNr]));
                    metamorphicQuestions = q.ToArray();

                    // Remove solved question answer from the pool
                    List<string> a = new List<string>(metamorphicAnswers);
                    a.RemoveAt(a.IndexOf(metamorphicAnswers[questionNr]));
                    metamorphicAnswers = a.ToArray();

                    // Remove solved question hint from the pool
                    List<string> h = new List<string>(metamorphicHint);
                    h.RemoveAt(h.IndexOf(metamorphicHint[questionNr]));
                    metamorphicHint = h.ToArray();

                    // Display Message
                    congratsMessage.SetActive(true);
                    failMessage.SetActive(false);
                }
                else
                {
                    playerResponse.text = "";

                    // Enable the hint
                    ShowHint();

                    // Display Message
                    congratsMessage.SetActive(false);
                    failMessage.SetActive(true);
                }
            }

            #endregion
        }

        #region Questions and Hints

        void ChooseQuestion(string rock)
        {          
            if(rock == "silicon")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "silicon";
                questionNr = randomNr;
            }
            if (rock == "iron")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "iron";
                questionNr = randomNr;
            }
            if (rock == "aluminium")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "aluminium";
                questionNr = randomNr;
            }
            if (rock == "calcium")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "calcium";
                questionNr = randomNr;
            }
            if (rock == "igneous")
            {
                int randomNr = Random.Range(0, igneousQuestions.Length);

                question.text = igneousQuestions[randomNr];
                Hint(igneousHint[randomNr]);
                currentRock = "igneous";
                questionNr = randomNr;

            }
            if (rock == "sedimentary")
            {
                int randomNr = Random.Range(0, sedimentaryQuestions.Length);

                question.text = sedimentaryQuestions[randomNr];
                Hint(sedimentaryHint[randomNr]);
                currentRock = "sedimentary";
                questionNr = randomNr;

            }
            if (rock == "metamorphic")
            {
                int randomNr = Random.Range(0, metamorphicQuestions.Length);

                question.text = metamorphicQuestions[randomNr];
                Hint(metamorphicHint[randomNr]);
                currentRock = "metamorphic";
                questionNr = randomNr;
            }

            // Text-to-speech
            if(question.text == otherRocksQuestions[0])
                LOLSDK.Instance.SpeakText("Other_Question_01");
            if (question.text == otherRocksQuestions[1])
                LOLSDK.Instance.SpeakText("Other_Question_02");
            if (question.text == otherRocksQuestions[2])
                LOLSDK.Instance.SpeakText("Other_Question_03");
            if (question.text == otherRocksQuestions[3])
                LOLSDK.Instance.SpeakText("Other_Question_04");
            if (question.text == otherRocksQuestions[4])
                LOLSDK.Instance.SpeakText("Other_Question_05");
            if (question.text == igneousQuestions[0])
                LOLSDK.Instance.SpeakText("Igneous_Question");
            if (question.text == sedimentaryQuestions[0])
                LOLSDK.Instance.SpeakText("Sedimentary_Question");
            if (question.text == metamorphicQuestions[0])
                LOLSDK.Instance.SpeakText("Metamorphic_Question_01");
            if (question.text == metamorphicQuestions[1])
                LOLSDK.Instance.SpeakText("Metamorphic_Question_02");
        }

        void Hint(string hint_)
        {
            hint.text = hint_;
        }

        void ShowHint()
        {
            // Enable the hint
            hintGO.SetActive(true);

            // Text-to-speech
            if (hint.text == otherRocksHint[0])
                LOLSDK.Instance.SpeakText("Other_Hint_01");
            if (hint.text == otherRocksHint[1])
                LOLSDK.Instance.SpeakText("Other_Hint_02");
            if (hint.text == otherRocksHint[2])
                LOLSDK.Instance.SpeakText("Other_Hint_03");
            if (hint.text == otherRocksHint[3])
                LOLSDK.Instance.SpeakText("Other_Hint_04");
            if (hint.text == otherRocksHint[4])
                LOLSDK.Instance.SpeakText("Other_Hint_05");
            if (hint.text == igneousHint[0])
                LOLSDK.Instance.SpeakText("Igneous_Hint");
            if (hint.text == sedimentaryHint[0])
                LOLSDK.Instance.SpeakText("Sedimentary_Hint");
            if (hint.text == metamorphicHint[0])
                LOLSDK.Instance.SpeakText("Metamorphic_Hint_01");
            if (hint.text == metamorphicHint[1])
                LOLSDK.Instance.SpeakText("Metamorphic_Hint_02");
        }

        #endregion
    }
}
