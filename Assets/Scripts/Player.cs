using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private List<Card> Hand; 
    private string name;
    public Player(string Name)
    {
        SetHand(new List<Card>());
        this.name=Name;
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
