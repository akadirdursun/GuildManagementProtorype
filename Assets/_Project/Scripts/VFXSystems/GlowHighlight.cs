using AdventurerVillage.Utilities;
using UnityEngine;

namespace AdventurerVillage.VFXSystems
{
    public class GlowHighlight : MonoBehaviour
    {
        [SerializeField, ReadOnly] private MeshRenderer[] renderers;

        [ContextMenu("Refresh Renderer List")]
        private void RefreshMeshRendererList()
        {
            renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        }

        [ContextMenu("Enable Glow")]
        public void EnableGlow()
        {
            foreach (var meshRenderer in renderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    if (!material.HasInt("_GlowActive")) continue;
                    material.SetInt("_GlowActive", 1);
                }
            }
        }

        [ContextMenu("Disable Glow")]
        public void DisableGlow()
        {
            foreach (var meshRenderer in renderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    if (!material.HasInt("_GlowActive")) continue;
                    material.SetInt("_GlowActive", 0);
                }
            }
        }
    }
}