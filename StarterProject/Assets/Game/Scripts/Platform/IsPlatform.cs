using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlatform : MonoBehaviour {
    
    public Vector2Int tilePosition = new Vector2Int();

    public float growSpeed = 0.1f;

    public float cutoff = 0.5f;
    
    // Grow x size of platform
    public void grow() {

        Vector3 scale = transform.localScale;

        scale.x += growSpeed;

        transform.localScale = scale;
    }

    // Shrink x size of platform, return false when too small to shrink further
    public bool shrink() {
        
        Vector3 scale = transform.localScale;

        if (scale.x > cutoff) {

            scale.x -= growSpeed;

            transform.localScale = scale;

            return true;
        }
        
        return false;
    }
}
