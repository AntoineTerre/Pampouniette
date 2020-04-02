using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuckAndPampouniette
{
    private GameObject FuckUI;

    private GameObject Name1Button;
    private GameObject Name2Button;
    private GameObject Name3Button;
    private GameObject Name4Button;
    private GameObject Name5Button;

    private GameObject FuckName;

    private List<GameObject> listButtonActive;
    
    private List<Player> listPlayer;

    public FuckAndPampouniette(List<Player> ListPlayer,string currentPlayer)
    {
        FuckName = GameObject.Find("Pseudo");
        this.listPlayer = ListPlayer;
        FindButtonAndText();
        ButtonName(currentPlayer);
        FuckUI =GameObject.Find("UIFuck");
        FuckUI.SetActive(false);
    }
    public void FindButtonAndText()
    {
        Name1Button = GameObject.Find("Name1Button");
        Name2Button = GameObject.Find("Name2Button");
        Name3Button = GameObject.Find("Name3Button");
        Name4Button = GameObject.Find("Name4Button");
        Name5Button = GameObject.Find("Name5Button");
    }
    public void ButtonName(string currentPlayer)
    {
        int nbrsPlayer = listPlayer.Count;
        listButtonActive = new List<GameObject>();
        if (nbrsPlayer == 2)
        {
            Name1Button.GetComponentsInChildren<Text>()[0].text = listPlayer[1].GetName();
            listButtonActive.Add(Name1Button);
            Name2Button.SetActive(false);
            Name3Button.SetActive(false);
            Name4Button.SetActive(false);
            Name5Button.SetActive(false);

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
        }
        ActivateOnClickButton();
    }

    public void ActivateOnClickButton()
    {
        foreach(GameObject GOBUT in listButtonActive)
        {
            Button b = GOBUT.GetComponent<Button>();
            b.onClick.AddListener(delegate () { this.ButtonClicked(b); });
        }
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

        }
        else
        {
            FuckPage();
        }

    }
    private void FuckPage()
    {
        FuckUI.SetActive(true);
    }
}
