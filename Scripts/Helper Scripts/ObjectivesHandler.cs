using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesHandler : MonoBehaviour
{
    private string obj1 = "Objectives: Collect three components ";
    private string obj2 = "and then find the extraction ! ";
    private string obj3 = "/3";

    private int counter;

    // Start is called before the first frame update
    void Awake()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text();
    }

    void Text()
    {
        counter = PlayerStats.numberOfCollectibles;
        this.GetComponent<Text>().text = obj1 + obj2 +"\n"+ counter + obj3;
        if(counter == 3)
        {
            this.GetComponent<Text>().color = new Color(0, 1, 0, 1);
        }
    }
}
