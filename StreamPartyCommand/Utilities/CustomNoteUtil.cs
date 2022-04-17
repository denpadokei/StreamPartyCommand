using IPA.Loader;
using System;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Utilities
{
    public class CustomNoteUtil
    {
        public bool IsInstallCustomNote { get; private set; }
        private readonly object _loader;
        public int SelectedNoteIndex => this._loader == null ? -1 : (int)this._loader.GetType().GetProperty("SelectedNote").GetValue(this._loader);
        public bool Enabled => this._loader != null && (bool)this._loader.GetType().GetProperty("Enabled").GetValue(this._loader);

        [Inject]
        public CustomNoteUtil(DiContainer container)
        {
            this.IsInstallCustomNote = PluginManager.GetPluginFromId("Custom Notes") != null;
            var loaderType = Type.GetType("CustomNotes.Managers.NoteAssetLoader, CustomNotes");
            if (loaderType == null) {
                this._loader = null;
            }
            else {
                this._loader = container.TryResolve(loaderType);
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
