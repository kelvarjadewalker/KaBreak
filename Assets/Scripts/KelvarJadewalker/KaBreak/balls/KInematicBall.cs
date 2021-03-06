﻿using System;
using UnityEngine;

namespace KelvarJadewalker.KaBreak.balls
{
    public class KinematicBall : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5.0f;
        [SerializeField] private float minimumSpeedFactor = 1.0f;
        [SerializeField] private float maximumSpeedFactor = 5.0f;
        [SerializeField] private Sprite alternateSprite = null;
        

        // by convention the ball will move up when instantiated 
        private Vector2 _currentDirection = new Vector2(0.0f, 1.0f);
        private Rigidbody2D _rigidbody;
        private GameManager _gameManager;
        private float _speed;
        private float _speedFactor;
 
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _speed = initialSpeed;
            _speedFactor = 1.0f;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_gameManager.GameIsActive) return;
            ExitGame();
        }

        private void ExitGame()
        {
            // If the Game Manager stops the game kill this object
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            var newDirection = new Vector2(_currentDirection.x, _currentDirection.y);
            newDirection *= Time.fixedDeltaTime * _speed * _speedFactor;
            _rigidbody.MovePosition(_rigidbody.position + newDirection);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var dx = _currentDirection.x;
            var dy= _currentDirection.y;

            var hitFactorX = HitFactorX(transform.position, 
                                          other.transform.position, 
                                          other.collider.bounds.size.x);

            if (other.gameObject.CompareTag("Paddle"))
            {
                dx = hitFactorX;
                dy = -_currentDirection.y;
            }
            else if (other.gameObject.CompareTag("Brick"))
            {
                dx = hitFactorX;
                dy = -_currentDirection.y;
            }
            else if(other.gameObject.CompareTag("TopWall"))
            {
                dx = _currentDirection.x;
                dy = -_currentDirection.y;
                
            }
            else if (other.gameObject.CompareTag("SideWall"))
            {
                dx = -_currentDirection.x;
                dy = _currentDirection.y;
            }
            _currentDirection = new Vector2(dx, dy);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("BottomWall")) return;
            
            _gameManager.LostBall();
            Destroy(gameObject);
        }

        private static float HitFactorX(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
        {
            // ||  1 <- at the left side of the paddle / brick
            // ||
            // ||  0 <- at the middle of the paddle / brick
            // ||
            // || -1 <- at the right of the paddle / brick

            return (ballPosition.x - paddlePosition.x) / paddleWidth;
        }

        public void SetSpeedFactor(float factor)
        {
            // Ignore if the same value
            if(Math.Abs(_speedFactor - factor) < 0.001f) return;
            
            // Allows the player to be influenced by object that has been hit
            _speedFactor = Mathf.Clamp(factor, minimumSpeedFactor, maximumSpeedFactor);
        }
    }
}
