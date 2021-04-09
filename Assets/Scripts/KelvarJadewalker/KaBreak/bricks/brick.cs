using UnityEngine;
using KelvarJadewalker.KaBreak.balls;
using KelvarJadewalker.KaBreak.templates;


namespace KelvarJadewalker.KaBreak.bricks
{
    public class Brick : MonoBehaviour
    {
        
        [SerializeField] private int pointsPerBrick = 1;
        [Range (1, 5)][SerializeField] private int hitsPerBrick = 1;
        [Range (1, 3)][SerializeField] private float speedUpFactor = 1.0f;
        [SerializeField] private bool isDestructible = true;
        [SerializeField] private bool usePrototypeColors = true;

        [Header("Sprites")]
        [SerializeField] private Sprite defaultSprite = null;
        [SerializeField] private Sprite indestructibleSprite = null;
        [SerializeField] private Sprite[] alternateSprites = null;

       
        public bool IsDestructible => isDestructible;
        
        private LevelManager _levelManager;
        private SpriteRenderer _spriteRenderer;
        
        private int _hitsRemaining;
        private int _timesHit;
        

        private void Awake()
        {
             _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            
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
             
            _levelManager.LostBrick(pointsPerBrick * hitsPerBrick, transform);
            Destroy(gameObject);
        }

        private void AssignPrototypeColor()
        {
            if (!isDestructible)
            {
                _spriteRenderer.color = Color.gray;
                return;
            }

            _spriteRenderer.color = _hitsRemaining switch
            {
                1 => Color.blue,
                2 => Color.green,
                3 => Color.yellow,
                4 => Color.red,
                5 => Color.magenta,
                _ => Color.blue
            };
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
            // Indestructible bricks do not change
            if (!isDestructible)
            {
                _spriteRenderer.sprite = indestructibleSprite;
                return;
            }
            if (_hitsRemaining <= 1)
            {
                _spriteRenderer.sprite = defaultSprite;
                return;
            }

            // Prevent any silly null reference errors
            var maxSprites = alternateSprites.Length -1;
            var spriteIndex = Mathf.Clamp(_hitsRemaining - 2, 0, maxSprites);

            _spriteRenderer.sprite = alternateSprites[spriteIndex];
        }

        public void MakeIndestructible()
        {
            // Can be called to turn the brick off to being destructible
            isDestructible = false;
            hitsPerBrick = 1;
            speedUpFactor = 1;
            _timesHit = 0;
            SetBrickDisplay();
        }

        public void SetBrickLevel(int level)
        {
            switch (level)
            {
                case 1:
                    hitsPerBrick = 1;
                    speedUpFactor = 1;
                    break;
                case 2:
                    hitsPerBrick = 2;
                    speedUpFactor = 1;
                    break;
                case 3:
                    hitsPerBrick = 3;
                    speedUpFactor = 1;
                    break;
                case 4:
                    hitsPerBrick = 4;
                    speedUpFactor = 2;
                    break;
                case 5:
                    hitsPerBrick = 5;
                    speedUpFactor = 3;
                    break;
             }
            
            isDestructible = true;
            usePrototypeColors = false;
            _hitsRemaining = hitsPerBrick;
            _timesHit = 0;
            SetBrickDisplay();
        }
        
    }
}
