using System.Collections.Generic;
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