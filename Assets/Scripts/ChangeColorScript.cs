using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class ChangeColorScript : MonoBehaviour {
    public bool green { get; private set; }
    public Sprite redSprite;
    public Sprite greenSprite;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    public void setSpriteRed()
    {
        spriteRenderer.sprite = redSprite;
        green = false;
    }

    public void setSpriteGreen()
    {
        spriteRenderer.sprite = greenSprite;
        green = true;
    }
}
