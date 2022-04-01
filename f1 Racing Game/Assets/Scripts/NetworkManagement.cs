using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagement : Photon.PunBehaviour
{
    public GameObject lobbyCam;
    public Transform spawnPoint;
    public Text StatusText;

    public GameObject lobbyUI;

    public const string Version = "1.0";
    public const string RoomName = "Multiplayer";
    public string playerPrefabName = "CAR";



    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(Version);

    }

    private void Update()
    {
        StatusText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }


    public override void OnConnectionFail(DisconnectCause cause)
    {
        print("Connection Failed: " + cause.ToString());
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
        base.OnConnectedToMaster();
    }


    public override void OnJoinedRoom()
    {
        lobbyCam.SetActive(false);
        lobbyUI.SetActive(false);
        Debug.Log("Joined");
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName,
        spawnPoint.position, spawnPoint.rotation, 0);
        base.OnJoinedRoom();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        print("New Player Connected");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        print("Player Disconnected");
    }
}
