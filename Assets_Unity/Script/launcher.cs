using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class launcher : MonoBehaviourPunCallbacks {
  public static launcher Instance;
  
  public GameObject title;
  public GameObject createroom;
  public GameObject joinroom;
  public GameObject loading;

public GameObject room;



  [SerializeField] TMP_Text titleWelcomeText;
  [SerializeField] TMP_InputField roomNameInputField;
  [SerializeField] Transform roomListContent;
  [SerializeField] GameObject roomListItemPrefab;
  [SerializeField] TMP_Text roomNameText;
  [SerializeField] Transform playerListContent;
  [SerializeField] GameObject playerListItemPrefab;
  [SerializeField] GameObject startGameButton;
  [SerializeField] TMP_Text errorText;

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    Activatemenu(loading.name);
    Debug.Log("Connecting to master...");
    PhotonNetwork.ConnectUsingSettings();
    
  }

  public override void OnConnectedToMaster() {
    Debug.Log("Connected to master!");
    PhotonNetwork.JoinLobby();
    Activatemenu(title.name);
    // Automatically load scene for all clients when the host loads a scene
    PhotonNetwork.AutomaticallySyncScene = true;
  }

  public override void OnJoinedLobby() {
    if (PhotonNetwork.NickName == "") {
      PhotonNetwork.NickName = PlayerPrefs.GetString("Account"); // Set a default nickname, just as a backup
    } else {
      Debug.Log("Error");
    }
    Debug.Log("Joined lobby");
  }

   public void SetName() {
    string name = PlayerPrefs.GetString("Account");
    if (!string.IsNullOrEmpty(name)) {
      PhotonNetwork.NickName = name;
      titleWelcomeText.text = $"Welcome, {name}!";
      Activatemenu(title.name);
    } else {
      Debug.Log("No player name entered");
      // TODO: Display an error to the user
    }
  }
  public void changetocreateroom(){
    Activatemenu(createroom.name);
  }
  public void changetojoinroom(){
    Activatemenu(joinroom.name);
  }
  public void changetotitlemenu(){
    Activatemenu(title.name);
  }
  public void startgame(){
    PhotonNetwork.LoadLevel(2);
  }
  public void CreateRoom() {
    if (!string.IsNullOrEmpty(roomNameInputField.text)) {
      PhotonNetwork.CreateRoom(roomNameInputField.text);
      Activatemenu(loading.name);
      roomNameInputField.text = "";
    } else {
      Debug.Log("No room name entered");
      // TODO: Display an error to the user
    }
  }

  public override void OnJoinedRoom() {
    // Called whenever you create or join a room
    Activatemenu(room.name);
    PhotonNetwork.NickName =  PlayerPrefs.GetString("Account");
    roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    Player[] players = PhotonNetwork.PlayerList;
    foreach (Transform trans in playerListContent) {
      Destroy(trans.gameObject);
    }
    for (int i = 0; i < players.Count(); i++) {
      Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
    }
    // Only enable the start button if the player is the host of the room
    startGameButton.SetActive(PhotonNetwork.IsMasterClient);
  }

  public override void OnMasterClientSwitched(Player newMasterClient) {
    startGameButton.SetActive(PhotonNetwork.IsMasterClient);
  }

  public void LeaveRoom() {
    PhotonNetwork.LeaveRoom();
    Activatemenu(loading.name);
  }

  public void JoinRoom(RoomInfo info) {
    PhotonNetwork.JoinRoom(info.Name);
    Activatemenu(loading.name);
    
  }

  public override void OnLeftRoom() {
    Activatemenu(title.name);
  }

  public override void OnRoomListUpdate(List<RoomInfo> roomList) {
    foreach (Transform trans in roomListContent) {
      Destroy(trans.gameObject);
    }
    for (int i = 0; i < roomList.Count; i++) {
      if (roomList[i].RemovedFromList) {
        // Don't instantiate stale rooms
        continue;
      }
      Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
    }
  }

  public override void OnCreateRoomFailed(short returnCode, string message) {
    errorText.text = "Room Creation Failed: " + message;
     Activatemenu(title.name);
  }

  public override void OnPlayerEnteredRoom(Player newPlayer) {
    Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
  }

  public void StartGame() {
    // 1 is used as the build index of the game scene, defined in the build settings
    // Use this instead of scene management so that *everyone* in the lobby goes into this scene
    PhotonNetwork.LoadLevel(2);
  }

  public void QuitGame() {
    Application.Quit();
  }

  
  public void Activatemenu(string name){
    title.SetActive(name.Equals(title.name));
    joinroom.SetActive(name.Equals(joinroom.name));
    createroom.SetActive(name.Equals(createroom.name));
    loading.SetActive(name.Equals(loading.name));
    room.SetActive(name.Equals(room.name));
    Debug.Log("title"+name.Equals(title.name));
  }
}