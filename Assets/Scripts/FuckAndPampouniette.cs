using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class FuckAndPampouniette
{
    private GameObject FuckUI;
    private GameObject PampounietteUI;
    private GameObject Pampouniette;
    private GameObject End;

    private GameObject PampText;
    private GameObject PampContinue;
    private GameObject namePlayPampGO;

    private GameObject Name1Button;
    private GameObject Name2Button;
    private GameObject Name3Button;
    private GameObject Name4Button;
    private GameObject Name5Button;
    private GameObject Name6Button;

    private string currentPlayer;

    private GameObject Name1ButtonPamp;
    private GameObject Name2ButtonPamp;
    private GameObject Name3ButtonPamp;
    private GameObject Name4ButtonPamp;
    private GameObject Name5ButtonPamp;
    private GameObject Name6ButtonPamp;

    private GameObject FuckName;

    private PhotonView photonViewGame;

    private List<GameObject> listButtonActive;
    private List<GameObject> listPampButtonActive;
    
    private List<Player> listPlayer;

    private GameObject Compteur;
    public FuckAndPampouniette(List<Player> ListPlayer, PhotonView photonViewGame,string currentPlayer)
    {
        Compteur = GameObject.Find("Compteur");
        this.currentPlayer = currentPlayer;
        this.photonViewGame = photonViewGame;
        FuckName = GameObject.Find("Pseudo");
        namePlayPampGO = GameObject.Find("namePlayPamp");
        this.listPlayer = ListPlayer;
        FindButtonAndText();
        ButtonName();
        PampContinue=GameObject.Find("PampContinue");
        PampContinue.GetComponent<Button>().onClick.AddListener(delegate () { this.PampContinuePress(); });
        FuckUI =GameObject.Find("UIFuck");
        FuckUI.SetActive(false);
        PampounietteUI=GameObject.Find("UIPampouniette");
        PampounietteUI.SetActive(false);
        PampText = GameObject.Find("NamePampou");
        Pampouniette = GameObject.Find("Pampouniette");
        Pampouniette.SetActive(false);
        End = GameObject.Find("Fin");
        
    }
    public void PampContinuePress()
    {
        string playName = namePlayPampGO.GetComponent<Text>().text;
        if (playName == currentPlayer)
        {
            Pampouniette.SetActive(false);
            photonViewGame.RPC("UnactivePamp", RpcTarget.Others);
            FuckPage();
        }
        
    }
    public void FindButtonAndText()
    {
        Name1Button = GameObject.Find("Name1Button");
        Name2Button = GameObject.Find("Name2Button");
        Name3Button = GameObject.Find("Name3Button");
        Name4Button = GameObject.Find("Name4Button");
        Name5Button = GameObject.Find("Name5Button");
        Name6Button = GameObject.Find("Name6Button");

        Name1ButtonPamp = GameObject.Find("Name1ButtonPamp");
        Name2ButtonPamp = GameObject.Find("Name2ButtonPamp");
        Name3ButtonPamp = GameObject.Find("Name3ButtonPamp");
        Name4ButtonPamp = GameObject.Find("Name4ButtonPamp");
        Name5ButtonPamp = GameObject.Find("Name5ButtonPamp");
        Name6ButtonPamp = GameObject.Find("Name6ButtonPamp");
    }
    public void ButtonName()
    {
        int nbrsPlayer = listPlayer.Count;
        listButtonActive = new List<GameObject>();
        listPampButtonActive = new List<GameObject>();
        if (nbrsPlayer == 2)
        {
            Name6Button.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listButtonActive.Add(Name6Button);
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listButtonActive.Add(Name1Button);
            Name2Button.SetActive(false);
            Name3Button.SetActive(false);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

            Name6ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listPampButtonActive.Add(Name6ButtonPamp);
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            Name2ButtonPamp.SetActive(false);
            Name3ButtonPamp.SetActive(false);
            Name4ButtonPamp.SetActive(false);
            Name5ButtonPamp.SetActive(false);



        }
        else if (nbrsPlayer == 3)
        {
            Name6Button.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listButtonActive.Add(Name6Button);
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listButtonActive.Add(Name1Button);
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            listButtonActive.Add(Name2Button);
            Name3Button.SetActive(false);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

            Name6ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listPampButtonActive.Add(Name6ButtonPamp);
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            Name2ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            listPampButtonActive.Add(Name2ButtonPamp);
            Name3ButtonPamp.SetActive(false);
            Name4ButtonPamp.SetActive(false);
            Name5ButtonPamp.SetActive(false);
        }
        else if (nbrsPlayer == 4)
        {
            Name6Button.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listButtonActive.Add(Name6Button);
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3Button.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            listButtonActive.Add(Name1Button);
            listButtonActive.Add(Name2Button);
            listButtonActive.Add(Name3Button);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

            Name6ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listPampButtonActive.Add(Name6ButtonPamp);
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            listPampButtonActive.Add(Name2ButtonPamp);
            listPampButtonActive.Add(Name3ButtonPamp);
            Name4ButtonPamp.SetActive(false);
            Name5ButtonPamp.SetActive(false);
        }
        else if (nbrsPlayer == 5)
        {
            Name6Button.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listButtonActive.Add(Name6Button);
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3Button.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            Name4Button.GetComponentsInChildren<Text>()[0].text = listPlayer[4].GetName();
            listButtonActive.Add(Name1Button);
            listButtonActive.Add(Name2Button);
            listButtonActive.Add(Name3Button);
            listButtonActive.Add(Name4Button);
            Name5Button.SetActive(false);


            Name6ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listPampButtonActive.Add(Name6ButtonPamp);
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            Name4ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[4].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            listPampButtonActive.Add(Name2ButtonPamp);
            listPampButtonActive.Add(Name3ButtonPamp);
            listPampButtonActive.Add(Name4ButtonPamp);
            Name5ButtonPamp.SetActive(false);
        }
        else
        {
            Name6Button.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listButtonActive.Add(Name6Button);
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3Button.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            Name4Button.GetComponentsInChildren<Text>()[0].text = listPlayer[4].GetName();
            Name5Button.GetComponentsInChildren<Text>()[0].text = listPlayer[5].GetName();
            listButtonActive.Add(Name1Button);
            listButtonActive.Add(Name2Button);
            listButtonActive.Add(Name3Button);
            listButtonActive.Add(Name4Button);
            listButtonActive.Add(Name5Button);

            Name6ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[0].GetName();
            listPampButtonActive.Add(Name6ButtonPamp);
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            Name4ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[4].GetName();
            Name5ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[5].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            listPampButtonActive.Add(Name2ButtonPamp);
            listPampButtonActive.Add(Name3ButtonPamp);
            listPampButtonActive.Add(Name4ButtonPamp);
            listPampButtonActive.Add(Name5ButtonPamp);
        }
        ActivateOnClickButton();
        ActivatePampOnClickButton();
    }

    public void ActivateOnClickButton()
    {
        foreach(GameObject GOBUT in listButtonActive)
        {
            Button b = GOBUT.GetComponent<Button>();
            b.onClick.AddListener(delegate () { this.ButtonClicked(b); });
        }
    }
    public void ActivatePampOnClickButton()
    {
        foreach (GameObject GOBUT in listPampButtonActive)
        {
            Button b = GOBUT.GetComponent<Button>();
            b.onClick.AddListener(delegate () { this.ButtonClickedPamp(b); });

        }
    }
    public void ButtonClickedPamp(Button b)
    {
        string name = b.GetComponentsInChildren<Text>()[0].text;
        PampounietteUI.SetActive(false);
        Pampouniette.SetActive(true);
        PampText.GetComponent<Text>().text = name;
        photonViewGame.RPC("OnPampSelect", RpcTarget.Others, name, currentPlayer);
    }


    public void ButtonClicked(Button b)
    {
        string name = b.GetComponentsInChildren<Text>()[0].text;
        FuckUI.SetActive(false);
        FuckName.GetComponent<TextMesh>().text = name;
        Debug.Log("FuckButton clicked");
        photonViewGame.RPC("ChangeFuckNameOther", RpcTarget.Others, name);
        Compteur.GetComponent<EndGameControleur>().ResetDecompte();
    }
    
    public void onFuckEvent(bool Pampouniette)
    {
        
        if (Pampouniette)
        {
            PampPage();
           
        }
        else
        {
            FuckPage();
        }

    }
    private void FuckPage()
    {
        photonViewGame.RPC("OnFuckOther", RpcTarget.Others, currentPlayer);
        FuckUI.SetActive(true);
    }
    private void PampPage()
    {
        photonViewGame.RPC("OnPampOther", RpcTarget.Others, currentPlayer);
        PampounietteUI.SetActive(true);
    }
}
