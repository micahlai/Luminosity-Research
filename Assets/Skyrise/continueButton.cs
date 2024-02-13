using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueButton : MonoBehaviour
{
   public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        FindAnyObjectByType<numberSpawner>().continueBut();
        anim.SetTrigger("disappear");
        GetComponent<AudioSource>().Play();
    }
    private void OnMouseEnter()
    {
        anim.SetBool("hovered", true);
    }
    void OnMouseExit()
    {
        anim.SetBool("hovered", false);
    }
    

}
