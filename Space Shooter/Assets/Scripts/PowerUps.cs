using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] 
    float _powerUpSpeed = 3;
    [SerializeField]
    private int powerupID; // 1 Triple shot 2 Speed 3 Shield

    [SerializeField]
    AudioClip _collectPowerupAudio;
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);
        if (transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if (_collectPowerupAudio != null)
                {
                    AudioSource.PlayClipAtPoint(_collectPowerupAudio, transform.position);
                }
                else
                {
                    Debug.LogWarning("No AudioClip assigned to _collectPowerupAudio in PowerUps script!");
                }
                switch (powerupID){
                    case 0: 
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedPowerUp();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}