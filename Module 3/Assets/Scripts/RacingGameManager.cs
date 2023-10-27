using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RacingGameManager : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;
    public Transform[] startingPositions;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Constants.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            {
                Debug.Log((int)playerSelectionNumber);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

                Vector3 instantiatePosition = startingPositions[actorNumber-1].position;
                PhotonNetwork.Instantiate(vehiclePrefabs[(int)playerSelectionNumber].name, instantiatePosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
