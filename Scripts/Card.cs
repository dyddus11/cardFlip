using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cardRenderer;
    
    [SerializeField]
    private Sprite animalSprite;

    [SerializeField]
    private Sprite backSprite;

    private bool isFlipped;
    private bool isFlipping;

    public int CardId;
    
    void Start()
    {
        
    }

    public void FlipCard()
    {
        isFlipping = true;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z);

        transform.DOScale(targetScale, 0.2f);
        StartCoroutine(Flip(originalScale));
      
    }

    void Update()
    {
        
    }

    public void SetCardId(int id)
    {
        CardId = id;
    }

    public void SetAnimalSprite(Sprite sprite)
    {
        animalSprite = sprite;
    }

    IEnumerator Flip(Vector3 originalScale)
    {
        yield return new WaitForSeconds(0.2f);
        
        isFlipped = !isFlipped;
        
        if (isFlipped)
        {
            cardRenderer.sprite = animalSprite;
        }
        else
        {
            cardRenderer.sprite = backSprite;
        }
        
        transform.DOScale(originalScale, 0.2f);
        yield return new WaitForSeconds(0.2f);
        isFlipping = false;
    }

    private void OnMouseDown()
    {
        if (!isFlipping && !GameManger.instance.pause && !isFlipped)
        {
            GameManger.instance.CardClicked(this);
        }
    }
}
