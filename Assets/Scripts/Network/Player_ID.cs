using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {
    [SyncVar] public string PlayerUniqueName;
    private NetworkInstanceId playerNetID;
    GameManagerNetworked gameManager;
    public override void OnStartLocalPlayer() {
        GetNetIdentity();
        SetIdentity();
    }

    // Use this for initialization
    void Awake () {
        gameManager = GameObject.Find("NetworkManager").GetComponent<GameManagerNetworked>();
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.name == "" || transform.name == "Player(Clone)") {
            SetIdentity();
        }
	}
    void SetIdentity() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerNetworked>();
        if (!isLocalPlayer) {
            transform.name = PlayerUniqueName;
        }
        else {
            transform.name = PlayerUniqueName;
        }
    }
    public void GetNetIdentity() {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdenity(MakeUniqueIdentity());
    }
    string MakeUniqueIdentity() {
        string UniqueIdentity = "Player " + playerNetID.ToString();
        return UniqueIdentity;
    }
    [Command]
    public void CmdTellServerMyIdenity(string name) {
        PlayerUniqueName = name;
        //transform.name = name;
    }
    [Command]
    public void CmdAddGameObject(GameObject player) {
        //var gameManager = GameObject.Find("NetworkManager").GetComponent<GameManagerNetworked>();
        //gameManager.RpcChangePlayer();
    }
}
