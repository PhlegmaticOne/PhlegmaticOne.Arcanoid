using Game.Blocks.Spawners;
using Game.Field.Helpers;
using Game.Field.Installer;
using Game.PlayerObjects.BallObject.Factories;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Libs.InputSystem;
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
        [SerializeField] private FieldInstaller _fieldInstaller;

        [SerializeField] private InteractableZoneSetter _interactableZoneSetter;
        [SerializeField] private ControlSystem _controlSystem;
        [SerializeField] private Ship _ship;

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
            var fieldBuilder = _fieldInstaller.CreateFieldBuilder(blockSpawner);

            var interactableBounds = _interactableZoneSetter.CalculateZoneBounds(_fieldInstaller.GetFieldBounds());
            _interactableZoneSetter.SetInteractableZone(interactableBounds);
            
            _controlSystem.Initialize(inputSystem.CreateInput(), _ship);
            _controlSystem.SetInteractableBounds(interactableBounds);
            
            return new MainGame(fieldBuilder, _controlSystem, ballSpawner, _ship);
        }
    }
}