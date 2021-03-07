using KelvarJadewalker.KaBreak.enums;
using UnityEngine;

namespace KelvarJadewalker.KaBreak.powerups
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpTypes powerUpType = PowerUpTypes.Blue;

        private LevelManager _levelManager;

        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Paddle"))
            {
                _levelManager.CollectedPowerUp(powerUpType);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("BottomWall"))
            {
                Debug.Log("Power Up Lost");
                Destroy(gameObject);
            }
            
        }
    }
}
