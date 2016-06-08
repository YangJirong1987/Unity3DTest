using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }

    private AudioSource audio;
    public AudioClip xiaoDiao, onClick, gameOver, guoGuan, selectedSound,poJiLu,dropAudio,replaceAudio;
    void Start()
    {
        audio = transform.GetComponent<AudioSource>();
    }
    public void PlayAudioxiaoDiao()
    {
        audio.PlayOneShot(xiaoDiao);
    }
    public void PlayAudioSelected()
    {
        audio.PlayOneShot(selectedSound);
    }
    public void PlayAudioGuoGuan()
    {
        audio.PlayOneShot(guoGuan);
    }
    public void PlayAudioGameOver()
    {
        audio.PlayOneShot(gameOver);
    }
    public void PlayAudioOnClice()
    {
        audio.PlayOneShot(onClick);
    }
    public void PlayAudioHigtScore()
    {
        audio.PlayOneShot(poJiLu);
    }
    public void PlayAudioDrop()
    {
        audio.PlayOneShot(dropAudio);
    }
    public void PlayAudioReplaceAudio()
    {
        audio.PlayOneShot(replaceAudio);
    }

}
