using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class FlyingBombNameEffect : FlyingObjectEffect
    {
        public virtual void InitAndPresent(string text, float duration, Vector3 targetPos, Quaternion rotation, Color color, float fontSize, bool shake)
        {
            base.InitAndPresent(duration, targetPos, rotation, shake);
            this._color = color;
            this._text.text = text;
            this._text.fontSize = fontSize;
        }

        protected override void ManualUpdate(float t)
        {
            this._text.color = this._color.ColorWithAlpha(this._fadeAnimationCurve.Evaluate(t));
        }

        private TextMeshPro _text;
        private Color _color;
        private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
        public class Pool : MemoryPool<FlyingBombNameEffect>
        {

        }
    }
}
