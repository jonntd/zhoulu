﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlayCameraShakeEventSkill : EventSkillSetData
    {

    }

    /// <summary>
    /// 镜头抖动
    /// </summary>
    public class PlayCameraShake : ASkillAction
    {
        public PlayCameraShakeEventSkill _data;

        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayCameraShakeEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_shake, _data);
            Finish();
        }

        public override void OnExit()
        {

        }

        public override void Destroy()
        {
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }
    }

}
