using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("UI/Effects/Normal Gradient Color")]
[RequireComponent(typeof(Text))]
public class NormalGradient : BaseMeshEffect
{
    public Color bottomColor = Color.black;
    public Color topColor = Color.white;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
        {
            return;
        }
        var vertexList = new List<UIVertex>();
        vh.GetUIVertexStream(vertexList);
        var count = vertexList.Count;
        ApplyGradient(vertexList, 0, count);
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }

    private void ApplyGradient(List<UIVertex> vertxList, int start, int end)
    {
        if (vertxList.Count <= 0)
        {
            return;
        }
        var bottomY = vertxList[0].position.y;
        var topY = vertxList[0].position.y;
        for (var i = start; i < end; i++)
        {
            var y = vertxList[i].position.y;
            if (y > topY)
            {
                topY = y;
            }
            else if (y < bottomY)
            {
                bottomY = y;
            }
        }
        var uiElementHight = topY - bottomY;
        for (var i = start; i < end; i++)
        {
            var uiVertex = vertxList[i];
            uiVertex.color = Color.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY)/uiElementHight);
            vertxList[i] = uiVertex;
        }
    }
}