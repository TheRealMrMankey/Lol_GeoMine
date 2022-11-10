using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        // All Questions Text-to-speech
        [Space(10)]
        [Header("Questions Audio TTS")]

        public AudioClip[] igneousTTS; // Value to Save
        public AudioClip[] sedimentaryTTS; // Value to Save
        public AudioClip[] metamorphicTTS; // Value to Save
        public AudioClip[] otherRocksTTS; // Value to Save
        private AudioSource audioSource;

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

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            currentRock = gameManager.saveData.currentRock;
            hasDiscoveredIron = gameManager.saveData.hasDiscoveredIron;
            hasDiscoveredSilicon = gameManager.saveData.hasDiscoveredSilicon;
            hasDiscoveredAluminium = gameManager.saveData.hasDiscoveredAluminium;
            hasDiscoveredCalcium = gameManager.saveData.hasDiscoveredCalcium;
            hasDiscoveredIgneous = gameManager.saveData.hasDiscoveredIgneous;
            hasDiscoveredSedimentary = gameManager.saveData.hasDiscoveredSedimentary;
            hasDiscoveredMetamorphic = gameManager.saveData.hasDiscoveredMetamorphic;

            igneousQuestions = gameManager.saveData.igneousQuestions;
            sedimentaryQuestions = gameManager.saveData.sedimentaryQuestions;
            metamorphicQuestions = gameManager.saveData.metamorphicQuestions;
            otherRocksQuestions = gameManager.saveData.otherRocksQuestions;

            igneousAnswers = gameManager.saveData.igneousAnswers;
            sedimentaryAnswers = gameManager.saveData.sedimentaryAnswers;
            metamorphicAnswers = gameManager.saveData.metamorphicAnswers;
            otherRocksAnswers = gameManager.saveData.otherRocksAnswers;

            igneousHint = gameManager.saveData.igneousHint;
            sedimentaryHint = gameManager.saveData.sedimentaryHint;
            metamorphicHint = gameManager.saveData.metamorphicHint;
            otherRocksHint = gameManager.saveData.otherRocksHint;
        }

        void Start()
        {
            

            // Get Player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            // Speech
            scientistSpeech.text = "Great your're here, let's see what you have brought me.";
            audioSource = this.gameObject.GetComponent<AudioSource>();

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
                scientistSpeech.text = "Good job! You have discovered all the rocks in the mine.";
            }
            else 
            {
                // If the player has mined
                if (player.totalRocks > 0)
                {              
                    #region Choose Questions

                    // Determine what rocks you have and what question to get
                    if (player.silicon > 0 && silicon[0].text == "????" )
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("silicon");
                    }
                    else if (player.iron > 0 && iron[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("iron");
                    }
                    else if(player.aluminium > 0 && aluminium[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("aluminium");
                    }
                    else if(player.calcium > 0 && calcium[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("calcium");
                    }
                    else if(player.igneous > 0 && igneous[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("igneous");
                    }
                    else if(player.sedimentary > 0 && sedimentary[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("sedimentary");
                    }
                    else if(player.metamorphic > 0 && metamorphic[0].text == "????")
                    {
                        labUI.SetActive(true);
                        ChooseQuestion("metamorphic");
                    }
                    else
                        scientistSpeech.text = "You have no rocks for me to analyze, come back later when you do.";

                    #endregion
                }
                else
                    scientistSpeech.text = "You have no rocks for me to analyze, come back later when you do.";
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
                            silicon[i].text = "Silicon";
                    }
                    if (currentRock == "iron")
                    {
                        hasDiscoveredIron = true;

                        for (int i = 0; i < iron.Length; i++)
                            iron[i].text = "Iron";
                    }
                    if (currentRock == "aluminium")
                    {
                        hasDiscoveredAluminium = true;

                        for (int i = 0; i < aluminium.Length; i++)
                            aluminium[i].text = "Aluminium";
                    }
                    if (currentRock == "calcium")
                    {
                        hasDiscoveredCalcium = true;

                        for (int i = 0; i < calcium.Length; i++)
                            calcium[i].text = "Calcium";
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
                    hintGO.SetActive(true);

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
                        igneous[i].text = "Igneous";

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
                    hintGO.SetActive(true);

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
                        sedimentary[i].text = "Sedimentary";

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
                    hintGO.SetActive(true);

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
                        metamorphic[i].text = "Metamorphic";

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
                    hintGO.SetActive(true);

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

                // Play audio
                audioSource.PlayOneShot(otherRocksTTS[randomNr]);
            }
            if (rock == "iron")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "iron";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(otherRocksTTS[randomNr]);
            }
            if (rock == "aluminium")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "aluminium";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(otherRocksTTS[randomNr]);
            }
            if (rock == "calcium")
            {
                int randomNr = Random.Range(0, otherRocksQuestions.Length);

                question.text = otherRocksQuestions[randomNr];
                Hint(otherRocksHint[randomNr]);
                currentRock = "calcium";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(otherRocksTTS[randomNr]);
            }
            if (rock == "igneous")
            {
                int randomNr = Random.Range(0, igneousQuestions.Length);

                question.text = igneousQuestions[randomNr];
                Hint(igneousHint[randomNr]);
                currentRock = "igneous";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(igneousTTS[randomNr]);
            }
            if (rock == "sedimentary")
            {
                int randomNr = Random.Range(0, sedimentaryQuestions.Length);

                question.text = sedimentaryQuestions[randomNr];
                Hint(sedimentaryHint[randomNr]);
                currentRock = "sedimentary";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(sedimentaryTTS[randomNr]);
            }
            if (rock == "metamorphic")
            {
                int randomNr = Random.Range(0, metamorphicQuestions.Length);

                question.text = metamorphicQuestions[randomNr];
                Hint(metamorphicHint[randomNr]);
                currentRock = "metamorphic";
                questionNr = randomNr;

                // Play audio
                audioSource.PlayOneShot(metamorphicTTS[randomNr]);
            }
        }

        void Hint(string hint_)
        {
            hint.text = hint_;
        }

        #endregion
    }
}
