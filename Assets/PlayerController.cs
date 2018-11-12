using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public GameObject gameob;
    private void Start() {
        gameob = GameObject.Find("GameObject");
    }
    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            CmdDisableRenderer();

        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
    [Command]
    public void CmdDisableRenderer() {
        gameob = GameObject.Find("GameObject");
        gameob.GetComponent<servercontrol>().RpcDisableRenderer();
        //gameob.GetComponent<SpriteRenderer>().enabled = false;
    }
}