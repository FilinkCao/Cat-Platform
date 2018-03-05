using UnityEngine;
using System.Collections;

public class TileSim : MonoBehaviour
{
	public int index;
	public int x;
	public int y;
	public FloorPadSim floorPad;
	public bool activated;
	public Color inactiveColor;
	public Color activeColor;
	public Transform tileNumber;
	public TextMesh tileNumberText;

	public void Initialize (int index, int x, int y, FloorPadSim floorPad, Color inactiveColor)
	{
		this.index = index;
		this.floorPad = floorPad;
		this.x = x;
		this.y = y;
		this.inactiveColor = inactiveColor;
		activeColor = GetActiveColor (inactiveColor);
				
		tileNumber = transform.Find ("TileNumber");
		tileNumberText = tileNumber.GetComponent<TextMesh> ();
		tileNumberText.text = (index).ToString ();
		name = (index).ToString ();
	}

	void Start ()
	{
		ResetColor ();
	}

	void Update ()
	{
		FloorPadInput.GetEvents (gameObject);
	}
	
	void OnTileDown (Vector2 coords)
	{
		if (coords.x == x && coords.y == y) {
			activated = true;
			ResetColor ();
		}
	}
	
	void OnTileUp (Vector2 coords)
	{
		if (coords.x == x && coords.y == y) {
			activated = false;
			ResetColor ();
		}

	}

	Color GetActiveColor (Color color)
	{
		HSBColor hsbColor = HSBColor.FromColor (color);
		hsbColor.s += .3f;
		hsbColor.b += .5f;
		return hsbColor.ToColor ();
	}

	void ResetColor ()
	{

		if (activated) {
			SetColor (activeColor);
		} else {
			SetColor (inactiveColor);
		}

	}

	void SetColor (Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}
}
