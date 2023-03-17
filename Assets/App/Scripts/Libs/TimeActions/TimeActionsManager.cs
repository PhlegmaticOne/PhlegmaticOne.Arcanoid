using System.Collections.Generic;
using System.Linq;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Libs.TimeActions
{
    public class TimeActionsManager : MonoBehaviour
    {
        private readonly List<ITimeAction> _timeActions = new List<ITimeAction>();

        public void AddTimeAction(ITimeAction timeAction)
        {
            _timeActions.Add(timeAction);
            timeAction.OnStart();
        }

        public bool ContainsAction<TAction>() where TAction : ITimeAction => _timeActions.Any(x => x is TAction);

        public bool TryGetAction<TAction>(out TAction action) where TAction : ITimeAction
        {
            action = (TAction)_timeActions.SingleOrDefault(x => x is TAction);
            return action != null;
        }

        public void StopAllActions()
        {
            foreach (var timeAction in _timeActions)
            {
                timeAction.RemainTime = 0;
                timeAction.OnEnd();
            }
            _timeActions.Clear();
        }

        private void Update()
        {
            if (_timeActions.Count == 0)
            {
                return;
            }
            
            var deltaTime = Time.deltaTime;

            for (var i = _timeActions.Count - 1; i >= 0; i--)
            {
                var timeAction = _timeActions[i];
                timeAction.RemainTime -= deltaTime;

                if (timeAction.RemainTime <= 0)
                {
                    timeAction.OnEnd();
                    _timeActions.Remove(timeAction);
                }
                else
                {
                    timeAction.OnUpdate(deltaTime);
                }
            }
        }
    }
}