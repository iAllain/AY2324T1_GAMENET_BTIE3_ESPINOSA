using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        this.camera = transform.Find("Camera").GetComponent<Camera>();
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("rc"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<LapController>().enabled = photonView.IsMine;
            camera.enabled = photonView.IsMine;
        }
        else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("dr"))
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
