using UnityEngine;
using System.Collections;

public class SoundOn : MonoBehaviour {

    public GameObject soundObj1, soundObj2;
    bool isPressDown = true;
    private AudioSource audio1, audio2;
   
    SpriteRenderer redon, redoff;
    public void OnMouseDown()
    {
        AudioManager.Instance.PlayAudioOnClice();
        isPressDown = !isPressDown;
        if (isPressDown)
        {
            audio1=soundObj1.GetComponent<AudioSource>();
            audio2=soundObj2.GetComponent<AudioSource>();
            redon=this.transform.GetComponent<SpriteRenderer>();
           // redon.sprite = CatManager.Instance.soundOnSprite;
            audio1.Play();
            audio2.Play();
        }
        else
        {
            audio1 = soundObj1.GetComponent<AudioSource>();
            audio2 = soundObj2.GetComponent<AudioSource>();
            //redon.sprite = CatManager.Instance.soundOffSprite;
            audio1.Stop();
            audio2.Stop();
        }
    }

}
