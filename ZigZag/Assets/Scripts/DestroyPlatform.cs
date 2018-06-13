using UnityEngine;

public class DestroyPlatform : MonoBehaviour {
    private Player player;
    private void Start()
    {
        GameObject thePlayer = GameObject.Find("Player");
        player = thePlayer.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && player.canmove)
        {
            Invoke("FallDown", 0.1f);
        }
    }

    private void FallDown()
    {
        if (player.canmove)
        {
            //fall
            this.GetComponentInParent<Rigidbody>().isKinematic = false;

            //desroy
            Destroy(this.transform.parent.gameObject, 2f);

            //create new
            GameObject theGameManager = GameObject.Find("GameManager");
            GameManager gameManager = theGameManager.GetComponent<GameManager>();
            gameManager.SpawnPlatform();
        }
    }
}
