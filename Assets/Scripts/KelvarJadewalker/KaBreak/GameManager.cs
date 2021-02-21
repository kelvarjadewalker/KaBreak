using System;
using System.Linq;
using KelvarJadewalker.KaBreak.bricks;
using KelvarJadewalker.KaBreak.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KelvarJadewalker.KaBreak
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab = null;
        [SerializeField] private Transform ballStart = null;
        [SerializeField] private int maxNumberOfLives = 3;
        [SerializeField] private int numberOfLevels = 1;
        
        public bool GameIsActive { get; private set; }

        private GameState _gameState;
        private int _numberOfBricks;
        private int _numberOfLives;
        private int _ballsInPlay;
        private int thisLevel = 1;
        
        private void OnGUI()
        {
            var numberOfBricksText = "Number of Bricks : " + _numberOfBricks;
            var numberOfLivesText = "Number of Lives :" + _numberOfLives;
            
            if (_gameState == GameState.GameLost || _gameState == GameState.GameWon)
            {
                GUI.Label(new Rect(25, 70, 300, 50),  "GAME OVER PRES 'R' to RESTART" );
            }
            else
            {
                GUI.Label(new Rect(25, 55, 300, 50),  numberOfBricksText );
                GUI.Label(new Rect(25, 70, 300, 50),  numberOfLivesText );
            }
        } 
        

        private void Awake()
        {
            _gameState = GameState.Starting;
            _numberOfBricks = GetNumberOfDestructibleBricks();
            
            // TODO : come up with a persistant scene to scene way to track this
            _numberOfLives = maxNumberOfLives;
        }
         private void Start()
         { 
             _ballsInPlay = 0;
             _gameState = GameState.LevelStart;
             GameIsActive = true;
         }

        // Update is called once per frame
        private void Update()
        {
            switch (_gameState)
            {
                case GameState.Starting:
                    // We should never really get here
                    break;
                case GameState.Running:
                    break;
                case GameState.LevelStart:
                    CanPutBallInPlay();
                    break;
                case GameState.LevelLost:
                    LevelLost();
                    break;
                case GameState.LevelWon:
                    LevelWon();
                    break;
                case GameState.GameLost:
                    GameLost();
                    break;
                case GameState.GameWon:
                    GameWon();
                    break;
                default:
                    // This should never happen but we'll leave for de-bugging
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CanPutBallInPlay()
        {
            // Wait for an action to start the ball
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            // we don't need a reference to the ball at the moment
            var ballPosition = ballStart.position;
            Instantiate(ballPrefab, ballPosition, ballPrefab.transform.rotation);
            _gameState = GameState.Running;
            _ballsInPlay++;
        }

        private void LevelLost()
        {
            _numberOfLives--;
            _gameState = _numberOfLives <= 0 ? GameState.GameLost : GameState.LevelStart;
        }

        private void LevelWon()
        {
            // By convention the levels will be set in the Build Order so we can do math to get the next level
            var nextLevel = thisLevel + 1;
            
            // For now there is just this level so this will always be true
            if (nextLevel >= numberOfLevels) _gameState = GameState.GameWon;
        }

        private void GameLost()
        {
            GameOver();
        }

        private void GameWon()
        {
            GameOver();
        }

        private void GameOver()
        {
            GameIsActive = false;
            
            // Wait for the user to restart the game
            if (!Input.GetKeyDown(KeyCode.R)) return;
            
            // We're just going to restart the scene for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        public void LostBall()
        {
            _ballsInPlay--;
            if (_ballsInPlay <= 0) _gameState = GameState.LevelLost;
        }

        public void LostBrick()
        {
            _numberOfBricks--;
            if (_numberOfBricks <= 0) _gameState = GameState.LevelWon;
        }
        
        private int GetNumberOfDestructibleBricks()
        { 
            // Since not every Brick using the Brick script can be destroyed we need to account for this
            var bricks = FindObjectsOfType<Brick>();
            return bricks.Count(brick => brick.IsDestructible);
        }
    }
}
