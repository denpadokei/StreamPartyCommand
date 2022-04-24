using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StreamPartyCommand.Utilities;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Models
{
    public class RainbowUtil
    {
        private static readonly int colorID = Shader.PropertyToID("_Color");
        private static readonly int addColorID = Shader.PropertyToID("_AddColor");
        private static readonly int tintColorID = Shader.PropertyToID("_TintColor");

        private IAudioTimeSource _timeSource;
        private BasicBeatmapObjectManager _beatmapObjectManager;
        private ConditionalWeakTable<GameNoteController, ColorNoteVisuals> noteVisualsMap = new ConditionalWeakTable<GameNoteController, ColorNoteVisuals>();

        [Inject]
        public RainbowUtil(IAudioTimeSource timeSource, BasicBeatmapObjectManager beatmapObjectManager)
        {
            this._timeSource = timeSource;
            this._beatmapObjectManager = beatmapObjectManager;
        }

        public bool IsRainbow(string name)
        {
            return name == "rainbow";
        }

        public void SetNoteColorRainbow(Color leftColor, Color rightColor)
        {
            UpdateNoteColorsRainbow(leftColor, rightColor);
        }

        private void UpdateNoteColorsRainbow(Color leftColor, Color rightColor)
        {
            List<GameNoteController> notes = ReflectionUtil.GetPrivateField<MemoryPoolContainer<GameNoteController>>(_beatmapObjectManager, "_basicGameNotePoolContainer").activeItems;

            foreach (GameNoteController note in notes)
            {
                if (!noteVisualsMap.TryGetValue(note, out ColorNoteVisuals noteVisuals))
                {
                    noteVisualsMap.Add(note, noteVisuals = note.GetComponent<ColorNoteVisuals>());
                }

                Color color;

                switch (note.noteData.colorType)
                {
                    case ColorType.ColorA:
                        color = leftColor;
                        break;
                    case ColorType.ColorB:
                        color = rightColor;
                        break;
                    default:
                        continue;
                }

                float arrowGlowIntensity = ReflectionUtil.GetPrivateField<float>(noteVisuals, "_defaultColorAlpha");
                ReflectionUtil.SetPrivateField<Color>(noteVisuals, "_noteColor", color);

                MaterialPropertyBlockController[] propertyBlockControllers = ReflectionUtil.GetPrivateField<MaterialPropertyBlockController[]>(noteVisuals, "_materialPropertyBlockControllers");

                foreach (MaterialPropertyBlockController propertyBlockController in propertyBlockControllers)
                {

                    propertyBlockController.materialPropertyBlock.SetColor(colorID, color.ColorWithAlpha(arrowGlowIntensity));
                    propertyBlockController.ApplyChanges();
                }
            }
        }

        public void SetWallRainbowColor(Color color)
        {
            UpdateWallRainbowColor(color);
        }

        private void UpdateWallRainbowColor(Color color)
        {
            foreach (ObstacleController wall in _beatmapObjectManager.activeObstacleControllers)
            {
                StretchableObstacle stretchable = ReflectionUtil.GetPrivateField<StretchableObstacle>(wall, "_stretchableObstacle");
                ParametricBoxFrameController frame = ReflectionUtil.GetPrivateField<ParametricBoxFrameController>(stretchable, "_obstacleFrame");
                ParametricBoxFakeGlowController fakeGlow = ReflectionUtil.GetPrivateField<ParametricBoxFakeGlowController>(stretchable, "_obstacleFakeGlow");
                frame.color = color;
                frame.Refresh();
                if (fakeGlow != null)
                {
                    fakeGlow.color = color;
                    fakeGlow.Refresh();
                }

                Color value = color * 0.1f;
                value.a = 0f;
                MaterialPropertyBlockController[] propertyBlockControllers = ReflectionUtil.GetPrivateField<MaterialPropertyBlockController[]>(stretchable, "_materialPropertyBlockControllers");
                foreach (MaterialPropertyBlockController propertyBlockController in propertyBlockControllers)
                {
                    propertyBlockController.materialPropertyBlock.SetColor(addColorID, value);
                    propertyBlockController.materialPropertyBlock.SetColor(tintColorID, Color.Lerp(color, Color.white, 0.75f));
                    propertyBlockController.ApplyChanges();
                }
            }
        }

        public Color LeftRainbowColor()
        {
            float hue = _timeSource.songTime / 5f % 1f;
            return Color.HSVToRGB(hue, 1f, 1f);
        }

        public Color RightRainbowColor()
        {
            float hue = (_timeSource.songTime / 5f + 0.5f) % 1f;
            return Color.HSVToRGB(hue, 1f, 1f);
        }

        public Color WallRainbowColor()
        {
            float hue = (_timeSource.songTime / 5f + 0.25f) % 1f;
            return Color.HSVToRGB(hue, 1f, 1f);
        }


        
    }
}
