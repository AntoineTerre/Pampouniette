using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameControleur : MonoBehaviour
{
    private GameObject compteur;
    private float decompte = 35;

    private GameObject End;
    private GameObject EndName;
    private GameObject EndShot;

    // Start is called before the first frame update
    void Start()
    {
        compteur = GameObject.Find("Compteur");
        End = GameObject.Find("Fin");
        EndName = GameObject.Find("NameFin");
        EndShot = GameObject.Find("TuBois");
        End.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateDecompte();
        updateCompteur();
        EndPartyControleur();
    }
    public void ResetDecompte()
    {
        decompte = 20.5f;
    }
    private void updateCompteur()
    {
        int timer = (int)decompte;
        compteur.GetComponent<TextMesh>().text = timer.ToString();
    }
    private void EndPartyControleur()
    {
        if (decompte <= 0)
        {
            GameObject Game = GameObject.Find("Scripts");
            GameObject FuckName = GameObject.Find("Pseudo");
            List<Card> PacketJeux = Game.GetComponent<Game>().GetPacketJeux();
            string name = FuckName.GetComponent<TextMesh>().text;
            float nbrsShot = PacketJeux.Count / 4;
            int nbrsShotInt = (int)nbrsShot;
            if (nbrsShotInt < 1)
            {
                nbrsShotInt = 1;
            }
            Time.timeScale = 0;
            End.SetActive(true);
            EndName.GetComponent<Text>().text = name;
            EndShot.GetComponent<Text>().text = "Tu bois " + nbrsShotInt + " shot !";

        }
    }
    private void updateDecompte()
    {
        decompte -= Time.deltaTime;
    }
}
