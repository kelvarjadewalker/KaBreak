using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class LevelManager : MonoBehaviour
    {
        // The Level manager handles game related object for this scene/level
        private GameManager _gameManager;
        
        // Start is called before the first frame update
        private void Start()
        {
            // By convention this is guaranteed to exist
            _gameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
