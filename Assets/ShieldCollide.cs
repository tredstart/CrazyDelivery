using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollide : MonoBehaviour
{
    private Score _score;

    private AudioSource _audio;
    public GameObject scoreTable;
    // Start is called before the first frame update

    private void Start()
    {
        _score = scoreTable.GetComponent<Score>();
        _audio = GetComponent<AudioSource>();
        Debug.Log(_score);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _audio.Play();
        _score.S += 5;
        Destroy(col.gameObject);
    }
}
