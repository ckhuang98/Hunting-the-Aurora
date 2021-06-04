﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScrolling : MonoBehaviour
{
    public GameObject artCredits;
    Text artText;

    public GameObject programCredits;
    Text programText;

    public GameObject producerCredits;
    Text producerText;

    public GameObject soundCredits;
    Text soundText;

    public GameObject designCredits;
    Text designText;

    public GameObject writingCredits;
    Text writingText;
    // Start is called before the first frame update
    void Start()
    {
        artText = artCredits.GetComponent<Text>();
        programText = programCredits.GetComponent<Text>();
        producerText = producerCredits.GetComponent<Text>();
        soundText = soundCredits.GetComponent<Text>();
        designText = designCredits.GetComponent<Text>();
        writingText = writingCredits.GetComponent<Text>();

        StartCoroutine(FadeinArtCredits(52, artText));
    }

    IEnumerator FadeinArtCredits(float delayTime, Text i)
    {
        
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);

        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(3.5f);
        StartCoroutine(FadeOutArtCredits(i));
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutArtCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        StartCoroutine(FadeinProgramCredits(programText));
    }

    IEnumerator FadeinProgramCredits(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(FadeOutProgramCredits(i));
        
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutProgramCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        StartCoroutine(FadeinProducerCredits(producerText));
        
    }

    IEnumerator FadeinProducerCredits(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOutProducerCredits(i));
        
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutProducerCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        StartCoroutine(FadeinSoundCredits(soundText));
    }

    IEnumerator FadeinSoundCredits(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOutSoundCredits(i));
        
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutSoundCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        StartCoroutine(FadeinDesignCredits(designText));
    }

    IEnumerator FadeinDesignCredits(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOutDesignCredits(i));
        
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutDesignCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        StartCoroutine(FadeinWritingCredits(writingText));
    }

    IEnumerator FadeinWritingCredits(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 1));
            //artText.color = i.color;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOutWritingCredits(i));
        
        //Do the action after the delay time has finished.
    }

    IEnumerator FadeOutWritingCredits(Text i){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        
    }
}