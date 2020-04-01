using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] cardsAssets;
    public string[] ListNamePlayer;
    private List<Player> ListPlayer;
    private Packet packet;
    private Card cardColor;
    private Card cardSymbole;
    private List<Card> PacketJeux;
    // Start is called before the first frame update
    void Start()
    {
        packet=new Packet(cardsAssets);
        DistribuerCard();
        SetUpGame();
        PlaceCardStart();



    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetUpGame()
    {
        cardColor = packet.Pioche();
        cardSymbole = packet.Pioche();
        PacketJeux = new List<Card>();
        PacketJeux.Add(cardSymbole);
        
    }
    private void DistribuerCard()
        {
        ListPlayer= new List<Player>();
        int NomberPlayer = ListNamePlayer.Length;
        if (NomberPlayer > 0)
        {
            int NbrCard = (int)(50 / NomberPlayer);
            for (int i = 0; i < NomberPlayer - 1; i++)
            {
                Player newPlayer = new Player(ListNamePlayer[i]);
                for (int j = 0; j < NbrCard - 1; j++)
                {
                    newPlayer.AddCard(packet.Pioche());
                }
                ListPlayer.Add(newPlayer);
            }
        }
        else
        {

        }
       

    }
    private void PlaceCardStart()
    {
        AllCardInvisible();
        int NbrsCard=ListPlayer[0].GetHand().Count;
        int countPlayer = 0;
        foreach(Player player in ListPlayer)
        {
            for(int i = 0; i < NbrsCard - 1; i++)
            {

            }
            countPlayer++;
        }
    }

    private void AllCardInvisible()
    {
        foreach (GameObject card in cardsAssets)
        {
            cardInvisible(card);
        }
    }

    private void cardInvisible(GameObject card)
    {
        SpriteRenderer sr = card.GetComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(1f, 1f, 1f, 0f);
    }

    private void cardVisible(GameObject card)
    {
        SpriteRenderer sr = card.GetComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

}
