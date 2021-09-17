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
        private void Awake()
        {
            this._textGO = new GameObject("DummyBombText", typeof(TextMeshPro));
            this._textGO.transform.SetParent(this.transform, false);
            this._textGO.transform.localPosition = Vector3.zero;
            this._text = this._textGO.GetComponent<TextMeshPro>();
            this._text.fontSize = 30;
        }

        private void OnDestroy()
        {
            Destroy(this._textGO);
        }

        public virtual void InitAndPresent(string text, float duration, Vector3 targetPos, Quaternion rotation, Color color, float fontSize, bool shake)
        {
            this._color = color;
            this._text.text = text;
            this._text.fontSize = fontSize;
            base.InitAndPresent(duration, targetPos, rotation, shake);
        }

        protected override void ManualUpdate(float t)
        {
            this._text.color = this._color.ColorWithAlpha(this._fadeAnimationCurve.Evaluate(t));
        }

        private GameObject _textGO;
        private TextMeshPro _text;
        private Color _color;
        private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
        public class Pool : MonoMemoryPool<FlyingBombNameEffect>
        {

        }
    }
}
