using UnityEngine;

public class GemSpin : MonoBehaviour
{
    public float speed;
    public float from = 0.0f;
    public float to = 0.000001f;
    void Start()
    {
        speed = 10000f;
    }

    void Update()
    {
        transform.Rotate(new Vector3(Random.Range(from, to), Random.Range(from, to), Random.Range(from, to)), speed * Time.deltaTime);
    }


}
