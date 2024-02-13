using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMover : MonoBehaviour
{
    public Transform background;
    public float target;

    public float smooth = 2;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioClip citySFX;
    public AudioClip brownNoise;

    public AnimationCurve citySFXCurve;
    public AnimationCurve brownNoiseSFXCurve;
    public AnimationCurve brownNoisePitchCurve;


    Vector3 startingPos;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;
        target = background.transform.position.y;
    }
    private void Start()
    {
        audioSource1.clip = citySFX;
        audioSource1.Play();
        audioSource2.clip = brownNoise;
        audioSource2.Play();

        audioSource2.volume = 0;
        audioSource1.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        background.transform.position = new Vector3(1.27f, Mathf.Lerp(background.transform.position.y, target, Time.deltaTime * smooth), 10);

        audioSource1.volume = citySFXCurve.Evaluate(-background.transform.position.y / 120);
        audioSource2.volume = brownNoiseSFXCurve.Evaluate(-background.transform.position.y / 120);
        audioSource2.pitch = brownNoisePitchCurve.Evaluate(-background.transform.position.y / 120);

    }
    public void moveUp(float value)
    {
        if (target > -147)
        {
            target -= value;
        }
        audioSource3.Play();
    }
}
