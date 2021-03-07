using System.Linq;
using KelvarJadewalker.KaBreak.bricks;
using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab = null;
        
        private int _numberOfBricks;
        
        // The Level manager handles game related object for this scene/level
        private GameManager _gameManager;
        
        // Start is called before the first frame update
        private void Start()
        {
            // By convention this is guaranteed to exist
            _gameManager = FindObjectOfType<GameManager>();

            _numberOfBricks = GetNumberOfDestructibleBricks();
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
            _gameManager.LostBrick(points);
            Instantiate(explosionPrefab, brick.position, explosionPrefab.transform.rotation);
        }
    }
}
