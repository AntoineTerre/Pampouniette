using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Game : MonoBehaviourPunCallbacks
{
    public GameObject[] cardsAssets;
    public string[] ListNamePlayer;

    private List<Player> ListPlayer;
    private Packet packet;
    private Card cardColor;
    private Card cardSymbole;
    private List<Card> PacketJeux;

    private string currentPlayer;

    private List<int> cardNoDisable;

    private GameObject Compteur;
    private PhotonView photonViewGame;
    private FuckAndPampouniette pCont;

    private bool SetUPClient = false;


    private GameObject FuckName;
    private GameObject OtherFuck;
    private GameObject NameOtherFuck;
    private GameObject OtherPamp;
    private GameObject NameOtherPamp;

    private GameObject Pampouniette;
    private GameObject PampText;
    private GameObject namePlayPampGO;

    // Start is called before the first frame update
    void Start()
    {
        PacketJeux = new List<Card>();
        OtherFuck = GameObject.Find("OtherFuck");
        NameOtherFuck = GameObject.Find("NameOtherFuck");
        OtherPamp = GameObject.Find("OtherPamp");
        NameOtherPamp = GameObject.Find("NameOtherPamp");
        namePlayPampGO = GameObject.Find("namePlayPamp");

        OtherPamp.SetActive(false);
        OtherFuck.SetActive(false);

        Pampouniette = GameObject.Find("Pampouniette");
        PampText = GameObject.Find("NamePampou");
        FuckName = GameObject.Find("Pseudo");
        cardNoDisable = new List<int>();
        cardNoDisable.Add(0);
        ListPlayer = new List<Player>();
        photonViewGame = PhotonView.Get(this);
        currentPlayer = PhotonNetwork.LocalPlayer.NickName;
        SetUpNameString();
        Compteur = GameObject.Find("Compteur");
        if (PhotonNetwork.IsMasterClient)
        {
            packet = new Packet(cardsAssets);
            DistribuerCard();
            placeColorSymbole();
        }
      

    }

    // Update is called once per frame
    void Update()
    {
        if(!SetUPClient && (ListPlayer.Count == ListNamePlayer.Length && cardSymbole !=null))
        {
            Debug.Log("SetUP call");
            SetUpGame();
            PlaceCardStart();
            DisableCard();
            pCont = new FuckAndPampouniette(ListPlayer,photonViewGame,currentPlayer);
            SetUPClient = true;
        }
        if (SetUPClient)
        {
            cardPlay();

            placeNewSymbole();
        }
        

        
    }

    private void SetUpNameString()
    {
        Dictionary<int, Photon.Realtime.Player> pList = Photon.Pun.PhotonNetwork.CurrentRoom.Players;
        ListNamePlayer = new string[pList.Count];
        int i = 0;
        foreach (KeyValuePair<int, Photon.Realtime.Player> p in pList)
        {
            ListNamePlayer[i] = p.Value.NickName;
            i++;
        }
    }
    public List<Card> GetPacketJeux()
    {
        return PacketJeux;
    }
    private void cardPlay()
    {
        bool cardPlayBool = false;
        foreach (Player player in ListPlayer)
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
                            //ICI le decompte est MIS EN PAUSE
                            Compteur.GetComponent<EndGameControleur>().DecomptePause();


                            PacketJeux.Add(card);

                            DisableOneCard(cardSymbole.GetCard());
                            cardSymbole = card;
                            bool TestPamp=TestPampouniette();
                            photonViewGame.RPC("SendNewSymboleCardToOther", RpcTarget.Others, cardSymbole.GetId(), currentPlayer);
                            photonViewGame.RPC("RemoveBackCardFromPlayer", RpcTarget.Others,currentPlayer);
                            pCont.onFuckEvent(TestPamp);
                            cardPlayBool = true;
                            break;
                        }
                    }
                }
                if (cardSymbole != null && cardPlayBool)
                {
                    player.GetHand().Remove(cardSymbole);
                }
            }

        }
    }
    [PunRPC]
    void ChangeFuckNameOther(string name)
    {
        OtherFuck.SetActive(false);
        FuckName.GetComponent<TextMesh>().text = name;
        Debug.Log("Fuck name Change");
    }
    [PunRPC]
    void OnPampOther(string namePlay)
    {
        OtherPamp.SetActive(true);
        NameOtherPamp.GetComponent<Text>().text = namePlay;
    }
    [PunRPC]
    void OnPampSelect(string name,string namePlayPamp)
    {
        Pampouniette.SetActive(true);
        PampText.GetComponent<Text>().text = name;
        namePlayPampGO.GetComponent<Text>().text = namePlayPamp;

    }
    [PunRPC]
    void UnactivePamp()
    {
        Pampouniette.SetActive(false);
    }
    [PunRPC]
    void OnFuckOther(string namePlay)
    {
        OtherFuck.SetActive(true);
        NameOtherFuck.GetComponent<Text>().text = namePlay;
    }
    [PunRPC]
    void SendNewSymboleCardToOther(int symbID,string playerPlay)
    {
        DisableOneCard(cardSymbole.GetCard());
        int[] cardSymbValEtColor = DetermColorSymbole(symbID);
        cardSymbole = new Card(symbID, cardSymbValEtColor[1], cardSymbValEtColor[0], cardsAssets[symbID]);
        PacketJeux.Add(cardSymbole);
        foreach(Player player in ListPlayer)
        {
            if (player.GetName() == playerPlay)
            {
                player.GetHand().Remove(cardSymbole);
                break;
            }
        }
        placeNewSymbole();
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
        Debug.Log("Disable is call");
        int cont = 0;
        foreach(GameObject card in cardsAssets)
        {
            bool Disable = true;
            foreach(int i in cardNoDisable)
            {
                if (i == cont)
                {
                    Disable = false;
                    break;
                }
                cont++;
            }
            if (Disable)
            {
                DisableOneCard(card);
            }
            
        }
    }
    private void DisableOneCard(GameObject card)
    {
        card.transform.position = new Vector3(-100f, -100f, 100);
        card.GetComponent<BoxCollider2D>().enabled = false;
        cardInvisible(card);
    }
    //setUp cardColor et carte sumbole ainsi que packet jeux
    private void SetUpGame()
    {
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
        placePacketSymbole();

    }
    private void placePacketSymbole()
    {

            for (int i = 0; i < PacketJeux.Count-1; i++)
            {
                Card card = PacketJeux[i];
                cardInvisible(card.GetCard());
            }
            for (int i = 1; i < PacketJeux.Count && i < 4; i++)
            {
                Card card = PacketJeux[PacketJeux.Count - i-1];
                card.GetCard().transform.position = new Vector3(-7.5f - i, 0f, i);
                cardVisible(card.GetCard());
            }
        
    }
    private int[] DetermColorSymbole(int IDCard)
    {
        int color = 0;
        int val = 0;
        if (IDCard <= 13)
        {
            color = 0;
            val = IDCard;

        }
        else if (IDCard <= 26)
        {

            color = 1;
            val = IDCard - 13;

        }
        else if (IDCard <= 39)
        {
            color = 2;
            val = IDCard - 26;
        }
        else
        {
            color = 3;
            val = IDCard - 39;
        }
        int[] R = new int[2];
        R[0] = color;
        R[1] = val;
        return R;
    }
    
    [PunRPC]
    void colorSymboleClient(int IDColor,int IDSymbole)
    {
        Debug.Log("colorSymbole is call");
        int[] colorSymboleForCOLOR = DetermColorSymbole(IDColor);
        int[] colorSymboleForSYMBOLE = DetermColorSymbole(IDSymbole);
        cardColor = new Card(IDColor, colorSymboleForCOLOR[1], colorSymboleForCOLOR[0], cardsAssets[IDColor]);
        cardSymbole = new Card(IDSymbole, colorSymboleForSYMBOLE[1], colorSymboleForSYMBOLE[0], cardsAssets[IDSymbole]);
        cardNoDisable.Add(IDColor);
        cardNoDisable.Add(IDSymbole);
        placeColorSymbole();

    }
    private void DistribuerCard()
        {
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
                    int[] listCardIDHandPlayer = new int[NbrCard];
                    for (int j = 0; j < NbrCard; j++)
                    {
                        Card newCard = packet.Pioche();
                        newPlayer.AddCard(newCard);
                        listCardIDHandPlayer[j]= newCard.GetId();
                        cardNoDisable.Add(newCard.GetId());
                }
                    ListPlayer.Add(newPlayer);
                    photonViewGame.RPC("SetCardOtherClient", RpcTarget.Others, listCardIDHandPlayer, ListNamePlayer[i],i) ;
 
            }
            cardColor = packet.Pioche();
            cardSymbole = packet.Pioche();
            photonViewGame.RPC("colorSymboleClient", RpcTarget.Others, cardColor.GetId(), cardSymbole.GetId());
            cardNoDisable.Add(cardColor.GetId());
            cardNoDisable.Add(cardSymbole.GetId());
            
        }

        else
        {

        }
       

    }

    [PunRPC]
    void SetCardOtherClient(int[] listCardIDHandPlayer, string playerName,int idPlayer)
    {
       // Debug.Log("SetCardOtherClient is call for " + playerName);
       
                Player newPlayer = new Player(playerName, idPlayer);
                foreach(int CardID in listCardIDHandPlayer)
                {
                    int color = 0;
                    int val = 0;
                    if (CardID <= 13)
                    {
                        color = 0;
                        val = CardID;

                    }
                    else if (CardID <= 26)
                    {

                        color = 1;
                        val = CardID - 13;

                    }
                    else if (CardID <= 39)
                    {
                        color = 2;
                        val = CardID - 26;
                    }
                    else
                    {
                        color = 3;
                        val = CardID - 39;
                    }
                    //Debug.Log("Add Card:" + CardID + " ,To Player:" + playerName);
                    Card newCard = new Card(CardID, val, color, cardsAssets[CardID]);
                    newPlayer.AddCard(newCard);
                    cardNoDisable.Add(CardID);
                }
                ListPlayer.Add(newPlayer);
        }
    [PunRPC]
    void RemoveBackCardFromPlayer(string playerPlay)
    {
        Debug.Log("Remove on card from"+playerPlay);
        for (int i = 0; i < ListPlayer.Count; i++)
        {
            if (ListPlayer[i].GetName() == playerPlay)
            {
                DisableOneBackCard(ListPlayer[i].CardHide[ListPlayer[i].CardHide.Count-1]);
                ListPlayer[i].CardHide.RemoveAt(ListPlayer[i].CardHide.Count-1);
                break;
            }
        }
    }
    private void DisableOneBackCard(GameObject card)
    {
        card.transform.position = new Vector3(-100f, -100f, 100);
        cardInvisible(card);
    }
    //Place card for all client
    private void PlaceCardStart()
    {
        AllCardInvisible();
        cardVisible(cardColor.GetCard());
        cardVisible(cardSymbole.GetCard());
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
            for(int i=0;i< nbrsPlayer; i++)
            {
                if(ListPlayer[i].GetName() != currentPlayer)
                {
                    placeBackcardHandUp(NbrsCard, i);
                }
            }
            

        }
        else if (nbrsPlayer == 3)
        {
             int j = 0;
             for(int i=0;i< nbrsPlayer; i++)
             {
                if(ListPlayer[i].GetName() != currentPlayer)
                {
                    if (j == 0)
                    {
                        placeBackcardHandGauche(NbrsCard, i);
                        
                    }
                    else
                    {
                        placeBackcardHandUp(NbrsCard, i);
                    }
                    j++;
                }
            }
        }
        else if (nbrsPlayer == 4)
        {
            int j = 0;
            for (int i = 0; i < nbrsPlayer; i++)
            {
                if (ListPlayer[i].GetName() != currentPlayer)
                {
                    if (j == 0)
                    {
                        placeBackcardHandGauche(NbrsCard, i);
                        
                    }
                    else if (j == 1)
                    {
                        placeBackcardHandUp(NbrsCard, i);
                    }
                    else
                    {
                        placeBackcardHandDroite(NbrsCard,i);
                    }
                    j++;
                }
            }
        }
        else if (nbrsPlayer == 5)
        {
            int j = 0;
            int playerIndex1 = -1;
            int playerIndex2 = -1;
            for (int i = 0; i < nbrsPlayer; i++)
            {
                if (ListPlayer[i].GetName() != currentPlayer)
                {
                    if (j == 0)
                    {
                        placeBackcardHandGauche(NbrsCard, i);
                        
                    }
                    else if (j == 1)
                    {
                        playerIndex1 = i;
                    }
                    else if (j == 2)
                    {
                        playerIndex2 = i;
                    }
                    else
                    {
                        placeBackcardHandDroite(NbrsCard,i);
                    }
                    j++;
                }
            }
            placeBackcardDualUp(NbrsCard, playerIndex1, playerIndex2) ;
        }
        else if (nbrsPlayer == 6)
        {
            int j = 0;
            int playerIndex1 = -1;
            int playerIndex2 = -1;
            int playerIndex3 = -1;
            for (int i = 0; i < nbrsPlayer; i++)
            {
                if (ListPlayer[i].GetName() != currentPlayer)
                {
                    if (j == 0)
                    {
                        placeBackcardHandGauche(NbrsCard, i);

                    }
                    else if (j == 1)
                    {
                        playerIndex1 = i;
                    }
                    else if (j == 2)
                    {
                        playerIndex2 = i;
                    }
                    else if (j == 3)
                    {
                        playerIndex3 = i;
                    }
                    else
                    {
                        placeBackcardHandDroite(NbrsCard, i);
                    }
                    j++;
                }
            }
            placeBackcardTrioUp(NbrsCard, playerIndex1, playerIndex2, playerIndex3);
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
    private void placeBackcardHandUp(int NbrsCard,int playerINDEX)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(posFirstCard + i * 4, 24f, (float)i, 0f, playerINDEX) ;
        }
        
    }
    private void placeBackcardHandGauche(int NbrsCard,int playerINDEX)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(-48f, posFirstCard + i * 4, (float)i,90f,playerINDEX);

        }
    }
    private void placeBackcardHandDroite(int NbrsCard, int playerINDEX)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(48f, posFirstCard + i * 4, (float)i, 90f, playerINDEX);

        }
    }
    private void placeBackcardDualUp(int NbrsCard,int playerIndex1,int playerIndex2)
    {
        
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(-45 + i * 4, 24f, (float)i, 0f, playerIndex1);
            placeBackcard(45 - i * 4, 24f, (float)i, 0f, playerIndex2);

        }
    }

    private void placeBackcardTrioUp(int NbrsCard,int playerIndex1,int playerIndex2,int playerIndex3)
    {
        float posFirstCard = (float)(-NbrsCard * 2);
        for (int i = 0; i < NbrsCard; i++)
        {
            placeBackcard(posFirstCard + i * 4, 24f, (float)i, 0f, playerIndex1);
            placeBackcard(-48 + i * 4, 24f, (float)i, 0f, playerIndex2);
            placeBackcard(48 - i * 4, 24f, (float)i, 0f, playerIndex3);

        }
    }
    private void placeBackcard(float x, float y, float z, float angle, int playerINDEX)
    {
        GameObject backCard=Instantiate(cardsAssets[0]);
        backCard.transform.position = new Vector3(x, y, z);
        backCard.transform.Rotate(0f, 0f, angle, Space.Self);
        ListPlayer[playerINDEX].CardHide.Add(backCard);
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
