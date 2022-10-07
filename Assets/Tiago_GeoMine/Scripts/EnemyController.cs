using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiago_GeoMine
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables

        private Vector2 direction;
        private int healthPoints;

        public float moveSpeed;

        #endregion

        void Start()
        {
            // Variables Initial Value
            direction = Vector2.right;
            healthPoints = 50;
        }

        void Update()
        {
            // Move enemy left/right
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            // Get all point of contact
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                // If the collision is on the right, change direction to left
                if (hitPos.normal.x < 0)
                    direction = Vector2.left;
                // If the collision is on the left, change direction to right
                if (hitPos.normal.x > 0)
                    direction = Vector2.right;
            }
        }
    }
}
