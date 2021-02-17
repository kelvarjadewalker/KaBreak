using System;
using UnityEngine;

namespace KelvarJadewalker.KaBreak.balls
{
    public class KinematicBall : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5.0f;


        private float speed;
        private Vector2 currentDirection = new Vector2(0.0f, -1.0f);
        private Rigidbody2D _rigidbody;
        
        // Start is called before the first frame update
        private void Start()
        {
            speed = initialSpeed;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var newDirection = new Vector2(currentDirection.x, currentDirection.y);
            newDirection *= Time.fixedDeltaTime * speed;
            _rigidbody.MovePosition(_rigidbody.position + newDirection);
        }
    }
}
