using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Timer : MonoBehaviour
{
    public uint Seconds { get; set; } = 10;

    public float pause = 1.5f;
    public TextMeshPro clockText;
    public GameObject button;
    public bool IsCounting { get; set; } = true;

    private float _currentTime;

    public List<Sprite> modes;

    public SpriteRenderer Renderer { get; set; }

    private Score _score;
    public GameObject scoreTable;


    public List<GameObject> enemies;
    private List<Shoot> _enemiesData;

    public GameObject player;

    public GameObject panel;
    public GameObject losingText;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _currentTime = Time.time;
        Renderer = button.GetComponent<SpriteRenderer>();
        Renderer.sprite = modes[0];
        _score = scoreTable.GetComponent<Score>();
        _enemiesData = new List<Shoot>();
        foreach (var enemy in enemies)
        {
            _enemiesData.Add(enemy.GetComponent<Shoot>());
        }
        _text = losingText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Seconds > 30)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            _text.text += _score.S + "$";
            Seconds = 10;
        }
        if (Input.GetKeyUp("e"))
        {
            Debug.Log("pressed");
            if (IsCounting)
            {
                _score.S -= 100;
            }
            else
            {
                _score.S += 1000;
                Time.timeScale = 0;
                pause = Mathf.Max(0.4f, pause - 0.1f);
                player.SendMessage("PlayAnimation");
                foreach (var enemy in _enemiesData)
                {
                    enemy.speed += 0.1f;
                }
            }
        }
        clockText.text = Seconds.ToString();
        var t = Time.time;
        if (IsCounting)
        {
            if (!(t - _currentTime >= 1)) return;
            _currentTime = t;
            Seconds--;
            if (Seconds != 0) return;
            Renderer.sprite = modes[1];
            IsCounting = false;
        }
        else if (t - _currentTime >= pause)
        {
            Seconds = 10;
            _currentTime = t;
            Renderer.sprite = modes[0];
            IsCounting = true;
        }
        

    }
}
