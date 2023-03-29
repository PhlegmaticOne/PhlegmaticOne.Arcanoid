using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject.Behaviors.Punch
{
    public class PunchShipBehaviorInstaller : BehaviorInstaller<Ship>
    {
        [SerializeField] private Vector2 _punchDirection;
        [SerializeField] private float _time;
        
        public override IObjectBehavior<Ship> CreateBehaviour()
        {
            var behavior = new PunchShipBehavior();
            behavior.SetBehaviorParameters(_punchDirection, _time);
            return behavior;
        }
    }
}