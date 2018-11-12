using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworked : NetworkBehaviour {
    public GameManagerNetworked gameManager;
    [SyncVar] public int playerNumber = 0;
    [SyncVar] public bool doIhaveANumber = false;
    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerNetworked>();
    }
    private void Update() {
        if(isServer && gameManager.currentPlayer != playerNumber) {
            transform.position = new Vector3(8, 0, 0);
        }
        if(isServer && !doIhaveANumber) {
            var gM = GameObject.Find("GameManager").GetComponent<GameManagerNetworked>();
            gM.addNumberOfPlayers();
            playerNumber = gM.currentConnectedPlayers;
            doIhaveANumber = true;
        }
        if (!isLocalPlayer) {
            return;
        }
        //if (Input.GetKeyDown("x")) {
        //    gameManager.ChangeAllowedPlayer(playerNumber);
        //}
        if (Input.GetMouseButtonDown(0) && playerNumber == gameManager.GetAllowedPlayer()) {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision) {
    //    Debug.Log("Something hit me");
    //    if (collision.gameObject.GetComponent<SpaceScriptNetworked>()) {
    //        Hit(collision.gameObject);
    //    }
    //}
    //public void Hit(GameObject collision) {
    //    collision.gameObject.GetComponent<SpaceScriptNetworked>().MakeChange();
    //}
    [Command]
    public void CmdSendTheShit(int BoardNumber, int UnitNumber, int PlayerNumber) {
        gameManager.BoardManager.AddMove(BoardNumber, UnitNumber, PlayerNumber);
    }
    public void ResetPlayerPos() {
        transform.position = new Vector3(8, 0, 0);
    }
}
