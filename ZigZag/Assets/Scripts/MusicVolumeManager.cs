using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeManager: MonoBehaviour
{
    public Slider volume;

    void Start()
    {
        // read
        volume.value = PlayerPrefs.GetFloat("Volume", 0);
    }

    void Update()
    {
        //set
        BackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().volume = volume.value;

        //save
        PlayerPrefs.SetFloat("Volume", BackgroundMusic.Instance.gameObject.GetComponent<AudioSource>().volume);
    }
}
