using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class junction : MonoBehaviour
{
    public bool flipped;
    public junction alphaJunction;
    public junction betaJunction;
    public bool isEndpoint;
    [Space]

    public Vector2Int tilePosition;
    // Start is called before the first frame update
    void Start()
    {
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnMouseDown()
    {
        Debug.Log(provideNextJunction());
    }
    public junction provideNextJunction()
    {
        if (flipped)
        {
            return betaJunction;
        }
        else
        {
            return alphaJunction;
        }
    }

}
