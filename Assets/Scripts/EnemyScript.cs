using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{    
    [SerializeField] private float _health = 100;
    [SerializeField] private float _shotcounter;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 1;
    [SerializeField] private float _enemyLaserSpeed = 10;
    [SerializeField] private GameObject _enemyLaser;

    // Start is called before the first frame update
    void Start()
    {
        _shotcounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        _shotcounter -= Time.deltaTime;
        if (_shotcounter <= 0)
        {
            Fire();
            _shotcounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject EnemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_enemyLaserSpeed);
    }    

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        _health -= damageDealer.GetDamage();
    }
}
