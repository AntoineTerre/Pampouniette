using System.Collections.Generic;
using UnityEngine;
using System;


public class Packet
{
    public List<Card> packetCard;
    public Packet(GameObject[] cardsAssets)
    {
        packetCard = new List<Card>();
        for (int i = 1; i < 53; i++)
        {
            int color = 0;
            int val = 0;
            if (i <= 13)
            {
                color = 0;
                val = i;

            }
            else if (i <= 26)
            {

                color = 1;
                val = i - 13;

            }
            else if (i <= 39)
            {
                color = 2;
                val = i - 26;
            }
            else
            {
                color = 3;
                val = i - 39;
            }
            Card card = new Card(i, val, color, cardsAssets[i]);
            packetCard.Add(card);
        }
    }
    public Card Pioche()
    {
        int i = RandomNumber(1, packetCard.Count);
        
        Card card = packetCard[i];
        packetCard.Remove(card);
        return card;
    }
    public void RemoveCard(Card card)
    {
        packetCard.Remove(card);
    }
    private int RandomNumber(int min, int max)
    {
        int alea= UnityEngine.Random.Range(min, max);
        return alea;
    }
}
