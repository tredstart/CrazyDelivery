using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Score : MonoBehaviour
{
    private TextMeshPro _text;
    
    public int S { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = S.ToString() + "$";
    }
}
