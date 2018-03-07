using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : Singleton<CatController> {

    BoxCollider2D triggerBox;
    public GameObject standOn;

    public GameObject fish;

    public float platformDisplacement;

    public List<GameObject> platforms = new List<GameObject>();
	// Use this for initialization
	void Start () {
        triggerBox = gameObject.GetComponentInChildren<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (standOn)
        {
            platformDisplacement = (gameObject.transform.position.x - standOn.transform.position.x) / (standOn.GetComponent<SpriteRenderer>().size.x / 2f);
        }
        else
        {
            platformDisplacement = 0;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Platform") || collision.gameObject.tag == ("Ground"))
        {
            gameObject.GetComponent<Animator>().SetBool("grounded", true);
            standOn = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Platform") || collision.gameObject.tag == ("Ground"))
        {
            gameObject.GetComponent<Animator>().SetBool("grounded", false);
            standOn = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            platforms.Add(collision.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (platforms.Contains (collision.gameObject ))
        {
            platforms.Remove(collision.gameObject);
        }
    }
    //public BoxCollider2D GetCollider2D()
    //{

    //}
}
