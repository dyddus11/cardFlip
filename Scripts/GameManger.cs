using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    private bool isGameOver = false;
    public static GameManger instance;
    private Card flippedCard;
    public bool pause;

    [SerializeField] private Slider tiemOutSlider;
    private float tiemLimit = 90f;
    private float currentTime = 0f;
    float totalMatch = 12;
    private AudioSource audioSource;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverPanelText;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        currentTime = tiemLimit;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CountDownTimer());
    }
    
    public void CardClicked(Card card)
    {
        if(isGameOver)
            return;
        
        card.FlipCard();

        if (flippedCard == null)
            flippedCard = card;
        else
        {
            pause = true;
            StartCoroutine(CheckMatch(flippedCard, card));
        }
    }

    IEnumerator CountDownTimer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            tiemOutSlider.value = currentTime / tiemLimit;

            if (currentTime <= 0)
            {
                GameOver(false);
            }

            yield return null;
        }
    }

    IEnumerator CheckMatch(Card card1, Card card2)
    {
        if (card1.CardId == card2.CardId && (card1 != card2))
        {
            audioSource.Play();

            yield return new WaitForSeconds(1f);
            
            Destroy(card1.gameObject);
            Destroy(card2.gameObject);
            totalMatch--;

            if (totalMatch <= 0)
            {
                GameOver(true);
            }

        }
        else
        {
            yield return new WaitForSeconds(1f);
            
            card1.FlipCard();
            card2.FlipCard();
            
            yield return new WaitForSeconds(0.4f);
        }

        flippedCard = null;
        pause = false;
    }

    void GameOver(bool success)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StopCoroutine(CountDownTimer());
        
            if (success)
            {
                gameOverPanelText.SetText("You Win");
            }
            else
            {
                gameOverPanelText.SetText("Game Over");
            }
        
            Invoke("ShowGameOverPanel",0f);
        }

    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
