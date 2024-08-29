using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage images;
    [SerializeField] private float _x, _y, _z;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        images.uvRect = new Rect(images.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, images.uvRect.size);
    }
}
