using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform boardO;
    // Start is called before the first frame update
    void Start()
    {
        for (float y = -3.5f; y < 4.5f; y++)
        {
            for (float x = -3.5f; x < 4.5f; x++)
            {
                Instantiate(boardO, new Vector2(x, y), boardO.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
