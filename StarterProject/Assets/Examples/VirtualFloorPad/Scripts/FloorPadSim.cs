using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorPadSim : MonoBehaviour
{
		public TileSim[] tiles;
		public int gridSize = 10;
		private float tileSize = 1;
		public GameObject tilePrefab;
		public GameObject coordPrefab;
		private Transform tileHolder;
		public List<Vector2> coords;
		public FloorPadInput.TileColor tileColor;

		void Update ()
		{
				FloorPadInput.GetEvents (gameObject);

				if (FloorPadInput.GetTile (0, 0)) {
						Debug.Log ("The top left tile was pressed.");
				}

				if (FloorPadInput.GetTile (0, 9)) {
						Debug.Log ("The bottom left tile was pressed.");
				}
				if (FloorPadInput.GetTile (9, 0)) {
						Debug.Log ("The top right tile was pressed.");
				}

				if (FloorPadInput.GetTile (9, 9)) {
						Debug.Log ("The bottom right tile was pressed.");
				}
		}
	
		void OnTileDown (Vector2 coords)
		{
				tileColor = FloorPadInput.GetTileColor (coords);
		}
	
		void OnTileUp (Vector2 coords)
		{
				
		}

		void Awake ()
		{

				Application.runInBackground = true;

				tileHolder = GameObject.Find ("FloorPad").transform;

				tiles = new TileSim[gridSize * gridSize];	
				for (int i=0; i<tiles.Length; i++) {

						int row = i / gridSize;
						int column = i % gridSize;
						int tileIndex = i + 1;

						int x = (gridSize - 1) - (i / (gridSize));
						int y = i % (gridSize);

						GameObject tileGameObject = (GameObject)Instantiate (tilePrefab);
						tileGameObject.transform.position = new Vector3 (gridSize / 2f - row - tileSize / 2f, gridSize / 2f - column - tileSize / 2f, 0);
						tileGameObject.transform.parent = tileHolder;
						TileSim tile = tileGameObject.GetComponent<TileSim> ();	
						tile.Initialize (tileIndex, x, y, this, GetTileColour (tileIndex));
						tiles [i] = tile;
				}



				for (int i=0; i<10; i++) {
						GameObject numberGameObject = (GameObject)Instantiate (coordPrefab);
						numberGameObject.transform.position = new Vector3 (-4.5f + i, 5.5f, 0);
						numberGameObject.transform.GetChild (0).GetComponent<TextMesh> ().text = i.ToString ();
						numberGameObject.GetComponent<Renderer>().material.color = new Color (0, 0, 0, 0);


						numberGameObject = (GameObject)Instantiate (coordPrefab);
						numberGameObject.transform.position = new Vector3 (-5.5f, 4.5f - i, 0);
						numberGameObject.transform.GetChild (0).GetComponent<TextMesh> ().text = i.ToString ();
						numberGameObject.GetComponent<Renderer>().material.color = new Color (0, 0, 0, 0);
			
				}
				//tileGameObject.transform.parent = tileHolder;
		
		}

		Color GetTileColour (int tileNum)
		{
				// These are the colours of the actual carpet tiles used in the ScreenPlay
				// installation. RGB values are normalized for use with Unity.
				Color active = new Color (127.0f / 255.0f, 127.0f / 255.0f, 127.0f / 255.0f, 1.0f);   // colour of tiles that are being stepped on
				Color briteGreen = new Color (141.0f / 255.0f, 140.0f / 255.0f, 82.0f / 255.0f, 1.0f);  // colour of the brite green carpet tiles      
				Color calypso = new Color (125.0f / 255.0f, 58.0f / 255.0f, 95.0f / 255.0f, 1.0f);      // colour of the calypso carpet tiles
				Color marina = new Color (89.0f / 255.0f, 111.0f / 255.0f, 146.0f / 255.0f, 1.0f);      // colour of the marina carpet tiles
				Color portabella = new Color (127.0f / 255.0f, 110.0f / 255.0f, 92.0f / 255.0f, 1.0f);  // colour of the portabella carpet tiles

				switch (tileNum) {
				case 1:
						return calypso;
				case 2:
						return marina;
				case 3:
						return briteGreen;
				case 4:
						return calypso;
				case 5:
						return briteGreen;
				case 6:
						return marina;
				case 7:
						return portabella;
				case 8:
						return briteGreen;
				case 9:
						return marina;
				case 10:
						return briteGreen;
				case 11:
						return marina;
				case 12:
						return portabella;
				case 13:
						return portabella;
				case 14:
						return marina;
				case 15:
						return calypso;
				case 16:
						return portabella;
				case 17:
						return calypso;
				case 18:
						return portabella;
				case 19:
						return briteGreen;
				case 20:
						return marina;
				case 21:
						return calypso;
				case 22:
						return briteGreen;
				case 23:
						return marina;
				case 24:
						return briteGreen;
				case 25:
						return portabella;
				case 26:
						return briteGreen;
				case 27:
						return marina;
				case 28:
						return calypso;
				case 29:
						return marina;
				case 30:
						return calypso;
				case 31:
						return portabella;
				case 32:
						return marina;
				case 33:
						return portabella;
				case 34:
						return calypso;
				case 35:
						return briteGreen;
				case 36:
						return calypso;
				case 37:
						return briteGreen;
				case 38:
						return marina;
				case 39:
						return portabella;
				case 40:
						return briteGreen;
				case 41:
						return briteGreen;
				case 42:
						return calypso;
				case 43:
						return briteGreen;
				case 44:
						return marina;
				case 45:
						return calypso;
				case 46:
						return marina;
				case 47:
						return calypso;
				case 48:
						return marina;
				case 49:
						return briteGreen;
				case 50:
						return calypso;
				case 51:
						return briteGreen;
				case 52:
						return marina;
				case 53:
						return calypso;
				case 54:
						return portabella;
				case 55:
						return marina;
				case 56:
						return portabella;
				case 57:
						return briteGreen;
				case 58:
						return calypso;
				case 59:
						return marina;
				case 60:
						return portabella;
				case 61:
						return calypso;
				case 62:
						return briteGreen;
				case 63:
						return portabella;
				case 64:
						return calypso;
				case 65:
						return calypso;
				case 66:
						return marina;
				case 67:
						return portabella;
				case 68:
						return marina;
				case 69:
						return portabella;
				case 70:
						return briteGreen;
				case 71:
						return portabella;
				case 72:
						return calypso;
				case 73:
						return marina;
				case 74:
						return briteGreen;
				case 75:
						return portabella;
				case 76:
						return briteGreen;
				case 77:
						return marina;
				case 78:
						return briteGreen;
				case 79:
						return calypso;
				case 80:
						return calypso;
				case 81:
						return briteGreen;
				case 82:
						return portabella;
				case 83:
						return marina;
				case 84:
						return calypso;
				case 85:
						return briteGreen;
				case 86:
						return calypso;
				case 87:
						return briteGreen;
				case 88:
						return calypso;
				case 89:
						return portabella;
				case 90:
						return marina;
				case 91:
						return marina;
				case 92:
						return calypso;
				case 93:
						return briteGreen;
				case 94:
						return portabella;
				case 95:
						return marina;
				case 96:
						return marina;
				case 97:
						return calypso;
				case 98:
						return portabella;
				case 99:
						return briteGreen;
				case 100:
						return calypso;
				default:
						return active;
				}
		}
}
