using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField]Transform player;

    Vector3 offset;
	void Start () {
        offset = player.transform.position - this.transform.position;
	}

    void Update() {
        if (player != null && player.gameObject.GetComponent<Player>().canmove)
        { 
            Vector3 requiredPos = player.transform.position - offset;
            this.transform.position = Vector3.Lerp(this.transform.position, requiredPos, 1.5f);
        }
	}
}
