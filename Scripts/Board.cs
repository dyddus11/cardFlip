using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Sprite[] cardSprites;
    private List<int> cardIDList = new List<int>();
    private List<Card> cardList = new List<Card>();
    
    void Start()
    {
        GenerateCardId();
        shuffleCardId();
        InitBoard();
    }

    void GenerateCardId()
    {
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardIDList.Add(i);
            cardIDList.Add(i);
        }
    }

    void shuffleCardId()
    {
        int cardCount = cardIDList.Count;

        for (int i = 0; i < cardCount; i++)
        {
            int randomIdx = Random.Range(i, cardCount);
            int temp = cardIDList[randomIdx];
            cardIDList[randomIdx] = cardIDList[i];
            cardIDList[i] = temp;
        }
        
    }

    void Update()
    {
        
    }

    void InitBoard()
    {
        float space = 2.2f;
        
        int colunm = 4;
        int row = 6;
        int cardIndex = 0;
        
        Vector3 startPos = transform.position - new Vector3(
            (row - 1) * space * 0.5f, 
            (colunm - 1) * space * 0.5f,
            0);
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < colunm; j++)
            {
                Vector3 position = startPos + new Vector3(i * space, j * space, 0);
                
                GameObject cardObject = Instantiate(cardPrefab, position, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();

                int cardId = cardIDList[cardIndex++];
                
                card.SetCardId(cardId);
                card.SetAnimalSprite(cardSprites[cardId]);
                
            }
        }
    }
}
