using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class startUIButton : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] string targetMessage;
    public Color highlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }
    public void OnMouseDown()
    {
        transform.localScale = Vector3.one * 1.1f;
    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }

    }
}
