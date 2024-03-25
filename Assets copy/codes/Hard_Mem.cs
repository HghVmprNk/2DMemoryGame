using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_Mem : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] PinkControl controller;

    private int _id;

    public int Id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void OnMouseDown()
    {
        //Debug.Log("Testing 1 2 3");
        //cardBack.SetActive(false);

        if(cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }


}
