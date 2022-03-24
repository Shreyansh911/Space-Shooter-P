using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScoller : MonoBehaviour
{
    [SerializeField] float _BGMovingSpeed = 1;

    private Material _myMaterial;
    private Vector2 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _offset = new Vector2(0, _BGMovingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _myMaterial.mainTextureOffset += _offset * Time.deltaTime;
    }
}
