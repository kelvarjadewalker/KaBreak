using UnityEngine;

namespace KelvarJadewalker.KaBreak.bricks
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private bool isDestructible = true;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!isDestructible || !other.gameObject.CompareTag("Player")) return;
            _gameManager.LostBrick();
            Destroy(gameObject);
        }
    }
}
