using UnityEngine;
using System.Collections;

public class OnDestroyScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(OnDestroyobj());
	}
    private float time;
    public float Time
    {
        get
        {return time;}
        set{
            time=value;

        }
    }
    IEnumerator OnDestroyobj()
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(this.gameObject);
       
    }
    

}
