using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroppyBox : MonoBehaviour
{
		public GameObject cube;

		void Awake ()
		{
				Application.runInBackground = true;
		}

		void Update ()
		{
				FloorPadInput.GetEvents (gameObject);
		}

		void OnTileDown (Vector2 coords)
		{
				CreateNewBall (new Vector3 (coords.x, 10, -coords.y));
		}

		void OnTileUp (Vector2 coords)
		{
				//CreateNewBall (new Vector3 (coords.x, 10, coords.y));
		}

		void CreateNewBall (Vector3 coordinates)
		{
				Instantiate (cube, coordinates, cube.transform.rotation);
		}

}
