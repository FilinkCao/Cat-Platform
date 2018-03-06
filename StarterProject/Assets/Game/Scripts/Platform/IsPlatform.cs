using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlatform : MonoBehaviour {

    private SpriteRenderer platRenderer = null; // Used to scale with tiling

    private int collidingPlatforms = 0; // Ignores growth if a platform is in the way

    public GameObject spawnAnimationObject = null;

    public Vector2Int tilePosition = new Vector2Int();

    public float growSpeed = 0.01f;

    public float minCutoff = 0.99f;
    public float maxCutoff = 5.0f;

    void Start() {

        platRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.GetComponent<IsPlatform>()) {

            collidingPlatforms++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.GetComponent<IsPlatform>()) {

            collidingPlatforms--;
        }
    }

    // Grow x size of platform
    public void grow() {

        if (platRenderer != null && collidingPlatforms <= 0) {

            Vector2 scale = platRenderer.size;

            if (scale.x <= maxCutoff) {

                scale.x += growSpeed;

                platRenderer.size = scale;
            }
        }
    }

    // Shrink x size of platform, return false when too small to shrink further
    public bool shrink() {

        if (platRenderer != null) {

            Vector3 scale = platRenderer.size;

            if (scale.x > minCutoff) {

                scale.x -= growSpeed;

                platRenderer.size = scale;

                return true;
            }
        }

        return false;
    }
}
