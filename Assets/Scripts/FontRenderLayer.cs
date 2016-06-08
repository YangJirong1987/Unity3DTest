using UnityEngine;
using System.Collections;

public class FontRenderLayer : MonoBehaviour
{


    void Awake()
    {
        this.transform.GetComponent<Renderer>().sortingLayerName = "UI";
        this.transform.GetComponent<Renderer>().sortingOrder = 1;
    }
}
