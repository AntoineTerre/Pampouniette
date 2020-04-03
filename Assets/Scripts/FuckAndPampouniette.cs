using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuckAndPampouniette
{
    private GameObject FuckUI;
    private GameObject PampounietteUI;
    private GameObject Pampouniette;
    private GameObject End;

    private GameObject PampText;

    private GameObject Name1Button;
    private GameObject Name2Button;
    private GameObject Name3Button;
    private GameObject Name4Button;
    private GameObject Name5Button;


    private GameObject Name1ButtonPamp;
    private GameObject Name2ButtonPamp;
    private GameObject Name3ButtonPamp;
    private GameObject Name4ButtonPamp;
    private GameObject Name5ButtonPamp;

    private GameObject FuckName;

    private List<GameObject> listButtonActive;
    private List<GameObject> listPampButtonActive;
    
    private List<Player> listPlayer;

    public FuckAndPampouniette(List<Player> ListPlayer,string currentPlayer)
    {
        FuckName = GameObject.Find("Pseudo");
        this.listPlayer = ListPlayer;
        FindButtonAndText();
        ButtonName(currentPlayer);
        FuckUI =GameObject.Find("UIFuck");
        FuckUI.SetActive(false);
        PampounietteUI=GameObject.Find("UIPampouniette");
        PampounietteUI.SetActive(false);
        PampText = GameObject.Find("NamePampou");
        Pampouniette = GameObject.Find("Pampouniette");
        Pampouniette.SetActive(false);
        End = GameObject.Find("Fin");
        
    }
    public void FindButtonAndText()
    {
        Name1Button = GameObject.Find("Name1Button");
        Name2Button = GameObject.Find("Name2Button");
        Name3Button = GameObject.Find("Name3Button");
        Name4Button = GameObject.Find("Name4Button");
        Name5Button = GameObject.Find("Name5Button");

        Name1ButtonPamp = GameObject.Find("Name1ButtonPamp");
        Name2ButtonPamp = GameObject.Find("Name2ButtonPamp");
        Name3ButtonPamp = GameObject.Find("Name3ButtonPamp");
        Name4ButtonPamp = GameObject.Find("Name4ButtonPamp");
        Name5ButtonPamp = GameObject.Find("Name5ButtonPamp");
    }
    public void ButtonName(string currentPlayer)
    {
        int nbrsPlayer = listPlayer.Count;
        listButtonActive = new List<GameObject>();
        listPampButtonActive = new List<GameObject>();
        if (nbrsPlayer == 2)
        {
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listButtonActive.Add(Name1Button);
            Name2Button.SetActive(false);
            Name3Button.SetActive(false);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

            Name1ButtonPamp.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listPampButtonActive.Add(Name1ButtonPamp);
            Name2ButtonPamp.SetActive(false);
            Name3ButtonPamp.SetActive(false);
            Name4ButtonPamp.SetActive(false);
            Name5ButtonPamp.SetActive(false);



        }
        else if (nbrsPlayer == 3)
        {
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listButtonActive.Add(Name1Button);
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            listButtonActive.Add(Name2Button);
            Name3Button.SetActive(false);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

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
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3Button.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            listButtonActive.Add(Name1Button);
            listButtonActive.Add(Name2Button);
            listButtonActive.Add(Name3Button);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);


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
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            Name2Button.GetComponentsInChildren<Text>()[0].text = listPlayer[2].GetName();
            Name3Button.GetComponentsInChildren<Text>()[0].text = listPlayer[3].GetName();
            Name4Button.GetComponentsInChildren<Text>()[0].text = listPlayer[4].GetName();
            listButtonActive.Add(Name1Button);
            listButtonActive.Add(Name2Button);
            listButtonActive.Add(Name3Button);
            listButtonActive.Add(Name4Button);
            Name5Button.SetActive(false);

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
       //ICI IL FAUT WAIT 
        Pampouniette.SetActive(false);
        FuckPage();

    }

    public void ButtonClicked(Button b)
    {
        string name = b.GetComponentsInChildren<Text>()[0].text;
        FuckUI.SetActive(false);
        Time.timeScale = 1;
        FuckName.GetComponent<TextMesh>().text = name;
    }

    public void onFuckEvent(bool Pampouniette)
    {
        
        Time.timeScale = 0;
        if (Pampouniette)
        {
            PampPage();
        }
        else
        {
            //PampPage();
            FuckPage();
        }

    }
    private void FuckPage()
    {
        FuckUI.SetActive(true);
    }
    private void PampPage()
    {
        PampounietteUI.SetActive(true);
    }
}
