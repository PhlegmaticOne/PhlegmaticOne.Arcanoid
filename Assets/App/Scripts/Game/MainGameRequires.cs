using Game.Base;
using Game.Field.Helpers;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using UnityEngine;

namespace Game
{
    public class MainGameRequires : IGameRequires
    {
        public ControlSystem ControlSystem { get; set; }
        public Ship Ship { get; set; }
        public InteractableZoneSetter InteractableZoneSetter { get; set; }
        public Camera Camera { get; set; }
    }
}