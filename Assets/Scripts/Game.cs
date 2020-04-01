using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] cardsAssets;
    public string[] ListNamePlayer;
    public string currentPlayer="Antoine";

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
        placeColorSymbole();
        DisableCard();

    }


    // Update is called once per frame
    void Update()
    {
        cardPlay();
    }
    private void cardPlay()
    {
        Card cardP = null;
        foreach(Player player in ListPlayer)
        {
            if (player.GetName() == currentPlayer)
            {
                foreach (Card card in player.GetHand())
                {
                    Vector3 point = card.GetCard().transform.position;
                    if (point.x < 20f && point.x > -20f && point.y < 10f && point.y > -10f)
                    {
                        if (card.GetColor() == cardColor.GetColor() || card.GetVal() == cardSymbole.GetVal())
                        {

                            PacketJeux.Add(card);
                            DisableOneCard(cardSymbole);
                            cardSymbole = card;
                            placeNewSymbole();
                            TestPampouniette();
                            cardP = card;
                            break;
                        }
                    }
                }
                if (cardP != null)
                {
                   // player.GetHand().Remove(cardP);
                }
            }
        }
    }
    private void TestPampouniette()
    {
        try
        {
            if (PacketJeux[PacketJeux.Count - 1].GetVal() == PacketJeux[PacketJeux.Count - 2].GetVal())
            {
                if (PacketJeux[PacketJeux.Count - 2].GetVal() == PacketJeux[PacketJeux.Count - 3].GetVal())
                {
                    if (PacketJeux[PacketJeux.Count - 3].GetVal() == PacketJeux[PacketJeux.Count - 4].GetVal())
                    {
                        Debug.Log("PAMPOUNIETTE");
                    }
                }
            }
        }catch(System.Exception e)
        {

        }
       
    }
    private void DisableCard()
    {
        foreach(Card card in packet.packetCard)
        {
            DisableOneCard(card);
        }
    }
    private void DisableOneCard(Card card)
    {
        card.GetCard().transform.position = new Vector3(-100f, -100f, 100);
        card.GetCard().GetComponent<BoxCollider2D>().enabled = false;
        cardInvisible(card.GetCard());
    }

    private void SetUpGame()
    {
        cardColor = packet.Pioche();
        cardSymbole = packet.Pioche();
        PacketJeux = new List<Card>();
        PacketJeux.Add(cardSymbole);
        
        
    }
    private void placeColorSymbole()
    {
        cardColor.GetCard().transform.position = new Vector3(7.5f,0f,0f) ;
        cardVisible(cardColor.GetCard());
        cardColor.GetCard().GetComponent<BoxCollider2D>().enabled = false;
        placeNewSymbole();

    }
    private void placeNewSymbole()
    {
        cardSymbole.GetCard().transform.position = new Vector3(-7.5f, 0f, 0f);
        cardVisible(cardSymbole.GetCard());
        cardSymbole.GetCard().GetComponent<BoxCollider2D>().enabled = false;
    }
    private void DistribuerCard()
        {
        ListPlayer= new List<Player>();
        int NomberPlayer = ListNamePlayer.Length;
        if (NomberPlayer > 0)
        {
            int NbrCard = (int)(50 / NomberPlayer);
            if (NbrCard > 7) {
                NbrCard = 7;
            }
            for (int i = 0; i < NomberPlayer; i++)
            {
                Player newPlayer = new Player(ListNamePlayer[i],i);
                for (int j = 0; j < NbrCard; j++)
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
        foreach(Player player in ListPlayer)
        {
            if (player.GetName() == currentPlayer)
            {
                float posFirstCard = (float)(-NbrsCard * 2);
                for (int i = 0; i < NbrsCard; i++)
                {
                    GameObject cardSel = player.GetHand()[i].GetCard();
                    cardSel.transform.position = new Vector3(posFirstCard + i * 4, -24f, -(float)i - 2f);

                    cardVisible(cardSel);
                }
            }
        }
        placeOtherPlayerCard(NbrsCard);
    }
    private void placeOtherPlayerCard(int NbrsCard)
    {
        int nbrsPlayer = ListPlayer.Count;
        if (nbrsPlayer == 2)
        {
            placeBackcardHandUp(NbrsCard);
        }
        else if (nbrsPlayer == 3)
        {
            placeBackcardHandUp(NbrsCard);
            placeBackcardHandGauche(NbrsCard);
        }
        else if (nbrsPlayer == 4)
        {
            placeBackcardHandUp(NbrsCard);
            placeBackcardHandGauche(NbrsCard);
            placeBackcardHandDroite(NbrsCard);
        }
        else if (nbrsPlayer == 5)
        {
            placeBackcardHandGauche(NbrsCard);
            placeBackcardHandDroite(NbrsCard);
            placeBackcardDualUp(NbrsCard);
        }
        else if (nbrsPlayer == 6)
        {
            placeBackcardHandGauche(NbrsCard);
            placeBackcardHandDroite(NbrsCard);
            placeBackcardTrioUp(NbrsCard);
        }
        else
        {
            Debug.Log("Too much Player");
        }
    }
    private void placeBackcardHandUp(int NbrsCard)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(posFirstCard + i * 4, 24f, (float)i,0f);

        }
    }
    private void placeBackcardHandGauche(int NbrsCard)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(-48f, posFirstCard + i * 4, (float)i,90f);

        }
    }
    private void placeBackcardHandDroite(int NbrsCard)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(48f, posFirstCard + i * 4, (float)i, 90f);

        }
    }
    private void placeBackcardDualUp(int NbrsCard)
    {
        
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(-45 + i * 4, 24f, (float)i, 0f);
            placeBackcard(45 - i * 4, 24f, (float)i, 0f);

        }
    }

    private void placeBackcardTrioUp(int NbrsCard)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(posFirstCard + i * 4, 24f, (float)i, 0f);
            placeBackcard(-48 + i * 4, 24f, (float)i, 0f);
            placeBackcard(48 - i * 4, 24f, (float)i, 0f);

        }
    }
    private void placeBackcard(float x, float y, float z,float angle)
    {
        GameObject backCard=Instantiate(cardsAssets[0]);
        backCard.transform.position = new Vector3(x, y, z);
        backCard.transform.Rotate(0f, 0f, angle, Space.Self);
        cardVisible(backCard);
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
