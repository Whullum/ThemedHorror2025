using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image oldImage;
    public Sprite[] newImage;
    int imageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageChange()
    {
        oldImage.sprite = newImage[imageIndex];
    }

    public void ArrowUp()
    {
        if (imageIndex < newImage.Length-1)
        {
            imageIndex++;
        }
        else { imageIndex=0;}
        
    }

    public void ArrowDown()
    {
        if (imageIndex>0)
        {
            imageIndex--;
        }

        else {imageIndex = newImage.Length-1;}
        
    }
}
