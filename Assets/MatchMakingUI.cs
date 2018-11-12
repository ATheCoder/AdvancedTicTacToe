using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchMakingUI : MonoBehaviour {
    public NetworkManager networkManager;
	// Use this for initialization
	void Start () {
		
	}
    public void OnClick() {
        networkManager.StartMatchMaker();
        //networkManager.matchMaker.CreateMatch();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
