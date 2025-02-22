using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image oldImage;
    public Sprite[] newImage;
    public int imageIndex;
    [SerializeField] private CombinationPuzzle.Combination[] keyOrder;
    public CombinationPuzzle puzzleManager;

    public CombinationPuzzle.Combination GetCurrentKey()
    {
        return keyOrder[imageIndex];
    }

    public void ImageChange()
    {
        oldImage.sprite = newImage[imageIndex];
        puzzleManager.CheckCombination();
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
