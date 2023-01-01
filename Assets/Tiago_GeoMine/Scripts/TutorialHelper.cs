using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using TMPro;

namespace Tiago_GeoMine
{
    public class TutorialHelper : MonoBehaviour
    {
        private GameManager gameManager;
        private PlayerController player;
        private Lab lab;
        private GameObject research;
        private GameObject shop;

        public TextMeshProUGUI continueText;
        public TextMeshProUGUI mainText;

        private bool hasMoved;
        private bool hasMined;
        private bool hasResearched;
        private bool hasShoped;

        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            lab = GameObject.FindGameObjectWithTag("Text").GetComponent<SetText>().labScript;
            research = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().lab;
            shop = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().shop;

            continueText.gameObject.SetActive(true);

#if UNITY_EDITOR
            mainText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble01"];
            continueText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble_ContinueButton"];
#elif UNITY_WEBGL
            mainText.text = gameManager.langDefs["Tutorial_Bubble01"];
            continueText.text = gameManager.langDefs["Tutorial_Bubble_ContinueButton"];
#endif
            LOLSDK.Instance.SpeakText("Tutorial_Bubble01");
        }

        void Update()
        {
            if (hasMoved == true && hasMined == false && player.totalRocks > 0)
            {
#if UNITY_EDITOR
                mainText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble03"];
#elif UNITY_WEBGL
                mainText.text = gameManager.langDefs["Tutorial_Bubble03"];
#endif
                LOLSDK.Instance.SpeakText("Tutorial_Bubble03");
                hasMined = true;
            }

            if(hasMined == true && (lab.hasDiscoveredAluminium == true || lab.hasDiscoveredCalcium == true || lab.hasDiscoveredIron == true || lab.hasDiscoveredSilicon == true))
            {
#if UNITY_EDITOR
                mainText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble04"];
#elif UNITY_WEBGL
                mainText.text = gameManager.langDefs["Tutorial_Bubble04"];
#endif
                if (research.activeSelf == false && hasResearched == false)
                {
                    LOLSDK.Instance.SpeakText("Tutorial_Bubble04");
                    hasResearched = true;
                }
            }

            if (hasResearched == true && player.money > 0)
            {
                continueText.gameObject.SetActive(true);

#if UNITY_EDITOR
                mainText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble05"];             
                continueText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_CloseBubble"];
#elif UNITY_WEBGL
                mainText.text = gameManager.langDefs["Tutorial_Bubble05"];             
                continueText.text = gameManager.langDefs["Tutorial_CloseBubble"];
#endif
                if (shop.activeSelf == false && hasShoped == false)
                {
                    LOLSDK.Instance.SpeakText("Tutorial_Bubble05");
                    hasShoped = true;
                }
            }
        }

        public void ButtonClick()
        {
            if (hasMoved == false)
            {
#if UNITY_EDITOR
                mainText.text = gameManager.langDefs[gameManager.langCode]["Tutorial_Bubble02"];
#elif UNITY_WEBGL
            mainText.text = gameManager.langDefs["Tutorial_Bubble02"];
#endif
                LOLSDK.Instance.SpeakText("Tutorial_Bubble02");
                continueText.gameObject.SetActive(false);

                hasMoved = true;
            }

            if (hasMoved == true && hasMined == true && hasResearched == true && player.money > 0)
                this.gameObject.SetActive(false);
        }
    }
}
