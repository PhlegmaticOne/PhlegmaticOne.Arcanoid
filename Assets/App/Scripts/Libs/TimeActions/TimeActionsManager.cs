using System;
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

        public bool TryGetAction<TAction>(out TAction action) where TAction : ITimeAction
        {
            action = (TAction)_timeActions.SingleOrDefault(x => x is TAction);
            return action != null;
        }

        public bool TryGetAction<TAction>(Func<TAction, bool> predicate, out TAction action) 
            where TAction : ITimeAction
        {
            action = (TAction)_timeActions.FirstOrDefault(x => x is TAction action && predicate(action));
            return action != null;
        }

        public void StopAllExcept(ITimeAction timeAction)
        {
            for (var i = _timeActions.Count - 1; i >= 0; i--)
            {
                var action = _timeActions[i];

                if (action == timeAction)
                {
                    continue;
                }

                EndAction(action);
                _timeActions.Remove(action);
            }
        }

        public void StopAllActions()
        {
            foreach (var timeAction in _timeActions)
            {
                EndAction(timeAction);
            }
            _timeActions.Clear();
        }

        private void EndAction(ITimeAction timeAction)
        {
            timeAction.RemainTime = 0;
            timeAction.OnEnd();
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