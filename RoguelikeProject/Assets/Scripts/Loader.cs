using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    public GameObject player;

    void Awake() {
        if(GameManager.Instance==null)
            GameObject.Instantiate(gameManager);
    }

    private void Update()
    {
        FixCameraPos();
    }

    void FixCameraPos()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

}
