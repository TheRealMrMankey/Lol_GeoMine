using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

namespace Tiago_GeoMine
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private NavMeshAgent agent;
        private Tilemap tilemap;
        private NavMeshSurface surface2D;

        #endregion

        void Start()
        {
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

                            if (distance < 1.25f)
                            {
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
        }
    }
}
