using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleLayer : MonoBehaviour {

    private static ParticleLayer instance;
    public static ParticleLayer Instance
    {
        get { return instance; }
    }

    public GameObject particlePrefab;
    void Awake()
    {
        instance = this;
    }

    Queue<GameObject> particlePoolObj = new Queue<GameObject>();
    public void AddParticlePool(Vector3 pos,CatType type)
    {
        GameObject particleobj = null;
        if (particlePoolObj.Count>0)
        {
            
            particleobj=particlePoolObj.Dequeue();
            particleobj.SetActive(true);
        }
        else
        {
            particleobj = GameObject.Instantiate(particlePrefab) as GameObject;
        }
        particleobj.transform.position = pos;
        ParticleSystem parsy= particleobj.GetComponent<ParticleSystem>();
        parsy.GetComponent<Renderer>().material.mainTexture=CatManager.Instance.defaultSpriteList[(int)type].texture;
        parsy.Play();
        StartCoroutine(SetAction(particleobj));
    }
    IEnumerator SetAction( GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(false);
        
    }
    public void RemoveParticlePool(GameObject obj)
    {
        particlePoolObj.Enqueue(obj);
    }

    

}
