using IPA.Loader;
using System;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace StreamPartyCommand.Utilities
{
    public class CustomNoteUtil
    {
        public bool IsInstallCustomNote { get; private set; }
        private readonly object _loader;
        private readonly object _customNotesConfig;
        public int SelectedNoteIdx => this._loader == null ? -1 : (int)this._loader.GetType().GetProperty("SelectedNoteIdx").GetValue(this._loader);
        public bool Enabled => this._customNotesConfig != null && (bool)this._customNotesConfig.GetType().GetProperty("Enabled").GetValue(this._customNotesConfig);
        private static readonly Type s_customNoteController;
        private static readonly PropertyInfo s_customNoteControllerColorInfo;

        static CustomNoteUtil()
        {
            s_customNoteController = Type.GetType("CustomNotes.Managers.CustomNoteController, CustomNotes");
            s_customNoteControllerColorInfo = s_customNoteController?.GetProperty("Color", BindingFlags.Instance | BindingFlags.Public);
            
        }

        [Inject]
        public CustomNoteUtil(DiContainer container)
        {
            this.IsInstallCustomNote = PluginManager.GetPluginFromId("CustomNotes") != null;
            var loaderType = Type.GetType("CustomNotes.Managers.NoteAssetLoader, CustomNotes");
            this._loader = loaderType == null ? null : container.TryResolve(loaderType);
            var configType = Type.GetType("CustomNotes.PluginConfig, CustomNotes");
            this._customNotesConfig = configType == null ? null : container.TryResolve(configType);
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

        public void SetColor(GameObject noteControllerGO, in Color color)
        {
            if (!this.IsInstallCustomNote) {
                return;
            }
            if (s_customNoteController == null || s_customNoteControllerColorInfo == null) {
                return;
            }
            var instance = noteControllerGO.GetComponent(s_customNoteController);
            if (instance == null) {
                return;
            }
            s_customNoteControllerColorInfo.SetValue(instance, color);
        }
    }
}
