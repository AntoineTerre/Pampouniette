using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EndGame : MonoBehaviourPunCallbacks
{
    public void Reset()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Start");
        }
    }
}
