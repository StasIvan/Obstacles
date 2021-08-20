using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    CharacterController _controller;
    //Rigidbody _rb;
    float _characterYVelocity;
    [SerializeField] protected float _characterSpeed = 1.0f;
    float _gravityValue = -9.81f;
    bool _groundedCharacter;
    [SerializeField] FootAnimation _footTransformL;
    [SerializeField] FootAnimation _footTransformR;
    //bool _isFootRotate;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        //_thisTransform = transform;
        //_isFootRotate = true;
        //_rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //CharacterMove();
    }
    private void FixedUpdate()
    {
        CharacterMove();
    }
    void CharacterMove()
    {
        if (_groundedCharacter && _characterYVelocity < 0)
        {
            _characterYVelocity = 0f;
        }

        _controller.Move(Vector3.forward * Time.fixedDeltaTime * _characterSpeed);
        
        //_thisTransform.Translate(Vector3.forward * _characterSpeed * Time.fixedDeltaTime );

        //_rb.AddForce(Vector3.forward * _characterSpeed, ForceMode.Acceleration);
        //_rb.velocity = Vector3.forward * _characterSpeed;
        _controller.Move(Vector3.up * _characterYVelocity * Time.fixedDeltaTime);
        _characterYVelocity += _gravityValue * Time.fixedDeltaTime;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            FootAnimation.Ground = collision.transform;
            if (_isFootRotate)
            {
                _footTransformL.RotateFoot();
                _footTransformR.RotateFoot();
                _isFootRotate = false;
            }
            _groundedCharacter = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _groundedCharacter = false;
        }
    }*/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("Ground"))
        {
            FootAnimation.Ground = hit.transform;
            //if (_isFootRotate)
            //{
            //    _footTransformL.RotateFoot();
            //    _footTransformR.RotateFoot();
            //    _isFootRotate = false;
            //}

        }
    }

}
