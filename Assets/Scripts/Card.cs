using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int id;  // color*13+val
    private int val = 0;// val = nbrs card ou A=1 , J=11 , D=12 , R=13
    private int color; // carreaux =0 , coeur =1 , pics = 2 , trefle= 3
    private GameObject card;
    public Card(int id , int val,int color, GameObject card)
    {
        this.SetId(id);
        this.SetVal(val);
        this.SetColor(color);
        this.SetCard(card);
    }

    public GameObject GetCard()
    {
        return card;
    }

    public void SetCard(GameObject value)
    {
        card = value;
    }

    public int GetColor()
    {
        return color;
    }

    public void SetColor(int value)
    {
        color = value;
    }

    public int GetVal()
    {
        return val;
    }

    public void SetVal(int value)
    {
        val = value;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int value)
    {
        id = value;
    }
}
