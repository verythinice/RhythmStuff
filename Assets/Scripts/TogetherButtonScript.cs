using UnityEngine;
using System.Collections;

public class TogetherButtonScript : MonoBehaviour {
    private AudioSource audioSource;
    private ChangeColorScript colorScript;
    private float greenTime;
    private float totalGreenTime = .1f;

    private float previousBeat;
    private float currentBeat;
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
        previousBeat = 0;
        currentBeat = 0;
        timePerBeat = 60 / (bpm);
        clipLength = audioSource.clip.length;
        nextBeat = currentBeat + timePerBeat;
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
            previousBeat = currentBeat;
            currentBeat = nextBeat;
            nextBeat += timePerBeat;
            if (nextBeat >= clipLength)
            {
                nextBeat = 0;
            }
        }
    }

    private void checkClick()
    {
        print((audioSource.timeSamples - ((previousBeat + inputDelay) * 44100))+" "+ (audioSource.timeSamples - ((currentBeat + inputDelay) * 44100))+" "+ (audioSource.timeSamples - ((nextBeat + inputDelay) * 44100))+" "+check3Beats());
        if (check3Beats())
        {
            colorScript.setSpriteGreen();
        }
    }

    private bool check3Beats()
    {
        return ((Mathf.Abs(audioSource.timeSamples-((nextBeat + inputDelay) * 44100)) <= (inputWindow*44100))
            || (Mathf.Abs(audioSource.timeSamples - ((currentBeat + inputDelay) * 44100)) <= (inputWindow*44100))
            || (Mathf.Abs(audioSource.timeSamples - ((previousBeat + inputDelay) * 44100)) <= (inputWindow*44100)));
    }
}
