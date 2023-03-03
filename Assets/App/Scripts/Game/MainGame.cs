using Game.Field;
using UnityEngine;

namespace Game
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField] private GameField _gameField;

        public void StartGame()
        {
            _gameField.GenerateBlocks();
        }
    }
}