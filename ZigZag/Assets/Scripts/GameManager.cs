using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject startPlatform;

    private int score;
    public TMP_Text scoreText;

    private float maxSpeed;
    private float speed;
    public TMP_Text speedText;

    private float minPathsize;
    private float pathSize;
    public TMP_Text sizeText;

    private float gemSpawnChanse;

    // prefabs
    [SerializeField] GameObject platform;
    [SerializeField] GameObject gem;
    [SerializeField] GameObject particle;

    private Color platformColor;
    private string colorChange; 

    Vector3 lastPlatformPos;

    private int scene;

    void Start () {
        platformColor = new Color(0f, 0.4825f, 0.9150f);
        colorChange = "gu";

        scene = SceneManager.GetActiveScene().buildIndex;

        score = 0;
        minPathsize = 1.3f;
        maxSpeed = 5f;

        SetSpeed(4f);
        SetPathsize(1.5f);
        gemSpawnChanse = 0.4f;

        UpdateCounters();

        float topright = 2.5f - pathSize / 2;
        lastPlatformPos = new Vector3(topright, 0f, topright);
        
        //create some
        for (int i = 0; i < 30; i++) { SpawnPlatform(); }
    }


public void SpawnPlatform()
    {
        Vector3 dir = new Vector3(0f, 0f, 0f);
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            dir.x = pathSize;
        }
        else
        {
            dir.z = pathSize;
        }

        //new platform
        GameObject _platform = Instantiate(platform) as GameObject;

        //set size
        Vector3 temp = _platform.transform.localScale;
        temp.x = temp.z = pathSize;
        _platform.transform.localScale = temp;

        //rainbow platform color
        ChangePlatformColor();
        _platform.GetComponent<Renderer>().material.color = platformColor;
 

        //setpos
        _platform.transform.position = lastPlatformPos + dir;
        lastPlatformPos = _platform.transform.position;

        //spawn gem
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (random < gemSpawnChanse)
        {
            Instantiate(gem, lastPlatformPos + new Vector3(0f, 1.2f, 0f), gem.transform.rotation);
        }
        
    }
    private void Update()
    {
        if (score == 1 && startPlatform != null)
        {
            Invoke("StartPlatformFalldown", 0.1f);
        }
    }

    private void StartPlatformFalldown()
    {
        startPlatform.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(startPlatform, 2f);
    }

    private void ChangePlatformColor()
    {
        float scale = 0.03f; ;
        switch (colorChange)
        {
            //green up
            case "gu":
                platformColor.g += scale;
                if(platformColor.g >= 0.9f)
                {
                    colorChange = "bd";
                }
                break;

            //blue down
            case "bd":
                platformColor.b -= scale;
                if (platformColor.b < 0.1f)
                {
                    colorChange = "ru";
                }
                break;

            //red up
            case "ru":
                platformColor.r += scale;
                if (platformColor.r >= 0.9f)
                {
                    colorChange = "gd";
                }
                break;
            //green down
            case "gd":
                platformColor.g -= scale;
                if (platformColor.g < 0.1f)
                {
                    colorChange = "bu";
                }
                break;

            //blue up
            case "bu":
                platformColor.b += scale;
                if (platformColor.b >= 0.9f)
                {
                    colorChange = "rd";
                }
                break;

            //red down
            case "rd":
                platformColor.r -= scale;
                if (platformColor.r < 0.1f)
                {
                    colorChange = "gu";
                }
                break;
        }
    }

    public void GemPickup(Collider gem)
    {
        GameObject _particle = Instantiate(particle) as GameObject;
        _particle.transform.position = gem.transform.position;

        Destroy(gem.gameObject);
        Destroy(_particle, 1f);

        IncreaseScoreBy(2);
    }

    
    public void IncreaseScoreBy(int num)
    {
        score += num;
        LevelUp(num);

        int count = 100;
         Color bg = Camera.main.GetComponent<Camera>().backgroundColor;
         //bg.r -= 0.0006f;
         bg.b -= (0.9f/count);
         bg.g -= (0.9f / count);

        
        if (bg.b <= 0f)
        {
            bg.r -= (0.6f / count);
        }
       

         StartCoroutine(ChangeBackgroundColor(bg, 0.3f));
    }

    IEnumerator ChangeBackgroundColor(Color c, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Camera.main.GetComponent<Camera>().backgroundColor = c;
    }

    private void LevelUp(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (UnityEngine.Random.Range(0, 6) != 1)
            {
                // increase speed
                speed += 0.01f;
            }
            else
            {
                // decrease pathsize;
                SetPathsize(pathSize -= 0.01f);
            }
            UpdateCounters();
        }
    }


    private void UpdateCounters()
    {
        scoreText.text = "" + score;
        speedText.text = "SPEED " + (int)(speed * 10);
        sizeText.text = "SIZE " + (int)(pathSize * 10);
    }

    private void SetPathsize(float pathSize)
    {
        this.pathSize = Mathf.Max(pathSize, minPathsize);
    }
    private void SetSpeed(float speed)
    {
        this.speed = Mathf.Min(speed, maxSpeed);
    }

    public int GetScore()
    {
        return score;
    }
    public float GetSpeed()
    {
        return speed;
    }
}
