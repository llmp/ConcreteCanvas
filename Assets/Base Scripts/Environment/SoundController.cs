using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    // Use this for initialization
    
    [SerializeField] private AudioClip sprayClip;
    private AudioSource aSource;

    void Start () {
        aSource = gameObject.GetComponent<AudioSource>();
        aSource.clip = sound1;
        aSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeAudio()
    {
        aSource.clip = sound2;
        aSource.Play();
    }
}
