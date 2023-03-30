using Common.Scenes;
using DG.Tweening;
using Game.Field;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.ReturnToPool
{
    public class ReturnBlockToPoolBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _disappearTime;
        [SerializeField] private Ease _disappearEase;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var global = ServiceProviderAccessor.Global;
            var game = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            
            var poolProvider = global.GetRequiredService<IPoolProvider>();
            var fieldAccessor = game.GetRequiredService<GameField>();
            var behavior = new ReturnBlockToPoolBehavior(poolProvider, fieldAccessor);
            behavior.SetBehaviorParameters(_disappearTime, _disappearEase);
            return behavior;
        }
    }
}