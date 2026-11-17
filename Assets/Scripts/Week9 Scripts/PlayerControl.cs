using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform[] _lanes;
    [SerializeField] float _easeSpd = 10f;
    [SerializeField] float _jumpF = 5f;
    [SerializeField] float _gravity = -20f;
    public int playerLane = 1; //Indicator where the current player is!!!! 
    private bool _isJumping = false;
    private float _baseY;
    private float _velocityY;
    
    private void Start()
    {
        _baseY = transform.position.y;
    }

    private void Update()
    {
        if (_lanes == null || _lanes.Length == 0) return;

        float _target = _lanes[playerLane].position.x;
        float _currentX = transform.position.x;
        float _distance = _target - _currentX;

        float _step = _distance * _easeSpd * Time.deltaTime;
        float _newX = _currentX + _step;
        float _newY = transform.position.y;


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerLane > 0)
            {
                playerLane -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (playerLane < _lanes.Length - 1)
            {
                playerLane += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _velocityY = _jumpF;
        }
            if (_isJumping)
            {
                _velocityY += _gravity * Time.deltaTime;
                _newY += _velocityY * Time.deltaTime;

                if (_newY <= _baseY)
                {
                    _newY = _baseY;
                    _velocityY = 0f;
                    _isJumping = false;
                }
            }

            transform.position = new Vector3(_newX, _newY, transform.position.z);

        }

    }

