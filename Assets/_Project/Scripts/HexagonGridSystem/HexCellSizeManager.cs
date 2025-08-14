using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public class HexCellSizeManager : MonoBehaviour
    {
        [SerializeField] private MeshFilter groundMeshFilter;
        [SerializeField] private MeshFilter fenceMeshFilter;
        [SerializeField] private Transform groundTransform;
        [SerializeField] private Transform environmentTransform;
        [SerializeField] private Transform worldCanvasTransform;

        public void Initialize()
        {
            var mesh = groundMeshFilter.sharedMesh;
            var meshSize = mesh.bounds.size;
            var scaleX = HexUtility.GetInnerRadius() * 2f / meshSize.x;
            var scaleZ = HexGridSettings.OuterRadius * 2f / meshSize.z;
            groundTransform.localScale = new Vector3(scaleX, 3f, scaleZ);
            //
            var minScale = scaleX <= scaleZ ? scaleX : scaleZ;
            var hexComponentSize = Vector3.one * minScale;
            environmentTransform.localScale = hexComponentSize;
            var fenceTransform = fenceMeshFilter.transform;
            fenceTransform.localScale = hexComponentSize;
            worldCanvasTransform.localScale = hexComponentSize;
            //
            var fenceHeight = fenceMeshFilter.sharedMesh.bounds.size.y / 2f * minScale;
            fenceTransform.localPosition = Vector3.up * fenceHeight;
        }
    }
}