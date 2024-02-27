using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Sample1 : MonoBehaviour
{
    Scrollbar bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Scrollbar>();
        bar.numberOfSteps = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
