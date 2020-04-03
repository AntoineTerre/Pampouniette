using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject Compteur;

    private FuckAndPampouniette pCont;
    // Start is called before the first frame update
    void Start()
    {
        Compteur = GameObject.Find("Compteur");
        packet=new Packet(cardsAssets);
        DistribuerCard();
        SetUpGame();
        PlaceCardStart();
        placeColorSymbole();
        DisableCard();
        pCont = new FuckAndPampouniette(ListPlayer,currentPlayer) ;
    }


    // Update is called once per frame
    void Update()
    {
        cardPlay();
    }
    public List<Card> GetPacketJeux()
    {
        return PacketJeux;
    }
    private void cardPlay()
    {
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
                            Compteur.GetComponent<EndGameControleur>().ResetDecompte();
                            PacketJeux.Add(card);
                            DisableOneCard(cardSymbole);
                            cardSymbole = card;
                            bool TestPamp=TestPampouniette();
                            
                            pCont.onFuckEvent(TestPamp);
                            break;
                        }
                    }
                }
                if (cardSymbole != null)
                {
                   player.GetHand().Remove(cardSymbole);
                   placeNewSymbole();
                }
            }

        }
    }
    private bool TestPampouniette()
    {
        if (PacketJeux.Count > 3)
        {
                if (PacketJeux[PacketJeux.Count - 1].GetVal() == PacketJeux[PacketJeux.Count - 2].GetVal())
                {
                    if (PacketJeux[PacketJeux.Count - 2].GetVal() == PacketJeux[PacketJeux.Count - 3].GetVal())
                    {
                        if (PacketJeux[PacketJeux.Count - 3].GetVal() == PacketJeux[PacketJeux.Count - 4].GetVal())
                        {
                            Debug.Log("PAMPOUNIETTE");
                            return true;
                        }
                    }
                }

        }

        return false;

      
       
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
        CreateAllTextName();



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
            Player curPlayer= new Player(currentPlayer, 0);
            for (int j = 0; j < NbrCard; j++)
            {
                curPlayer.AddCard(packet.Pioche());
            }
            ListPlayer.Add(curPlayer);
            int jou = 0;
            for (int i = 0; i < NomberPlayer; i++)
            {
                if(currentPlayer!= ListNamePlayer[i])
                {
                    Player newPlayer = new Player(ListNamePlayer[i], jou+1);
                    for (int j = 0; j < NbrCard; j++)
                    {
                        newPlayer.AddCard(packet.Pioche());
                    }
                    ListPlayer.Add(newPlayer);
                    jou++;
                }
                
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
    private void CreateAllTextName()
    {
        int nbrsPlayer = ListPlayer.Count;
        int j = 0;
        for(int i=0; i < nbrsPlayer; i++)
        {
            if(ListPlayer[i].GetName() != currentPlayer)
            {
                if (nbrsPlayer == 2)
                {
                    CreateTextName(ListPlayer[i].GetName(),-6f, 21f, -7f, 0f);
                    
                }
                else if(nbrsPlayer == 3)
                {
                    if (j == 0)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -45f, -6f, -7f, 90f);
                        j++;
                    }
                    else
                    {
                        CreateTextName(ListPlayer[i].GetName(), -6f, 21f, -7f, 0f);
                    }
                }
                else if (nbrsPlayer == 4)
                {
                    if (j == 0)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -45f, -6f, -7f, 90f);
                        j++;
                    }else if (j == 1)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -6f, 21f, -7f, 0f);
                        j++;
                    }
                    else
                    {
                        CreateTextName(ListPlayer[i].GetName(), 45f, -6f, -7f, 90f);
                    }
                }
                else if (nbrsPlayer == 5)
                {
                    if (j == 0)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -45f, -6f, -7f, 90f);
                        j++;
                    }
                    else if (j == 1)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -36f, 21f, -7f, 0f);
                        j++;
                    }else if (j == 2)
                    {
                        CreateTextName(ListPlayer[i].GetName(), 31f, 21f, -7f, 0f);
                        j++;
                    }
                    else
                    {
                        CreateTextName(ListPlayer[i].GetName(), 45f, -6f, -7f, 90f);
                    }
                }
                else if (nbrsPlayer == 6)
                {
                    if (j == 0)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -45f, -6f, -7f, 90f);
                        j++;
                    }
                    else if (j == 1)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -40f, 21f, -7f, 0f);
                        j++;
                    }
                    else if (j == 2)
                    {
                        CreateTextName(ListPlayer[i].GetName(), -6f, 21f, -7f, 0f);
                        j++;
                    }else if(j==3){
                        CreateTextName(ListPlayer[i].GetName(), 34f, 21f, -7f, 0f);
                        j++;
                    }
                    else
                    {
                        CreateTextName(ListPlayer[i].GetName(), 45f, -6f, -7f, 90f);
                    }
                }
            }
        }
    }
    private void CreateTextName(string Name, float x, float y, float z, float angle)
    {
        GameObject newGO = new GameObject("myTextGO");
        newGO.AddComponent<TextMesh>().text =Name;
        newGO.transform.position = new Vector3(x,y,z) ;
        newGO.transform.Rotate(0f, 0f, angle, Space.Self);
        newGO.GetComponent<TextMesh>().characterSize = 3;
        newGO.GetComponent<TextMesh>().color = new Color(1f,0f,0f,1f);
        newGO.GetComponent<TextMesh>().fontStyle = FontStyle.Bold;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        newGO.GetComponent<TextMesh>().font = ArialFont;


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
