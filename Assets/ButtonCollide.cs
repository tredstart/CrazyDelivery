using UnityEngine;

public class ButtonCollide : MonoBehaviour
{
    public GameObject platform;
    public GameObject scoreTable;

    private Timer _timer;
    private Score _score;

    // Start is called before the first frame update
    private AudioSource _audio;
    private void Start()
    {
        _timer = platform.GetComponent<Timer>();
        _score = scoreTable.GetComponent<Score>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _audio.Play();
        Destroy(col.gameObject);
        if (_timer.IsCounting)
        {
            _timer.Seconds += 4;
            _score.S -= 10;
        }
        else
        {
            _timer.Seconds = 10;
            _score.S -= 50;
            _timer.Renderer.sprite = _timer.modes[0];
        }
        
    }
}
