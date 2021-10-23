using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoveForwad : MonoBehaviour
{
    public float secondsToDestroy = 10;
    private int rndSpeed = 1;
    private float rndScale = 1;
    private Vector3 newScale;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
        rndSpeed = Random.Range(5, 7);
        rndScale = Random.Range(0.5f, 2.2f);
        newScale = new Vector3(1, 1, 1);

        gameObject.transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * rndSpeed);


    }


}
