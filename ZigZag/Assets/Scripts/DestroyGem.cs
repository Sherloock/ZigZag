using System;
using UnityEngine;

public class DestroyGem : MonoBehaviour {
    private Player player;
    private void Start()
    {
        GameObject thePlayer = GameObject.Find("Player");
        player = thePlayer.GetComponent<Player>();
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.canmove)
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
            if (this != null)
            {
                try
                {
                    Destroy(this.transform.parent.gameObject, 2f);
                }
                catch (Exception e) { }
            }
        }
        
    }
}
      
