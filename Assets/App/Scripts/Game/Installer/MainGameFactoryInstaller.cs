using Game.Base;
using Game.Blocks.Spawners;
using Game.Field.Helpers;
using Game.Field.Installer;
using Game.PlayerObjects.BallObject.Spawners;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using Libs.Services;
using UnityEngine;

namespace Game.Composite
{
    public class MainGameFactoryInstaller : MonoBehaviour
    {
        [SerializeField] private InputSystemInstaller _inputSystemInstaller;
        [SerializeField] private BlockSpawnerInstaller _blockSpawnerInstaller;
        [SerializeField] private BallSpawnerInstaller _ballSpawnerInstaller;
        [SerializeField] private FieldBuilderInstaller _fieldInstaller;

        public void AddPools(PoolBuilder poolBuilder)
        {
            poolBuilder.AddPool(_blockSpawnerInstaller.BlockPoolInstaller.CreateObjectPool());
            poolBuilder.AddPool(_ballSpawnerInstaller.BallPoolInstaller.CreateObjectPool());
        }
        
        public IGameFactory<MainGameRequires, MainGame> CreateGame(IPoolProvider poolProvider)
        {
            var blockSpawner = _blockSpawnerInstaller.CreateBlockSpawner(poolProvider);
            var ballSpawner = _ballSpawnerInstaller.CreateBallSpawner(poolProvider);
            var inputSystem = _inputSystemInstaller.Create();
            var fieldBuilder = _fieldInstaller.CreateFieldBuilder(blockSpawner);
            return new MainGameFactory(fieldBuilder, poolProvider, ballSpawner, inputSystem.CreateInput());
        }
    }
}