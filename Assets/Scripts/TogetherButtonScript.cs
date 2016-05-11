using UnityEngine;
using System.Collections;

public class TogetherButtonScript : MonoBehaviour {
    private AudioSource audioSource;
    private ChangeColorScript colorScript;
    private float greenTime;
    private float totalGreenTime = .1f;

    private float nextBeat;
    private float bpm = 120.0f;
    private float timePerBeat;
    private float clipLength;

    public float inputWindow=0.2f;
    public float inputDelay;

    // Use this for initialization
    void Start()
    {
        colorScript = GetComponent<ChangeColorScript>();
        greenTime = 0;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        nextBeat = 0;
        timePerBeat = 60 / (bpm);
        clipLength = audioSource.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextBeat == 0)
        {
            if (audioSource.timeSamples < (nextBeat + timePerBeat) * 44100)
            {
                checkBeat();
            }
        }
        else
        {
            checkBeat();
        }

        if (Input.GetButtonDown("Jump"))
        {
            checkClick();
        }

        if (colorScript.green)
        {
            greenTime += Time.deltaTime;
            if (greenTime > totalGreenTime)
            {
                colorScript.setSpriteRed();
                greenTime = 0;
            }
        }
    }

    private void checkBeat()
    {
        if (audioSource.timeSamples >= (nextBeat) * 44100) 
        { 
            nextBeat += timePerBeat;
            if (nextBeat >= clipLength)
            {
                nextBeat = 0;
            }
        }
    }

    private void checkClick()
    {
        if (audioSource.timeSamples >= (nextBeat - inputWindow + inputDelay) * 44100 && audioSource.timeSamples <= (nextBeat + inputWindow + inputDelay) * 44100)
        {
            colorScript.setSpriteGreen();
        }
    }
}
