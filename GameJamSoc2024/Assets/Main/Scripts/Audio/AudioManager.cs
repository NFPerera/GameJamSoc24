using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] AudioSource audio;
    [SerializeField][Range(0, 1)] float musicVol;
    [SerializeField][Range(1, 5)] float fadeTime;

    Coroutine fadingCoroutine;
    bool isTransitioning;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        if (audio.isPlaying && !isTransitioning) fadingCoroutine = StartCoroutine(TransitionMusic(index));
        else if (audio.isPlaying && isTransitioning)
        {
            StopCoroutine(fadingCoroutine);
            fadingCoroutine = StartCoroutine(TransitionMusic(index));
        }
        else fadingCoroutine = StartCoroutine(FadeInMusic(index));
    }

    IEnumerator TransitionMusic(int index)
    {
        isTransitioning = true;
        // FadeOut Transition
        while (audio.volume > 0)
        {
            audio.volume -= musicVol * Time.deltaTime / fadeTime;
            yield return null;
        }

        // Change music index
        audio.Stop();
        audio.clip = music[index];
        audio.Play();

        // FadeIn Transition
        while (audio.volume < musicVol)
        {
            audio.volume += musicVol * Time.deltaTime / fadeTime;
            yield return null;
        }
        isTransitioning = false;
    }

    IEnumerator FadeInMusic(int index)
    {
        isTransitioning = true;
        // Change music index
        audio.clip = music[index];
        audio.Play();

        // FadeIn Transition
        while (audio.volume < musicVol)
        {
            audio.volume += musicVol * Time.deltaTime / fadeTime;
            yield return null;
        }

        isTransitioning = false;
    }

    public void StopMusic()
    {
        audio.Stop();
    }
}
