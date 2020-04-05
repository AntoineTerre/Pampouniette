using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EndGameControleur : MonoBehaviourPunCallbacks
{
    private PhotonView photonViewGame;

    private GameObject compteur;
    private float decompte =60;

    private GameObject End;
    private GameObject EndName;
    private GameObject EndShot;

    private float decompteReset = 30;

    // Start is called before the first frame update
    void Start()
    {
        photonViewGame = PhotonView.Get(this);
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
        if (PhotonNetwork.IsMasterClient)
        {
            EndPartyControleur();
        }
            
    }
    public void DecomptePause()
    {
        decompte = 10000f;
        photonViewGame.RPC("DecomptePauseOther", RpcTarget.Others);
    }
    [PunRPC]
    void DecomptePauseOther()
    {
        decompte = 10000f;
    }
    public void ResetDecompte()
    {
        decompte = decompteReset;
        photonViewGame.RPC("ResetDecompteOther", RpcTarget.Others);
    }
    [PunRPC]
    void ResetDecompteOther()
    {
        decompte = decompteReset-0.6f;
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
            EndGame();

        }
    }
    private void EndGame()
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
        End.SetActive(true);
        EndName.GetComponent<Text>().text = name;
        EndShot.GetComponent<Text>().text = "Tu bois " + nbrsShotInt + " shot !";
        photonViewGame.RPC("ActiveEndToOther", RpcTarget.Others);
    }
    [PunRPC]
    void ActiveEndToOther()
    {
        EndGame();
    }
    private void updateDecompte()
    {
        decompte -= Time.deltaTime;
    }
}
