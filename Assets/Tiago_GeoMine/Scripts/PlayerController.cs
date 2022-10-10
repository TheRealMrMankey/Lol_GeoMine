using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Tiago_GeoMine
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        // UI Variables
        [Space(10)]
        [Header("UI")]

        public GameObject inventory;

        public GameObject shop;
        public GameObject lab;

        // Tilemap and Navigation Variables

        private NavMeshAgent agent;
        private Tilemap tilemap;
        private NavMeshSurface surface2D;

        // Player Variables
        [Space(10)]
        [Header("Player")]

        public int healthPoints;

        // Inventory Variables
        [Space(10)]
        [Header("Upgrades")]

        public int money;

        /// Upgradable      
        public int pickaxeLvl = 1;
        public int lanternLvl = 1;
        public int armourLvl = 1;

        [Space(10)]
        [Header("Rocks and Minerals")]

        /// Rocks and Minerals
        public int silicon;
        public int iron;
        public int aluminium;
        public int calcium;
        public int igneous;
        public int sedimentary;
        public int volcanic;
        public int metamorphic;
        public int gravel;

        #endregion

        void Start()
        {
            #region Components and GameObjects

            // NavMeshAgent (Player)
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            // Tilemap
            tilemap = GameObject.FindGameObjectWithTag("CanMine").GetComponent<Tilemap>();

            // NavMeshSurface (Walkable Area)
            surface2D = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>();
            surface2D.BuildNavMeshAsync();
            surface2D.UpdateNavMesh(surface2D.navMeshData);

            #endregion

            #region UI

            // UI
            inventory.SetActive(false);

            #endregion
        }

        void Update()
        {
            #region Movement and Rock Destruction (Mouse) //Needs Tweaking

            // Mouse Version
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // Get mouse position in a 2D environment
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                    if (hit.collider != null)
                    {
                        // Test
                        Debug.Log("Hit: " + hit.transform.name);

                        // Movement
                        /// Buildings
                        if (hit.transform.tag == "Shop")
                            agent.SetDestination(new Vector2(1.7f, 0.72f));
                        if (hit.transform.tag == "Mine")
                            agent.SetDestination(new Vector2(-6.98f, 0.72f));
                        if (hit.transform.tag == "Research")
                            agent.SetDestination(new Vector2(6.57f, 0.72f));

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

                        if(hit.transform.tag == "Background")
                        {
                            Vector3 tilePos = tilemap.WorldToCell(mousePos);
                            Vector3 offset = new Vector3(.5f, .35f, 0);
                            agent.SetDestination(offset + tilePos);
                        }

                        // Mining
                        if (hit.transform.tag == "CanMine")
                        {
                            // Check if it is in range
                            float distance = Vector3.Distance(transform.position, tilemap.WorldToCell(mousePos));

                            if (distance < 1.5f)
                            {
                                // Get Tile Name
                                TileBase tileBase = tilemap.GetTile(tilemap.WorldToCell(mousePos));
                                string tileName = tileBase.ToString();

                                //Test
                                Debug.Log("Name: " + tileName);
                                if (tileName.Contains("Rocks"))
                                {
                                    silicon++;      
                                }
                                
                                // Destroy Tile
                                tilemap.SetTile(tilemap.WorldToCell(mousePos), null);                            

                                // Update Walkable Area
                                surface2D.UpdateNavMesh(surface2D.navMeshData);
                            }
                        }
                    }
                }
            }

            #endregion

            #region Death

            // Death
            if (healthPoints <= 0)
            {
                //Teleport to starting position
                agent.Warp(new Vector2(-2.9f, 0.57f));

                //Regain all health
                healthPoints = 100;             
            }

            #endregion
        }

        #region Backpack

        // Open/Close Inventory
        public void OpenBackpack()
        {
            if (inventory.activeSelf == false) // Open
            {
                inventory.SetActive(true);

                // Update Backpack
                GetInventoryValues getInventory = FindObjectOfType<GetInventoryValues>();
                getInventory.UpdateBackpack();
            }
            else // Close
                inventory.SetActive(false);
        }

        #endregion

        #region Collisions

        // If the player collides with the enemy, the player takes damage
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Enemy")
                healthPoints -= 10;
        }

        #endregion
    }
}
