using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlatform : MonoBehaviour {

    private SpriteRenderer platRenderer = null; // Used to scale with tiling

    public Vector2Int tilePosition = new Vector2Int();

    public float growSpeed = 0.01f;

    public float cutoff = 0.99f;

    void Start() {

        platRenderer = GetComponent<SpriteRenderer>();
    }

    // Grow x size of platform
    public void grow() {

        if (platRenderer != null) {

            Vector2 scale = platRenderer.size;

            scale.x += growSpeed;

            platRenderer.size = scale;
        }
    }

    // Shrink x size of platform, return false when too small to shrink further
    public bool shrink() {

        if (platRenderer != null) {

            Vector3 scale = platRenderer.size;

            if (scale.x > cutoff) {

                scale.x -= growSpeed;

                platRenderer.size = scale;

                return true;
            }
        }

        return false;
    }
}
