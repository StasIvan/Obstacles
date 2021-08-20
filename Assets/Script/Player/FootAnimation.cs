using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FootAnimation : MonoBehaviour
{
    public static float _footSpeed = 1f;
    public static float _forwardDistance = 0.4f;
    public static float _backwardDistance = 0;
    float _desiredHeight = 50.0f;
    float _targetDistance;
    float _verticalDistance;
    float _currentDistance;
    float _currentVerticaltDistance;
    int _animationCount = 0;
    Transform _thisTransform;
    Coroutine moveCoroutine;
    List<float> _listDistance = new List<float>();
    public static Transform Ground;
    Transform _oldGround;

    private void Awake()
    {
        _thisTransform = GetComponent<Transform>();
        _thisTransform.position = new Vector3(_thisTransform.position.x, _thisTransform.position.y, _thisTransform.position.z - _forwardDistance / 2);
        _listDistance.AddRange(new float[] { _forwardDistance, _backwardDistance });
    }
    
    public IEnumerator AnimationController()
    {

        while (true)
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            ChooseNewEndpoint();
            if (Ground != null && _oldGround != Ground) 
            {
                RotateFoot();
                _oldGround = Ground;
            }
            yield return moveCoroutine = StartCoroutine(Animate());
        }
    }

    void ChooseNewEndpoint()
    {
        if (_animationCount < _listDistance.Count)
        {
            _targetDistance = (_listDistance.ElementAt(_animationCount));
            _animationCount++;
        }
        else
        {
            _animationCount = 0;
        }
        if (Ground != null)
        {
            _verticalDistance = Mathf.Abs(_forwardDistance * Mathf.Tan((360.0f - Ground.eulerAngles.x) * Mathf.PI / 180));
            if ((_targetDistance == _backwardDistance && (360.0f - Ground.eulerAngles.x) < 180.0f) || (_targetDistance == _forwardDistance && (360.0f - Ground.eulerAngles.x) > 180.0f))  
            {
                _verticalDistance *= -1;
            }
            
        }
    }

    public void RotateFoot()
    {
        _thisTransform.rotation = Quaternion.Euler(_thisTransform.eulerAngles.x, _thisTransform.eulerAngles.y, _thisTransform.eulerAngles.z + Ground.eulerAngles.x - (_oldGround != null ? _oldGround.eulerAngles.x : 0));
    }    

    private IEnumerator Animate()
    {
        float _sin = 0;
        float _deltaDistance;
        float _deltaVerticalDistance = 0;
        while (_targetDistance <= float.Epsilon ? _currentDistance >= _targetDistance : _currentDistance <= _targetDistance)
        {
            if (_targetDistance == _forwardDistance)
            {
                _deltaDistance = Time.fixedDeltaTime * _footSpeed;
                _sin = Mathf.Sin(Mathf.PI * _currentDistance / (_forwardDistance / 2)) / _desiredHeight;
            }
            else
                _deltaDistance = Time.fixedDeltaTime * _footSpeed * -1f;
            _thisTransform.Translate(Vector3.forward * _deltaDistance + Vector3.up * _sin + Vector3.up * _deltaVerticalDistance, Space.World);
            _currentDistance += _deltaDistance;
            if (_verticalDistance <= float.Epsilon ? _currentVerticaltDistance >= _verticalDistance / 2 : _currentVerticaltDistance <= _verticalDistance / 2)
            {
                _deltaVerticalDistance = Time.fixedDeltaTime * _footSpeed * (_verticalDistance != 0 ? _verticalDistance / Mathf.Abs(_verticalDistance) : 0);
                _currentVerticaltDistance += _deltaVerticalDistance;
            }
            else
            {
                _deltaVerticalDistance = 0;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
