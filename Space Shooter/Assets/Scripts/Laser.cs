using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 1.0f;

    void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if ( transform.position.y > 8)
        {
            if( transform.parent != null )
            {
                Destroy( transform.parent.gameObject );
            }
            Destroy(gameObject);
        }
    }
}