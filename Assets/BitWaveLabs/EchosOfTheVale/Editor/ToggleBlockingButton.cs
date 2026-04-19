#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BitWaveLabs.EchosOfTheVale.Editor
{
    [Overlay(typeof(SceneView), "Blocking Visibility")]
    public class BlockingVisibilityOverlay : ToolbarOverlay
    {
        public BlockingVisibilityOverlay() : base(ToggleBlockingButton.ID)
        {
        }
    }

    [EditorToolbarElement(ID, typeof(SceneView))]
    public class ToggleBlockingButton : EditorToolbarButton
    {
        public const string ID = "BitWaveLabs/ToggleBlockingButton";

        private static ToggleBlockingButton _instance;

        public ToggleBlockingButton()
        {
            clicked += ToggleBlockingRenderer;

            _instance = this;
            EditorApplication.update += UpdateState;

            RefreshButton();
        }

        private static void ToggleBlockingRenderer()
        {
            TilemapRenderer blockingRenderer = FindBlockingRenderer();

            if (blockingRenderer == null)
            {
                Debug.LogWarning("Could not find a TilemapRenderer on a GameObject named 'Blocking'.");
                return;
            }

            Undo.RecordObject(blockingRenderer, "Toggle Blocking Tilemap Renderer");
            blockingRenderer.enabled = !blockingRenderer.enabled;
            EditorUtility.SetDirty(blockingRenderer);

            RefreshButton();
        }

        private static void UpdateState()
        {
            if (_instance == null)
                return;

            RefreshButton();
        }

        private static void RefreshButton()
        {
            if (_instance == null)
                return;

            TilemapRenderer blockingRenderer = FindBlockingRenderer();

            if (blockingRenderer == null)
            {
                _instance.text = "🚫 Blocking ?";
                _instance.tooltip = "Could not find a TilemapRenderer on a GameObject named 'Blocking'.";
                _instance.SetEnabled(false);
                return;
            }

            bool isVisible = blockingRenderer.enabled;

            _instance.text = isVisible ? "🟥 Blocking On" : "⬜ Blocking Off";
            _instance.tooltip = isVisible
                ? "Hide the Blocking TilemapRenderer."
                : "Show the Blocking TilemapRenderer.";

            _instance.SetEnabled(true);
        }

        private static TilemapRenderer FindBlockingRenderer()
        {
            GameObject blockingObject = GameObject.Find("Blocking");
            if (blockingObject == null)
                return null;

            return blockingObject.GetComponent<TilemapRenderer>();
        }
    }
}
#endif