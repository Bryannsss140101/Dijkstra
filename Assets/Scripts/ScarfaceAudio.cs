using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfaceAudio : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private AudioSource voice;
    [SerializeField] private AudioSource steps;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> roars;
    [SerializeField] private AudioClip chirp;
    [SerializeField] private AudioClip step;

    private void PlayClip(AudioSource source, AudioClip clip,
        float min = 0.85f, float max = 1.125f)
    {
        source.pitch = Random.Range(min, max);
        source.PlayOneShot(clip);
    }

    public void PlayRoar()
    {
        PlayClip(voice, roars[Random.Range(0, roars.Count)]);
    }

    public void PlayChirp()
    {
        if (Random.Range(0f, 1f) < 0.25f)
            PlayClip(voice, chirp);
    }

    public void PlayStep()
    {
        PlayClip(steps, step);
    }
}