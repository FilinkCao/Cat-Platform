using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloorPadInputManager))]
public class InputGridManager : MonoBehaviour {

    private bool[,] activeTiles = null;

    public static InputGridManager This = null;

    public static readonly int gridSize = 10;

    void Start() {
        
        // Make singleton instance accessable anywhere
        if (This == null) {

            This = this;

            activeTiles = new bool[gridSize, gridSize];
        }
    }

    // Update is called once per frame
    void Update () {

        tilesPressedUpdate();
	}

    // Clear grid and mark the new currently pressed grids
    private void tilesPressedUpdate() {
        
        activeTiles = new bool[gridSize, gridSize];
        List<Vector2> tilesPressed = FloorPadInput.GetPressedCoordinates();
        
        foreach (Vector2 tile in tilesPressed) {

            activeTiles[(int)tile.x, (int)tile.y] = true;
        }
    }

    public bool[,] getActiveTiles() {

        return activeTiles;
    }
}
