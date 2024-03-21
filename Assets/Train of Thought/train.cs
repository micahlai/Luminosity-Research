using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train : MonoBehaviour
{
    public junction Destination;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(
    Destination.transform.position - transform.position,
    transform.TransformDirection(Vector3.up)
);
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        transform.position += speed * Time.deltaTime * Vector3.up;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        junction j = collision.gameObject.GetComponent<junction>();
        if (j == Destination)
        {
            Destination = j.provideNextJunction();
        }
    }
}
