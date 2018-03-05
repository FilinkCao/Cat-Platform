using UnityEngine;
using System.Collections;

public class Flythrough : MonoBehaviour
{

		public Vector3 initialPosition;
		public Vector3 targetPosition;
		public Vector3 targetDirection;
		public float lerpStartTime;
		public float lerpElapsedTime;
		public float  lerpPercent;
		public float lerpDuration;
		public AnimationCurve lerpCurve;

		void Awake ()
		{
				Application.runInBackground = true;
		}

		void OnTileDown (Vector2 coords)
		{
				initialPosition = transform.position;
				targetPosition = new Vector3 (coords.x * 4f, 10f, coords.y * -4f);
				targetDirection = targetPosition - transform.position;
				lerpStartTime = Time.time;
		}
	
		void OnTileUp (Vector2 coords)
		{
		}

		void Update ()
		{
				FloorPadInput.GetEvents (gameObject);
		}

		void LateUpdate ()
		{
				lerpElapsedTime = Time.time - lerpStartTime;
				lerpPercent = lerpElapsedTime / lerpDuration;
				if (lerpElapsedTime < lerpDuration) {
						transform.position = Vector3.Lerp (initialPosition, targetPosition, lerpCurve.Evaluate (lerpPercent));
						transform.LookAt (new Vector3 (3, 0, -3));

				} else {
						transform.position = targetPosition;
				}
		}
}
