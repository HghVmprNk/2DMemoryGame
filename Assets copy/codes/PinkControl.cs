using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PinkControl : MonoBehaviour
{
    public int gridRows = 2;
    public int gridCols = 4;    // deleted const from all for 
    public float offsetX = 2f;
    public float offsetY = 2.5f; // you can change some of the stuff in the inspector. See vid for more information 

    private int score = 0;

    
    private Hard_Mem firstRevealed;
    private Hard_Mem secondRevealed;

    [SerializeField] int[] cardIdsForDeck = { 0, 1, 2, 3 }; // new
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] Hard_Mem originalCard;
    [SerializeField] Sprite[] images;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(Hard_Mem card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            //Debug.Log("Match? " + (firstRevealed.Id == secondRevealed.Id));
            StartCoroutine(CheckMatch());
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("HardScene"); 
    }


    void Start()
    {
        //int id = Random.Range(0, images.Length);
        //originalCard.SetCard(id, images[id]);
        Vector3 startPos = originalCard.transform.position;

        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] numbers = ProduceDeck(cardIdsForDeck); // new 
        numbers = ShuffleArray(numbers);

        int cardsAdded = 0; //new; this is adds a counter for the number of cards placed 

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                if (cardsAdded >= numbers.Length)
                {
                    continue;
                } // new 

                Hard_Mem card;

                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as Hard_Mem;
                }

                //int index = j * gridCols + i;
                int index = cardsAdded; //new, keeps track of #'s added 
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

                //adding a debug line
                Debug.Log($"Placing card {cardsAdded} with ID {id} at position {i}, -{j} from top-left card.");

                // add an increment at the end of the inner loop's code
                cardsAdded++; //new

            }


        }


    }
    // this is adding a private method that produces a deck of cards
    private int[] ProduceDeck(int[] numbers)
    {
        int[] newArray = new int[numbers.Length * 2];
        for (int i = 0; i < newArray.Length; i += 2)
        {
            newArray[i] = numbers[i / 2];
            newArray[i + 1] = numbers[i / 2];
        }
        return newArray;
    } // this whole section is new
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int rand = Random.Range(i, newArray.Length);
            newArray[i] = newArray[rand];
            newArray[rand] = temp;
        }
        return newArray;
    }
    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            //Debug.Log($"Score:  {score}");
            scoreLabel.text = $"Score: {score}";

        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        firstRevealed = null;
        secondRevealed = null;
    }
}
