using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StartManager : MonoBehaviourPunCallbacks
{
    private GameObject UIStart1;
    private GameObject UIStart2;

    public GameObject InputField;

    private GameObject Empty1;
    private GameObject Empty2;
    private GameObject Empty3;
    private GameObject Empty4;
    private GameObject Empty5;
    private GameObject Empty6;

    private List<GameObject> Empty;

    private GameObject ButtonStart1;
    private GameObject ButtonStart2;


    private string _name;
    const string playerNamePrefKey = "PlayerName";


    private int RoomId;
    // Start is called before the first frame update
    void Start()
    {
        setDefaultName();
        ButtonStart1 = GameObject.Find("ButtonStart1");
        ButtonStart1.SetActive(false);
        ButtonStart2 = GameObject.Find("ButtonStart2");
        //Photon connection serveur
        PhotonNetwork.ConnectUsingSettings();

        UIStart1 = GameObject.Find("Start");
        UIStart2 = GameObject.Find("WaitMatchMaking");
        findAllText();
    }
    private void setDefaultName()
    {
        string defaultName = string.Empty;
        InputField _inputField = InputField.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }


        PhotonNetwork.NickName = defaultName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void findAllText()
    {
        Empty = new List<GameObject>();
        Empty1 = GameObject.Find("Empty1");
        Empty2 = GameObject.Find("Empty2");
        Empty3 = GameObject.Find("Empty3");
        Empty4 = GameObject.Find("Empty4");
        Empty5 = GameObject.Find("Empty5");
        Empty6 = GameObject.Find("Empty6");
        Empty.Add(Empty1);
        Empty.Add(Empty2);
        Empty.Add(Empty3);
        Empty.Add(Empty4);
        Empty.Add(Empty5);
        Empty.Add(Empty6);

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " serveurs !");
        PhotonNetwork.AutomaticallySyncScene = true;
        ButtonStart1.SetActive(true);
    }
    public void QuickStart()
    {
        _name=InputField.GetComponent<InputField>().text.ToString();
        if(name==null || name == "")
        {
            Debug.Log("Name Vide");
        }
        else
        {
            //on ajoute le pseudo du joueur
            PhotonNetwork.NickName = _name;
            //on se souvien du pseudo du joueur
            PlayerPrefs.SetString(playerNamePrefKey, _name);
            //make sure no double click
            ButtonStart1.SetActive(false);
            //Try to join ramdom room
            PhotonNetwork.JoinRandomRoom();
            //disable the starting UI while player connected
            UIStart1.SetActive(false);
        }
        
    }

    public void RealStart()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        else
        {
            if (Photon.Pun.PhotonNetwork.CurrentRoom.Players.Count < 2)
            {
                Debug.Log("Not Enouth Player");
            }
            else
            {
                PhotonNetwork.LoadLevel("GameScene");
            }
            

        }
        
    }
    
    //sappel quand un joueur ELOIGNER !! rejoin la room
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        changeName();
        Debug.Log("Name Changed: A player is Entered");
    }
    //sappel quand un joueur ELOIGNER !! quit la room
    public override void OnPlayerLeftRoom(Photon.Realtime.Player oldPlayer)
    {
        changeName();
        Debug.Log("Name Changed: A player has left");
    }
   
    //active when a peaple join or left
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() call");
        changeName();
    }
    
    private void changeName()
    {
        Dictionary<int, Photon.Realtime.Player> pList = Photon.Pun.PhotonNetwork.CurrentRoom.Players;
        int i = 1;
        foreach (KeyValuePair<int, Photon.Realtime.Player> p in pList)
        {
            if (p.Value.IsMasterClient)
            {
                Empty[0].GetComponent<Text>().text = p.Value.NickName;
            }
            else
            {
                Empty[i].GetComponent<Text>().text = p.Value.NickName;
                i++;
            }
           

        }
    }

    //active si il n'y a pas de master a qui se connecter
    public override void OnJoinRandomFailed(short returnCode,string message)
    {
        CreateRoom();
    }
    void CreateRoom()
    {
        Debug.Log("Create new room");
        RoomId = Random.Range(0, 10000);

        RoomOptions rOp = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)6 };
        
        PhotonNetwork.CreateRoom("Room" + RoomId, rOp);
    }
    public override void OnCreateRoomFailed(short returnCode,string message)
    {
        Debug.Log("Creation Room Failed Try AGAIN !");
        CreateRoom();
    }
}
