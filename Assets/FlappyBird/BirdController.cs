using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float speed;
    public float flapVelocity;
    
    private Rigidbody _body;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var velocity = _body.velocity;
            velocity.y = flapVelocity;
            _body.velocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        var velocity = _body.velocity;
        velocity.x = speed;
        _body.velocity = velocity;

        var rotation = Quaternion.LookRotation(velocity);
        var rotate90 = Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
        _body.MoveRotation(rotation * rotate90);
    }

    private void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene("SampleScene");
    }
}
