using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _padding = 0.5f;
    [SerializeField] private float _laserSpeed = 20;
    [SerializeField] private float _fireInterval = 0.1f;
    [SerializeField] private GameObject _laser;

    private Coroutine _firingCorouting;

    private float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        CameraEdges();
    }

     // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

  
 private void CameraEdges()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }
    private void Movement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _speed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXpos, newYpos);
    }

    void Shooting()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _firingCorouting =  StartCoroutine(FireBullets());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCorouting);
        }
    }
    
   IEnumerator FireBullets()
    {
        while(true)
        {
        GameObject Laser = Instantiate(_laser, transform.position, Quaternion.identity) as GameObject;

        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _laserSpeed);

        yield return new WaitForSeconds(_fireInterval);
        }
        
    }    
}
