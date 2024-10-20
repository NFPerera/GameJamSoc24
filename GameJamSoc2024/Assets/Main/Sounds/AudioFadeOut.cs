using UnityEngine;
using System.Collections;

public  class AudioFadeOut: MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeTime;
    private float startVolume;

    private void Start() {
        startVolume = audioSource.volume;
        FadeOut();
    }

    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine() {
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}