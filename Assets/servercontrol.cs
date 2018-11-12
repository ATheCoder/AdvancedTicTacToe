using UnityEngine;
using UnityEngine.Networking;

public class servercontrol : NetworkBehaviour {
    void Update() {
        //if (!isServer) {
        //    return;
        //}
        //else if (Input.GetMouseButtonDown(0)) {
        //    RpcDisableRenderer();

        //}
    }
    [ClientRpc]
    public void RpcDisableRenderer() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    [Command]
    public void CmdDisableRendrer() {
        //GetComponent<SpriteRenderer>().enabled = false;
        RpcDisableRenderer();
    }
}