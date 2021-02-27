using UnityEngine;
using KelvarJadewalker.KaBreak.balls;

namespace KelvarJadewalker.KaBreak.bricks
{
    public class Brick : MonoBehaviour
    {
        
        [SerializeField] private int pointsPerBrick = 1;
        [Range (1, 5)][SerializeField] private int hitsPerBrick = 1;
        [Range (1, 3)][SerializeField] private float speedUpFactor = 1.0f;
        [SerializeField] private bool isDestructible = true;
        [SerializeField] private bool usePrototypeColors = true;
        
        public bool IsDestructible => isDestructible;
        
        private GameManager _gameManager;
        private SpriteRenderer _spriteRenderer;
        private int _hitsRemaining;
        private int _timesHit;
        

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _hitsRemaining = hitsPerBrick;
            _timesHit = 0;
            SetBrickDisplay();

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!isDestructible || !other.gameObject.CompareTag("Player")) return;
            _timesHit++;
            _hitsRemaining--;

            // We will apply a speedup/slowdown only on the first hit
            if (_timesHit == 1)
            {
                var player = other.gameObject.GetComponent<KinematicBall>();
                if(player != null) player.SetSpeedFactor(speedUpFactor);
            }
            
            
            if (_hitsRemaining > 0)
            {
                SetBrickDisplay();
                return;
            }
            _gameManager.LostBrick(pointsPerBrick * hitsPerBrick);
            Destroy(gameObject);
        }

        private void AssignPrototypeColor()
        {
            if (!isDestructible)
            {
                _spriteRenderer.color = Color.gray;
                return;
            }

            switch (_hitsRemaining)
            {
                case  1:
                    _spriteRenderer.color = Color.blue; 
                    break;
                case 2:
                    _spriteRenderer.color = Color.green;
                    break;
                case 3:
                    _spriteRenderer.color = Color.yellow;
                    break;
                case 4:
                    _spriteRenderer.color = Color.red;
                    break;
                case 5:
                    _spriteRenderer.color = Color.magenta;
                    break;
                default:
                    _spriteRenderer.color = Color.blue; 
                    break;
            }
        }

        private void SetBrickDisplay()
        {
            if (usePrototypeColors)
            {
                AssignPrototypeColor();
            }
            else
            {
                AssignSprite();
            }
        }

        private void AssignSprite()
        {
            // TODO : Implement code to change the sprite based on hits remaining
            Debug.Log("Need to implement code to change the sprite");
        }
        
    }
}
