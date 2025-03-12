using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float _rotateSpeed = 20f;
    [SerializeField]
    GameObject _explosionPrefab;
    SpawnManager _spawnManager;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _explosionAudio;

    private void Awake()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(explosion, 2.8f);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
