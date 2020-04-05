using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private List<Card> Hand;
    public List<GameObject> CardHide;
    private string name;
    private int Id;
    public int GetId()
    {
        return Id;
    }

    public Player(string Name,int id)
    {
        CardHide = new List<GameObject>();
        SetHand(new List<Card>());
        this.name=Name;
        this.Id = id;
    }

    public List<Card> GetHand()
    {
        return Hand;
    }

    public void SetHand(List<Card> value)
    {
        Hand = value;
    }

    public string GetName()
    {
        return name;
    }
    public void RemouveCard(Card card)
    {
        Hand.Remove(card);
    }
    public bool IsInHand(Card card)
    {
        return Hand.Contains(card);
    }
    public void AddCard(Card card)
    {
        Hand.Add(card);
    }
}
