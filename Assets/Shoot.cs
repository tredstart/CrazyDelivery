using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Shoot : MonoBehaviour
{
    public Sprite bullet;
    public List<Sprite> shootingSprites;
    private SpriteRenderer _sp;

    public float speed = 0.5f;
    private float _rechargeTime;
    private bool _recharging = true;
    private float _currentTime;
    public float rotation;
    public float PauseTime { get; set; } = 1f;
    private GameObject _newBullet;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _currentTime = Time.time;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_newBullet)
        {
            if (rotation == 0)
            {
                MoveBulletFlat();
            }
            else
            {
                MoveBulletAngle();
            }
            
        }
        else
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        var t = Time.time;
        if (_recharging)
        {
            if (t - _currentTime >= _rechargeTime)
            {
                _currentTime = t;
                _sp.sprite = shootingSprites[1];
                _recharging = false;
            }
        }
        else
        {
            if (t - _currentTime >= PauseTime)
            {
                _currentTime = t;
                _sp.sprite = shootingSprites[0];
                _recharging = true;
                _rechargeTime = Random.Range(0.1f, 4f);
                _newBullet = GenerateBullet();
            }
        }
    }

    private GameObject GenerateBullet()
    {
        _audioSource.Play();
        var b = new GameObject
        {
            transform =
            {
                eulerAngles = new Vector3(0, 0, rotation)
            }
        };
        var bulletRenderer = b.AddComponent<SpriteRenderer>();
        bulletRenderer.sprite = bullet;
        bulletRenderer.flipX = _sp.flipX;
        var body = b.AddComponent<Rigidbody2D>();
        body.gravityScale = 0;
        b.transform.position = transform.position;        
        b.AddComponent<BoxCollider2D>();
        return b; 
    }

    private void MoveBulletFlat()
    {
        var direction = _sp.flipX ? -1 : 1;
        _newBullet.transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0);
    }

    private void MoveBulletAngle()
    {
        var direction = _sp.flipX ? -1 : 1;

        var directionY = Mathf.Cos(Mathf.Abs(rotation))  * speed / Mathf.Sin(Mathf.Abs(rotation));        
        
        _newBullet.transform.position += new Vector3(speed * Time.deltaTime * direction, directionY * Time.deltaTime, 0);
    }
}
