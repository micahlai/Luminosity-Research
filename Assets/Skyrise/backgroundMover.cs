using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMover : MonoBehaviour
{
    public Transform background;
    float target;

    public float smooth = 2;
    Vector3 startingPos;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;
        target = background.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        background.transform.position = new Vector3(1.27f, Mathf.Lerp(background.transform.position.y, target, Time.deltaTime * smooth), 10);
    }
    public void moveUp(float value)
    {
        if (target > -147)
        {
            target -= value;
        }
    }
}
