using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class number : MonoBehaviour
{
    public TextMeshPro text;
    public int numberAssigned;
    public int orderAssigned;
    float targetAlpha = 0;
    float alpha = 0;
    public float smooth = 4f;
    public bool disabled;
    public Animator anim;
    public bool destroy;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        text.text = numberAssigned.ToString();
        //updateAlpha(0);
        targetAlpha = 1;
        alpha = 0;
        smooth = smooth * Random.Range(0.5f, 1.5f);
        disabled = false;
        destroy = false;
    }
    private void Update()
    {
        //updateAlpha(alpha);
        //alpha = Mathf.Lerp(alpha, targetAlpha, Time.deltaTime * smooth);
        if (destroy)
        {
            destroyGO();
        }
    }
    public void hideNumbers()
    {
        
    }
    public void showNumbers()
    {
    }
    private void OnMouseDown()
    {
        if (!disabled)
        {
            FindAnyObjectByType<numberSpawner>().numberClicked(this);
        }
    }
    void updateAlpha(float a)
    {
        Color c = text.color;
        text.color = new Color(c.r, c.g, c.b, a);
    }
    public void destroyGO()
    {
        Destroy(gameObject);
        FindAnyObjectByType<numberSpawner>().numberDestroyed();
    }
    public void playFoldSFX()
    {
        audioSource1.Play();
    }
    public void playNoteSFX()
    {
        int note = orderAssigned + 1;
        int[] noteSteps = { 0, 2, 4, 5, 7, 9, 11, 12};
        audioSource2.pitch = Mathf.Pow(1.05946f, noteSteps[note]);
        audioSource2.Play();
    }

}
