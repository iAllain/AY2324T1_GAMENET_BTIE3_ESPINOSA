using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class BattleRoyaleController : MonoBehaviourPunCallbacks
{
    public List<GameObject> playerCars = new List<GameObject>();

    public enum RaiseEventsCode
    {
        WhoDiedEventCode
    }
    private int finishOrder = 3;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    void Start()
    {
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("rc"))
        {
            foreach (GameObject go in DeathRaceManager.instance.playerCars)
            {
                playerCars.Add(go);
            }  
        }
    }

    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventsCode.WhoDiedEventCode)
        {
            object[] data = (object[]) photonEvent.CustomData;

            string nicknameOfFinishedPlayer = (string)data[0];
            finishOrder = (int)data[1];
            int viewId = (int)data[2];

            Debug.Log(nicknameOfFinishedPlayer + " " + finishOrder);

            GameObject orderTMPro = DeathRaceManager.instance.finisherTextTMPro[finishOrder];
            orderTMPro.SetActive(true);

            if (viewId == photonView.ViewID)
            {
                orderTMPro.GetComponent<TextMeshProUGUI>().text = finishOrder + " " + nicknameOfFinishedPlayer;
                orderTMPro.GetComponent<TextMeshProUGUI>().color = Color.white;

            }
            else
            {
                orderTMPro.GetComponent<TextMeshProUGUI>().text = finishOrder + " " + nicknameOfFinishedPlayer + "(YOU)";
                orderTMPro.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
    }

    public void OnPlayerDeath()
    {
        GetComponent<PlayerSetup>().camera.transform.parent = null;
        GetComponent<VehicleMovement>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;

        finishOrder--;

        string nickname = photonView.Owner.NickName;
        int viewId = photonView.ViewID;

        object[] data = new object[] {nickname, finishOrder, viewId};

        RaiseEventOptions raisedEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };

        SendOptions sendOption = new SendOptions
        {
            Reliability = false
        };

        PhotonNetwork.RaiseEvent((byte) RaiseEventsCode.WhoDiedEventCode, data, raisedEventOptions, sendOption);
    }
}
