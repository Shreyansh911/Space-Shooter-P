using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private WaveConfig _waveConfig;

    private int _waypointIndex = 0;
    private List<Transform> _waypoints;

    // Start is called before the first frame update
    void Start()
    {
        _waypoints = _waveConfig.GetWayPoints();

        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void SetWaveConfig(WaveConfig _waveConfig)
    {
        this._waveConfig = _waveConfig;
    }

    private void Movement()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;
            var movementThisFrame = _waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
