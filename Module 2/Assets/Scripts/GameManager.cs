using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomX = Random.Range(-10, 10);
            int randomZ = Random.Range(-10, 10);
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomX, 0, randomZ), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
