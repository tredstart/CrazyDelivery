using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector2 _position;
    private Vector2 _minPosition;
    private Vector2 _maxPosition;
    public float distance = 5;

    private SpriteRenderer _sprite;

    public List<Sprite> playerSprites;

    public CapsuleCollider2D shield;

    private Vector3 _shieldStartingPosition; 
    private Vector3 _shieldNewPosition;

    private static readonly int Dance = Animator.StringToHash("Dance");

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        _minPosition = _position - new Vector2(distance, 0);
        _maxPosition = _position + new Vector2(distance, 0);
        _sprite = GetComponent<SpriteRenderer>();
        _shieldStartingPosition = shield.transform.position;
        _shieldNewPosition = _shieldStartingPosition;
        _shieldNewPosition += new Vector3(0, 0.05f, 0);
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _position.y = transform.position.y;
        _shieldStartingPosition.x = shield.transform.position.x;
        Move();
        ChangeDirection();
    }

    private void Move()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var newPosition = _position.x + distance * horizontalMovement;
        switch (horizontalMovement)
        {
            case < 0:
            {
                
                transform.eulerAngles = new Vector3(0, 180, 0);
                var x = Mathf.Min(_minPosition.x, newPosition);
                transform.position = new Vector3(x, _position.y, 0);
                break;
            }
            case > 0:
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                var x = Mathf.Max(_maxPosition.x, newPosition);
                transform.position = new Vector3(x, _position.y, 0f);
                break;
            }
        }
    }

    private void ChangeDirection()
    {
        var verticalMovement = Input.GetAxis("Vertical");
        var yRotation = shield.transform.eulerAngles.y;
        switch (verticalMovement)
        {
            case < 0:
                shield.transform.eulerAngles = new Vector3(0, yRotation, 0);
                _sprite.sprite = playerSprites[0];
                shield.transform.position = new Vector3(transform.position.x, _shieldStartingPosition.y, 0f);
                break;
            case > 0:
                shield.transform.eulerAngles = new Vector3(0, yRotation, 32);
                _sprite.sprite = playerSprites[1];
                shield.transform.position = new Vector3(transform.position.x, _shieldNewPosition.y, 0f);
                break;
        }
    }

    public void PlayAnimation()
    {
        _audio.Play();
        StartCoroutine(PlayAnimationCoroutine());
    }

    private IEnumerator PlayAnimationCoroutine()
    {
        var animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("HappyDance", -1, 0f);
        for (int i = 0; i < 140; i++)
        {
            yield return null;
        }
        animator.SetBool(Dance, false);
        animator.enabled = false;
        Time.timeScale = 1;
    }
}
