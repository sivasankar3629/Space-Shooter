using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _playerSpeed = 5f;

    [SerializeField]
    GameObject _laserPrefab;
    [SerializeField]
    GameObject _tripleShotPrefab;

    [SerializeField]
    float _fireRate = 0.5f;
    float _nextFire = 0;
    [SerializeField]
    int _lives = 3;
    bool _isAlive = true;

    bool _isTripleShotActive = false;
    bool _isSpeedBoostActive = false;
    bool _isShieldActive = false;

    [SerializeField]
    GameObject _shield;

    int _score = 0;
    UIManager _uiManager;

    [SerializeField]
    GameObject[] _playerEngine;
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _laserSoundClip;

    int engine; // Engine no. for engine damage animation


    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_uiManager == null) Debug.LogError("UI Manager is Null");
        if (_audioSource == null) Debug.LogError("AudioSource is Null");
    }

    void Start()
    {
        transform.position = new Vector3(0,-3,0);
        engine = Random.Range(0, 2);
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) FireLaser();
    }

    void Movement()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal,vertical,0) * _playerSpeed * Time.deltaTime);

        // Bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if ( transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y,0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        // SFX
        _audioSource.PlayOneShot(_laserSoundClip);
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            StopCoroutine(DeactivateShieldPowerUp());
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }
        _lives--;

        _uiManager.UpdateLives(_lives);

        // Engine Failure animation
        
        if ( _lives == 2)
        {
            _playerEngine[engine].SetActive(true);
        }
        else if( _lives == 1)
        {
            if (engine == 0)
            {
                _playerEngine[1].SetActive(true);
            }
            else
                _playerEngine[0].SetActive(true);
        }

        if (_lives < 1)
        {
            _isAlive = false;
            Destroy(this.gameObject);
        }
    }

    public bool IsAlive()
    {
        return _isAlive;    
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedPowerUp()
    {
        _isSpeedBoostActive = true;
        float speed = _playerSpeed;
        _playerSpeed = speed * 2;
        StartCoroutine(DeactivateSpeedPowerUp(speed));
    }

    IEnumerator DeactivateSpeedPowerUp(float speed)
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoostActive = false;
        _playerSpeed = speed;
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
        StartCoroutine(DeactivateShieldPowerUp());
    }

    IEnumerator DeactivateShieldPowerUp()
    {
        yield return new WaitForSeconds(6f);
        _isShieldActive = false;
        _shield.SetActive(false);
    }

    public void AddScore( int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }
}
