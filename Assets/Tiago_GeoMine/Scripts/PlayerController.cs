using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using LoLSDK;

namespace Tiago_GeoMine
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        // UI Variables
        [Space(10)]
        [Header("UI")]

        public GameObject inventory;
        public GameObject deathScreen;

        public GameObject shop;
        public GameObject lab;

        public GameObject[] buttons;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI[] moneyText;

        // Animation
        private Animator animator;

        // Audio
        [Space(10)]
        [Header("Audio")]

        public AudioClip damageSound;
        public AudioClip miningSound;
        public AudioClip killSound;
        private AudioSource audioSource;

        // Sprite
        private SpriteRenderer sprite;
        private float lastValue;

        // Player Variables
        [Space(10)]
        [Header("Player")]

        public int healthPoints; // Value to Save
        private int maxHp;

        // Tilemap and Navigation Variables     
        public NavMeshAgent agent;
        [HideInInspector] public Tilemap tilemap;
        [HideInInspector] public TileBase[] tiles;
        private NavMeshSurface surface2D;

        // Inventory Variables
        [Space(10)]
        [Header("Upgrades")]

        public int money; // Value to Save

        /// Upgradable      
        public int pickaxeLvl = 1; // Value to Save
        public int lanternLvl = 1; // Value to Save
        public Light2D helmetLight;
        public int armourLvl = 1; // Value to Save

        [Space(10)]
        [Header("Rocks and Minerals")]

        /// Rocks and Minerals
        public int totalRocks; // Value to Save
        public int silicon; // Value to Save
        public int iron; // Value to Save
        public int aluminium; // Value to Save
        public int calcium; // Value to Save
        public int igneous; // Value to Save
        public int sedimentary; // Value to Save
        public int metamorphic; // Value to Save

        #endregion

        private List<Vector3Int> tileWorldLocations;
        private List<TileBase> tileBases;

        public Vector3Int[] tilesLoc;
        public TileBase[] tilesType;
        public BoundsInt tileBounds;

        private SaveGame saveScript;
        private List<string> tilesNames;
        public string[] tilesNames_;

        void Start()
        {
            #region Components and GameObjects

            saveScript = GameObject.FindGameObjectWithTag("Save").GetComponent<SaveGame>();

            // NavMeshAgent (Player)
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            // Tilemap
            //tilemap = GameObject.FindGameObjectWithTag("CanMine").GetComponent<Tilemap>();

            // NavMeshSurface (Walkable Area)
            surface2D = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>();
            surface2D.BuildNavMeshAsync();
            surface2D.UpdateNavMesh(surface2D.navMeshData);

            // Animator
            animator = this.gameObject.GetComponent<Animator>();

            // Audio
            audioSource = this.gameObject.GetComponent<AudioSource>();

            // Sprite
            sprite = this.gameObject.GetComponent<SpriteRenderer>();      

            #endregion

            #region UI

            // UI
            inventory.SetActive(false);
            deathScreen.SetActive(false);

            shop.SetActive(false);
            lab.SetActive(false);

            #endregion

            #region Upgrades

            // Helmet Light Radius
            helmetLight.pointLightOuterRadius = lanternLvl * 2;
            helmetLight.gameObject.SetActive(false);

            // Health
            UpdateMaxHealth();

            // Money
            for (int i = 0; i < moneyText.Length; i++)
            {
                moneyText[i].text = money.ToString();
            }

            #endregion                

            // Grab tiles
            GetTiles();
            StartCoroutine(SaveTile());
        }

        void Update()
        {
            #region Sprite and Animation    

            // Sprite Flip       
            if (agent.velocity.x < 0 && agent.isStopped == false)
            {
                sprite.flipX = false;
                lastValue = agent.velocity.x;
            }
            else if (agent.velocity.x > 0 && agent.isStopped == false)
            {
                sprite.flipX = true;
                lastValue = agent.velocity.x;
            }

            if (agent.isStopped && lastValue < 0)
                sprite.flipX = true;
            else if (agent.isStopped && lastValue > 0)
                sprite.flipX = false;

            // Animation (Walking)
            Debug.Log("Velocity " + agent.velocity.x + " " + agent.velocity.y);
            if (agent.velocity.x != 0)
                animator.SetBool("isWalking", true);
            else
                animator.SetBool("isWalking", false);

            #endregion

            #region Movement and Rock Destruction (Mouse)

            if (Input.GetMouseButtonDown(0))
            {
                // Get mouse position in a 2D environment
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null)
                {
                    // Movement

                    /// Buildings
                    if (hit.transform.tag == "Shop")
                    {
                        GoToShop();

                        if (Vector2.Distance(agent.transform.position, hit.transform.position) <= 1.5f)
                        {
                            shop.SetActive(true);
                            agent.isStopped = true;

                            LOLSDK.Instance.SpeakText("Shop_TextBubble");
                        }
                    }
                    if (hit.transform.tag == "Mine")
                        GoToEntrance();
                    if (hit.transform.tag == "Research")
                    {
                        GoToResearch();

                        if (Vector2.Distance(agent.transform.position, hit.transform.position) <= 3f)
                        {
                            lab.SetActive(true);
                            agent.isStopped = true;

                            LOLSDK.Instance.SpeakText("Lab_TextBubble_01");
                        }
                    }

                    /// Underground
                    if (hit.transform.tag == "CanMine")
                    {
                        Vector3 tilePos = tilemap.WorldToCell(mousePos);
                        Vector3 offset = new Vector3(.5f, .35f, 0);

                        // Check if it is possible for the player to reach
                        NavMeshPath navPath = new NavMeshPath();

                        if (agent.CalculatePath(mousePos2D, navPath) && navPath.status == NavMeshPathStatus.PathComplete)
                            agent.SetDestination(offset + tilePos);
                    }

                    if (hit.transform.tag == "Background")
                    {
                        Vector3 tilePos = tilemap.WorldToCell(mousePos);
                        Vector3 offset = new Vector3(.5f, .35f, 0);
                        agent.SetDestination(offset + tilePos);
                    }

                    // Attacking Enemy
                    if (hit.transform.tag == "Enemy")
                    {
                        // Check if it is in range
                        float distance = Vector3.Distance(transform.position, hit.transform.position);

                        // Kill enemy
                        if (distance < 1.75f)
                        {
                            // Animation (Mining)
                            animator.SetTrigger("isMining");

                            // Enemy kill sound
                            audioSource.PlayOneShot(killSound);

                            // Destroy enemy Game Object
                            Destroy(hit.transform.gameObject);
                        }
                    }

                    // Mining
                    if (hit.transform.tag == "CanMine")
                    {
                        // Check if it is in range
                        float distance = Vector3.Distance(transform.position, tilemap.WorldToCell(mousePos));

                        if (distance < 1.8f)
                        {
                            // Get Tile Name
                            TileBase tileBase = tilemap.GetTile(tilemap.WorldToCell(mousePos));
                            string tileName = tileBase.ToString();

                            // If pickaxe is at least level 1, mine
                            if (tileName.Contains("Calcium") | tileName.Contains("Silicon") | tileName.Contains("Aluminium") | tileName.Contains("Iron") && pickaxeLvl >= 1)
                            {
                                // Find out which rock was destroyed
                                Destroy(tileName);

                                // Destroy Tile
                                tilemap.SetTile(tilemap.WorldToCell(mousePos), null);
                            }
                            // If pickaxe if level 2, mine
                            else if (tileName.Contains("Sedimentary") | tileName.Contains("Igneous") | tileName.Contains("Metamorphic") && pickaxeLvl == 2)
                            {
                                // Find out which rock was destroyed
                                Destroy(tileName);

                                // Destroy Tile
                                tilemap.SetTile(tilemap.WorldToCell(mousePos), null);
                            }

                            // Update Walkable Area
                            surface2D.UpdateNavMesh(surface2D.navMeshData);
                        }
                    }
                }
            }

            #endregion

            #region Movement and Rock Destruction (Touch)

            if (Input.touchCount > 1)
            {
                // Get touch position in a 2D environment
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touch2D = new Vector2(touchPos.x, touchPos.y);

                RaycastHit2D hit = Physics2D.Raycast(touch2D, Vector2.zero);

                if (hit.collider != null)
                {
                    // Movement

                    /// Buildings
                    if (hit.transform.tag == "Shop")
                    {
                        GoToShop();

                        if (Vector2.Distance(agent.transform.position, hit.transform.position) <= 1.5f)
                        {
                            shop.SetActive(true);
                            agent.isStopped = true;

                            LOLSDK.Instance.SpeakText("Shop_TextBubble");
                        }
                    }
                    if (hit.transform.tag == "Mine")
                        GoToEntrance();
                    if (hit.transform.tag == "Research")
                    {
                        GoToResearch();

                        if (Vector2.Distance(agent.transform.position, hit.transform.position) <= 3f)
                        {
                            lab.SetActive(true);
                            agent.isStopped = true;

                            LOLSDK.Instance.SpeakText("Lab_TextBubble_01");
                        }
                    }

                    /// Underground
                    if (hit.transform.tag == "CanMine")
                    {
                        Vector3 tilePos = tilemap.WorldToCell(touchPos);
                        Vector3 offset = new Vector3(.5f, .35f, 0);

                        // Check if it is possible for the player to reach
                        NavMeshPath navPath = new NavMeshPath();

                        if (agent.CalculatePath(touch2D, navPath) && navPath.status == NavMeshPathStatus.PathComplete)
                            agent.SetDestination(offset + tilePos);
                    }

                    if (hit.transform.tag == "Background")
                    {
                        Vector3 tilePos = tilemap.WorldToCell(touchPos);
                        Vector3 offset = new Vector3(.5f, .35f, 0);
                        agent.SetDestination(offset + tilePos);
                    }

                    // Attacking Enemy
                    if (hit.transform.tag == "Enemy")
                    {
                        // Check if it is in range
                        float distance = Vector3.Distance(transform.position, hit.transform.position);

                        // Kill enemy
                        if (distance < 1.75f)
                        {
                            // Animation (Mining)
                            animator.SetTrigger("isMining");

                            // Enemy kill sound
                            audioSource.PlayOneShot(killSound);

                            // Destroy enemy Game Object
                            Destroy(hit.transform.gameObject);
                        }
                    }

                    // Mining
                    if (hit.transform.tag == "CanMine")
                    {
                        // Check if it is in range
                        float distance = Vector3.Distance(transform.position, tilemap.WorldToCell(touchPos));

                        if (distance < 1.8f)
                        {
                            // Get Tile Name
                            TileBase tileBase = tilemap.GetTile(tilemap.WorldToCell(touchPos));
                            string tileName = tileBase.ToString();

                            // If pickaxe is at least level 1, mine
                            if (tileName.Contains("Calcium") | tileName.Contains("Silicon") | tileName.Contains("Aluminium") | tileName.Contains("Iron") && pickaxeLvl >= 1)
                            {
                                // Find out which rock was destroyed
                                Destroy(tileName);

                                // Destroy Tile
                                tilemap.SetTile(tilemap.WorldToCell(touchPos), null);
                            }
                            // If pickaxe if level 2, mine
                            else if (tileName.Contains("Sedimentary") | tileName.Contains("Igneous") | tileName.Contains("Metamorphic") && pickaxeLvl == 2)
                            {
                                // Find out which rock was destroyed
                                Destroy(tileName);

                                // Destroy Tile
                                tilemap.SetTile(tilemap.WorldToCell(touchPos), null);
                            }

                            // Update Walkable Area
                            surface2D.UpdateNavMesh(surface2D.navMeshData);
                        }
                    }
                }
            }

            #endregion

            #region Death

            // Death
            if (healthPoints <= 0)
            {
                //Regain all health
                healthPoints = maxHp;

                // Respawn
                /// Teleport to starting position
                agent.Warp(new Vector2(-2.9f, 0.57f));

                agent.isStopped = true;
                agent.destination = agent.transform.position;
                deathScreen.SetActive(true);
            }

            #endregion
        }

        #region Get Tiles

        private IEnumerator SaveTile()
        {
            while(true)
            {
                yield return new WaitForSeconds(30);
                GetTiles();

                GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                Lab labScript = lab.GetComponent<Lab>();

                gameManager.Save(

                labScript.currentRock,
                labScript.hasDiscoveredIron,
                labScript.hasDiscoveredSilicon,
                labScript.hasDiscoveredAluminium,
                labScript.hasDiscoveredCalcium,
                labScript.hasDiscoveredIgneous,
                labScript.hasDiscoveredSedimentary,
                labScript.hasDiscoveredMetamorphic,
                labScript.igneousQuestions,
                labScript.sedimentaryQuestions,
                labScript.metamorphicQuestions,
                labScript.otherRocksQuestions,
                labScript.igneousAnswers,
                labScript.sedimentaryAnswers,
                labScript.metamorphicAnswers,
                labScript.otherRocksAnswers,
                labScript.igneousHint,
                labScript.sedimentaryHint,
                labScript.metamorphicHint,
                labScript.otherRocksHint,

                money,
                pickaxeLvl,
                lanternLvl,
                armourLvl,
                totalRocks,
                silicon,
                iron,
                aluminium,
                calcium,
                igneous,
                sedimentary,
                metamorphic,
                tilesLoc,
                tilesNames_

                );

                    /*
                saveScript.money = money;
                saveScript.pickaxeLvl = pickaxeLvl;
                saveScript.lanternLvl = lanternLvl;
                saveScript.armourLvl = armourLvl;
                saveScript.totalRocks = totalRocks;
                saveScript.silicon = silicon;
                saveScript.iron = iron;
                saveScript.aluminium = aluminium;
                saveScript.calcium = calcium;
                saveScript.igneous = igneous;
                saveScript.sedimentary = sedimentary;
                saveScript.metamorphic = metamorphic;

                saveScript.tilesLoc = null;
                saveScript.tilesType = null;

                saveScript.tilesLoc = tilesLoc;
                saveScript.tilesType = tilesType;
                    */
            }
        }


        public void GetTiles()
        {
            tilesNames = new List<string>();

            tileWorldLocations = new List<Vector3Int>();
            tileBases = new List<TileBase>();

            foreach (var pos in tileBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (tilemap.HasTile(localPlace))
                {
                    tileWorldLocations.Add(localPlace);
                    tileBases.Add(tilemap.GetTile(localPlace));
                    tilesNames.Add(tilemap.GetTile(localPlace).name.ToString());
                }
            }

            //tilesNames = null;
            tilesLoc = null;
            tilesType = null;

            tilesLoc = tileWorldLocations.ToArray();
            tilesType = tileBases.ToArray();
            tilesNames_ = tilesNames.ToArray();
        }

        #endregion

        #region Death Screen

        public void CloseDeathScreen()
        {
            deathScreen.SetActive(false);
            agent.isStopped = false;
            agent.destination = agent.transform.position;
        }

        #endregion

        #region Type of Rock

        private void Destroy(string rockName)
        {
            // Animation (Mining)
            animator.SetTrigger("isMining");

            // Mining Sound
            audioSource.PlayOneShot(miningSound);

            totalRocks++;

            if (rockName.Contains("Silicon"))
                silicon++;
            if (rockName.Contains("Iron"))
                iron++;
            if (rockName.Contains("Aluminium"))
                aluminium++;
            if (rockName.Contains("Calcium"))
                calcium++;
            if (rockName.Contains("Igneous"))
                igneous++;
            if (rockName.Contains("Sedimentary"))
                sedimentary++;
            if (rockName.Contains("Metamorphic"))
                metamorphic++;
        }

        #endregion

        #region Quick Travel

        public void GoToEntrance()
        {
            agent.SetDestination(new Vector2(-7.448f, 0.34f));
        }

        public void GoToShop()
        {
            agent.SetDestination(new Vector2(-.159f, 0.534f));
        }

        public void GoToResearch()
        {
            agent.SetDestination(new Vector2(3.73f, 0.534f));
        }

        #endregion

        #region Update Healthpoints

        public void UpdateMaxHealth()
        {
            if (armourLvl == 1)
                healthPoints = 100;
            else if (armourLvl == 2)
                healthPoints = 150;
            else if (armourLvl == 3)
                healthPoints = 200;

            maxHp = healthPoints;
            healthText.text = healthPoints.ToString();
        }

        #endregion

        #region Backpack

        // Open/Close Inventory
        public void OpenBackpack()
        {
            if (inventory.activeSelf == false) // Open
            {
                agent.isStopped = true;
                agent.destination = agent.transform.position;

                inventory.SetActive(true);

                // Update Backpack
                GetInventoryValues getInventory = FindObjectOfType<GetInventoryValues>();
                getInventory.UpdateBackpack();

                // Disable unwanted buttons
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
            }
            else // Close
            {
                agent.isStopped = false;
                agent.destination = agent.transform.position;

                // Enable buttons
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(true);
                }

                inventory.SetActive(false);
            }
        }

        #endregion

        #region Collisions

        // If the player collides with the enemy, the player takes damage
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                // Taking Damage Sound
                audioSource.PlayOneShot(damageSound);

                healthPoints -= 10;
                healthText.text = healthPoints.ToString();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Trigger")
            {
                Debug.Log("trigger");

                if (helmetLight.gameObject.activeSelf == false)
                    helmetLight.gameObject.SetActive(true);
                else
                    helmetLight.gameObject.SetActive(false);
            }
        }

        #endregion      
    }
}