using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private GameObject UIStart1;
    private GameObject UIStart2;

    private GameObject Empty1;
    private GameObject Empty2;
    private GameObject Empty3;
    private GameObject Empty4;
    private GameObject Empty5;
    private GameObject Empty6;

    private List<GameObject> Empty;

    private GameObject ButtonStart1;
    private GameObject ButtonStart2;


    private int nbrsJoueur = 0;
    private List<string> names;

    // Start is called before the first frame update
    void Start()
    {
        UIStart1 = GameObject.Find("Start");
        UIStart2 = GameObject.Find("WaitMatchMaking");
        names = new List<string>();
        findAllText();
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

        ButtonStart1 = GameObject.Find("ButtonStart1");
        ButtonStart2 = GameObject.Find("ButtonStart2");
        //ButtonStart2.

    }

    private void ChangeName(int i,string name)
    {
        Empty[i].GetComponent<Text>().text = name;
    }

    private void onConnected(string name)
    {
        ChangeName(nbrsJoueur, name);
        names.Add(name);
        nbrsJoueur++;
    }

}
