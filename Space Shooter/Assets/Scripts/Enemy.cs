using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _enemySpeed = 5f;
    Player _player;
    [SerializeField]
    Animator _animator;

    [SerializeField] AudioClip _destroyAudio;
    AudioSource _audioSource;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null) Debug.LogError("Player is Null");
        if (_audioSource == null) Debug.LogError("Audio Source is Null"); 
    }

    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if (transform.position.y < -5)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _audioSource.PlayOneShot(_destroyAudio);
            DestroyThis();
        }
        else if ( other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            AudioSource audioSource = other.transform.GetComponent<AudioSource>();
            if (audioSource == null) Debug.LogError("Audio Source is Null on player");
            else
            {
                audioSource.PlayOneShot(_destroyAudio);
            }
                DestroyThis();
        }
    }

    void DestroyThis()
    {
        _animator.SetTrigger("OnEnemyDeath");
        Destroy(GetComponent<Collider2D>());
        _enemySpeed = 0;
        Destroy(gameObject, 2.5f);
    }
}