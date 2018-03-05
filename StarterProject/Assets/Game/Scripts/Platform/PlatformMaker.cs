﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMaker : MonoBehaviour {
    

    private Dictionary<Vector2Int, GameObject> platforms = new Dictionary<Vector2Int, GameObject>();

    public List<GameObject> platformTypes = new List<GameObject>();

	// Update is called once per frame
	void Update () {

        growPlatformUpdate();
        createPlatformUpdate();
	}

    // Check through tile input and grow or shrink platforms appropriately
    private void growPlatformUpdate() {

        bool[,] tiles = InputGridManager.This.getActiveTiles();
        IsPlatform platform = null;

        foreach (KeyValuePair<Vector2Int, GameObject> p in platforms) {

            platform = p.Value.GetComponent<IsPlatform>();

            if (platform != null) {

                // Grow the platform if still pushed, shrink otherwise
                if (tiles[p.Key.x, p.Key.y]) {

                    platform.grow();
                }
                // If shrink fails, platform is too small thus delete
                else if (!platform.shrink()) {

                    platforms.Remove(p.Key);

                    Destroy(p.Value);
                }
            }
            else {

                platforms.Remove(p.Key);

                Destroy(p.Value);
            }
        }
    }

    // Check through pressed tile input and create missing platforms
    private void createPlatformUpdate() {

        if (platformTypes.Count > 0) {

            bool[,] tiles = InputGridManager.This.getActiveTiles();

            for (int i = 0; i < InputGridManager.gridSize; i++) {
                for (int j = 0; j < InputGridManager.gridSize; j++) {

                    // If platform does not exist at this point create it
                    if (tiles[i, j] && !platforms.ContainsKey(new Vector2Int(i, j))) {                 /// ACCOUNT FOR MULTIPLE TILES IN ON AREA PRESSED

                        GameObject newPlat = Instantiate(platformTypes[(int)(Random.value * platformTypes.Count)]);

                        if (newPlat != null) {

                            IsPlatform platScript = newPlat.GetComponent<IsPlatform>();

                            if (platScript == null) {

                                platScript = newPlat.AddComponent<IsPlatform>();
                            }

                            platScript.tilePosition = new Vector2Int(i, j);

                            platforms.Add(platScript.tilePosition, newPlat);
                        }
                    }
                }
            }
        }
    }
}
