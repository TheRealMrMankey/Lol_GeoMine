using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables

        private Vector2 direction;
        private SpriteRenderer sprite;

        // Enemy Stats
        [Space(10)]
        [Header("Stats")]

        public int healthPoints = 50;
        public float moveSpeed;

        #endregion

        void Start()
        {
            // Get Enemy's sprite
            sprite = this.gameObject.GetComponent<SpriteRenderer>();

            // Variables Initial Value
            direction = Vector2.right;
            sprite.flipX = true;
        }

        void Update()
        {
            // Move enemy left/right
            transform.Translate(direction * moveSpeed * Time.deltaTime);       
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            // Get all points of contact
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                // If the collision is on the right, change direction to left
                if (hitPos.normal.x < 0)
                {
                    direction = Vector2.left;
                    sprite.flipX = false;
                }
                // If the collision is on the left, change direction to right
                if (hitPos.normal.x > 0)
                {
                    direction = Vector2.right;
                    sprite.flipX = true;
                }
            }
        }
    }
}
