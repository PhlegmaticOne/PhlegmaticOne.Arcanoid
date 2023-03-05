using Game.Blocks;
using Game.Blocks.Spawners;
using Game.Field.Builder;
using Game.PlayerObjects;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Factories;
using Game.Systems;
using Libs.InputSystem;
using Libs.Pooling;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using Libs.Services;
using UnityEngine;

namespace Game.Composite
{
    public class MainGameInstaller : MonoBehaviour
    {
        [SerializeField] private InputSystemInstaller _inputSystemInstaller;
        [SerializeField] private BlockSpawnerInstaller _blockSpawnerInstaller;
        [SerializeField] private BallSpawnerInstaller _ballSpawnerInstaller;

        [SerializeField] private FieldBuilder _fieldBuilder;
        [SerializeField] private ControlSystem _controlSystem;
        [SerializeField] private Racket _racket;

        public void AddPools(PoolBuilder poolBuilder)
        {
            poolBuilder.AddPool(_blockSpawnerInstaller.BlockPoolInstaller.CreateObjectPool());
            poolBuilder.AddPool(_ballSpawnerInstaller.BallPoolInstaller.CreateObjectPool());
        }
        
        public MainGame CreateGame(IServiceCollection serviceCollection, IPoolProvider poolProvider)
        {
            var blockSpawner = _blockSpawnerInstaller.CreateBlockSpawner(poolProvider);
            var ballSpawner = _ballSpawnerInstaller.CreateBallSpawner(poolProvider);
            var inputSystem = _inputSystemInstaller.Create();
            _fieldBuilder.Initialize(blockSpawner);
            _controlSystem.Initialize(inputSystem.CreateInput());

            return new MainGame(_fieldBuilder, ballSpawner, _racket);
        }
    }
}