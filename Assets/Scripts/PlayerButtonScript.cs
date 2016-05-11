using UnityEngine;
using System.Collections;

public class PlayerButtonScript : MonoBehaviour {

    private ChangeColorScript colorScript;
    private float greenTime;
    private float totalGreenTime=.1f;

	// Use this for initialization
	void Start () {
        colorScript = GetComponent<ChangeColorScript>();
        greenTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!colorScript.green)
        {
            if (Input.GetButtonDown("Jump"))
            {
                colorScript.setSpriteGreen();
            }
        }
        else
        {
            greenTime += Time.deltaTime;
            if (greenTime > totalGreenTime)
            {
                colorScript.setSpriteRed();
                greenTime = 0;
            }
        }
	}
}
