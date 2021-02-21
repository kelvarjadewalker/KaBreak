using UnityEngine;

namespace KelvarJadewalker.KaBreak.balls
{
    public class KinematicBall : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5.0f;


        private float _speed;
        private Vector2 _currentDirection = new Vector2(0.0f, -1.0f);
        private Rigidbody2D _rigidbody;
        
        // Start is called before the first frame update
        private void Start()
        {
            _speed = initialSpeed;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var newDirection = new Vector2(_currentDirection.x, _currentDirection.y);
            newDirection *= Time.fixedDeltaTime * _speed;
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
            if (other.gameObject.CompareTag("BottomWall"))
            {
                gameObject.SetActive(false);
            }
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
    }
}
