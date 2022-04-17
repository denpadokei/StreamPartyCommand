using IPA.Loader;
using System;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Utilities
{
    public class CustomNoteUtil
    {
        public bool IsInstallCustomNote { get; private set; }
        private object _loader;
        public int SelectedNoteIndex => _loader == null ? -1 : (int)_loader.GetType().GetProperty("SelectedNote").GetValue(_loader);
        public bool Enabled => _loader != null && (bool)_loader.GetType().GetProperty("Enabled").GetValue(_loader);

        [Inject]
        public CustomNoteUtil(DiContainer container)
        {
            IsInstallCustomNote = PluginManager.GetPluginFromId("Custom Notes") != null;
            var loaderType = Type.GetType("CustomNotes.Managers.NoteAssetLoader, CustomNotes");
            if (loaderType == null) {
                _loader = null;
            }
            else {
                _loader = container.TryResolve(loaderType);
            }
        }

        public static bool TryGetColorNoteVisuals(GameObject gameObject, out ColorNoteVisuals colorNoteVisuals)
        {
            colorNoteVisuals = gameObject.GetComponentInChildren<ColorNoteVisuals>();
            if (colorNoteVisuals == null) {
                var customColorType = Type.GetType("CustomNotes.Overrides.CustomNoteColorNoteVisuals, CustomNotes");
                colorNoteVisuals = (ColorNoteVisuals)gameObject.GetComponentInChildren(customColorType);
            }
            return colorNoteVisuals != null;
        }

        public static bool TryGetGameNoteController(GameObject gameObject, out GameNoteController component)
        {
            component = gameObject.GetComponentInChildren<GameNoteController>();
            return component != null;
        }
    }
}
