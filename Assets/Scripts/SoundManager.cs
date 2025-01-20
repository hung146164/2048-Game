using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource backgroundSource;
    private AudioSource SFXSource;
    [Header("SFX music")]
    public AudioClip moveTileSFX;
    public AudioClip enterButtonSFX;
    public AudioClip clickButtonSFX;

    [Header("Background Music")]
    public AudioClip background1;
    public AudioClip background2;

    private Queue<AudioClip> backgroundQueue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            AudioSource[] audioSources = GetComponents<AudioSource>();
            if (audioSources.Length < 2)
            {
                SFXSource = gameObject.AddComponent<AudioSource>();
                backgroundSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                SFXSource = audioSources[0];
                backgroundSource = audioSources[1];
            }

            backgroundSource.loop = false; 

            backgroundQueue = new Queue<AudioClip>();
            backgroundQueue.Enqueue(background1);
            backgroundQueue.Enqueue(background2);

            StartCoroutine(PlayBackgroundMusic());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayBackgroundMusic()
    {
        while (true)
        {
            if (!backgroundSource.isPlaying)
            {
                // Play the next song in the queue
                AudioClip nextClip = backgroundQueue.Dequeue();
                backgroundSource.clip = nextClip;
                backgroundSource.Play();

                // Re-add the clip to the end of the queue for rotation
                backgroundQueue.Enqueue(nextClip);
            }
            yield return null; // Wait until the next frame to check again
        }
    }

    public void PlaySFX(AudioClip audioclip)
    {
        SFXSource.PlayOneShot(audioclip);
    }
    public void PlayEnterButtonSFX()
    {
        SFXSource.PlayOneShot(enterButtonSFX);
    }
    public void PlayClickButtonSFX()
    {
        SFXSource.PlayOneShot(clickButtonSFX);
    }
    public void PlayMoveTileSFX()
    {
        SFXSource.PlayOneShot(moveTileSFX);
    }
    public void PlayBackground(AudioClip audioclip)
    {
        backgroundSource.clip = audioclip;
        backgroundSource.Play();
    }
}
