using System;
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

        private GameState _gameState;
        private int _numberOfLives;
        private int _ballsInPlay;
        
        private void OnGUI()
        {
            var numberOfLivesText = "Number of Lives :" + _numberOfLives;
  
            

            if (_gameState == GameState.GameOver)
            {
                GUI.Label(new Rect(25, 70, 300, 50),  "GAME OVER PRES 'R' to RESTART" );
            }
            else
            {
                GUI.Label(new Rect(25, 55, 300, 50),  numberOfLivesText );
            }
        } 
        

        private void Awake()
        {
            _gameState = GameState.Starting;
            
            // TODO : come up with a persistant scene to scene way to track this
            _numberOfLives = maxNumberOfLives;
        }
         private void Start()
         { 
             _ballsInPlay = 0;
             _gameState = GameState.LevelStart;
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
                case GameState.LevelOver:
                    RoundOver();
                    break;
                case GameState.GameOver:
                    GameOver();
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

        private void RoundOver()
        {
            _numberOfLives--;
            _gameState = _numberOfLives <= 0 ? GameState.GameOver : GameState.LevelStart;
        }

        private static void GameOver()
        {
            // Wait for the user to restart the game
            if (!Input.GetKeyDown(KeyCode.R)) return;
            
            // We're just going to restart the scene for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        public void LostBall()
        {
            _ballsInPlay--;
            if (_ballsInPlay <= 0) _gameState = GameState.LevelOver;
        }
    }
}
