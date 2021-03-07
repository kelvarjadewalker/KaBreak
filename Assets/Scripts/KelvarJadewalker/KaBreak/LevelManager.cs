using System;
using System.Collections;
using System.Linq;
using KelvarJadewalker.KaBreak.bricks;
using KelvarJadewalker.KaBreak.enums;
using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject paddle = null;
        [SerializeField] private GameObject explosionPrefab = null;
        [SerializeField] private GameObject[] powerUps = null;
        [Range(1,5)][SerializeField] private int blueScoreModifier = 2;

        private bool CanCollectPowerUp { get; set; }
        private readonly WaitForSeconds blueWaitForSeconds = new WaitForSeconds(20f);
        private const int BaseScoreModifier = 1;
        
        private int _numberOfBricks;
        private int _scoreModifier;
        private int _numberOfPowerUps;
        
        
        // The Level manager handles game related object for this scene/level
        private GameManager _gameManager;

        

        // Start is called before the first frame update
        private void Start()
        {
            // By convention this is guaranteed to exist
            _gameManager = FindObjectOfType<GameManager>();

            _numberOfBricks = GetNumberOfDestructibleBricks();
            _numberOfPowerUps = powerUps.Length;
           
            ResetLevel();
        }
        
        public int GetNumberOfDestructibleBricks()
        { 
            // Since not every Brick using the Brick script can be destroyed we need to account for this
            var bricks = FindObjectsOfType<Brick>();
            
            // Not a pro with LINQ expressions but this replaces a for loop
            return bricks.Count(brick => brick.IsDestructible);
        }

        public void LostBrick(int points, Transform brick)
        {
            _gameManager.LostBrick(points * _scoreModifier);
            Instantiate(explosionPrefab, brick.position, explosionPrefab.transform.rotation);
            
            if (CanCollectPowerUp)
            {
                Debug.Log("Power Up");
                var powerUp = powerUps[0];
                Instantiate(powerUp, brick.position, Quaternion.identity);
            }
        }
        
        private IEnumerator BlueScoreModifier()
        {
            CanCollectPowerUp = false;
            _scoreModifier = blueScoreModifier;
            yield return blueWaitForSeconds ;
            _scoreModifier = BaseScoreModifier;
            CanCollectPowerUp = true;
            Debug.Log("Power up Period Has ended");
        }

        private void ResetLevel()
        {
            CanCollectPowerUp = true;
            _scoreModifier = BaseScoreModifier;
        }

        public void CollectedPowerUp(PowerUpTypes powerUpType)
        {
            switch (powerUpType)
            {
               case PowerUpTypes.Blue :
                   Debug.Log("Power Up Collected : " + powerUpType);
                   StartCoroutine(BlueScoreModifier()); 
                   break;
               default:
                   throw new ArgumentOutOfRangeException(nameof(powerUpType), powerUpType, null);
            }
        }

        
    }
}
