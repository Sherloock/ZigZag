using UnityEngine;

public class Player : MonoBehaviour {
    public Rigidbody rb;
    private bool movingRight;
    public bool canmove;

    public delegate void PlayerDied(int score);
    public static event PlayerDied playerDied;


    private GameManager gameManager;

    void Start () {
        movingRight = true;
        canmove = true;
        GameObject theGameManager = GameObject.Find("GameManager");
        gameManager = theGameManager.GetComponent<GameManager>();
        rb = this.GetComponent<Rigidbody>();
    }
	
	void Update () {
        //ctrl  Input.GetMouseButtonDown(0)
        if (Input.GetKeyDown("space") && canmove)
        {
            movingRight = !movingRight;
            float speed = gameManager.GetSpeed();
            if (movingRight)
            {
                rb.velocity = new Vector3(speed, 0f, 0f);
            }
            else
            {
                rb.velocity = new Vector3(0f, 0f, speed);
            }
 
        }

        //float rotationSpeed = 100f;
        //if (movingRight)
        //{
        //    transform.Rotate(new Vector3(1f, 0, 0), rotationSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);
        //}




        //falldown
        if (!Physics.Raycast(this.transform.position, Vector3.down*2)){
            canmove = false;
            rb.velocity = new Vector3(0f, -8f, 0f);

            //killin player
            if (playerDied != null)
            {
                playerDied(gameManager.GetScore());
            }
            Destroy(gameObject, 2f);
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gem")
        {
            gameManager.GemPickup(other);

        }

        if (other.gameObject.tag == "Path" && canmove)
        {
            gameManager.IncreaseScoreBy(1);
        }
    }   
}
