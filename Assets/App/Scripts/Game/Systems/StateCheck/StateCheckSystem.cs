using Game.Blocks;
using Game.Field;
using UnityEngine.Events;

namespace Game.Systems.StateCheck
{
    public class StateCheckSystem
    {
        private readonly GameField _gameField;
        
        public event UnityAction ActiveBlocksDestroyed;

        public StateCheckSystem(GameField gameField)
        {
            _gameField = gameField;
            _gameField.BlockRemoved += GameFieldOnBlockRemoved;
        }

        private void GameFieldOnBlockRemoved(Block block)
        {
            if (_gameField.ActiveBlocksCount == 0)
            {
                ActiveBlocksDestroyed?.Invoke();
            }
        }

        public void Disable()
        {
            _gameField.BlockRemoved -= GameFieldOnBlockRemoved;
        }
    }
}