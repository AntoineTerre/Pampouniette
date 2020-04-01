using System.Collections.Generic;
using UnityEngine;
using System;


public class Packet
{
    private List<Card> packetCard;
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
        int i = RandomNumber(0, packetCard.Count - 1);
        Card card = packetCard[i];
        packetCard.Remove(card);
        return card;
    }
    private int RandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min,max);
    }
}
