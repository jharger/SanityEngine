using UnityEngine;

namespace SanityEngine.Structure.Graph.NavigationGraph
{
    /// <summary>
    /// Specialized version of the graph interface for graphs with nodes
    /// representing points in space.
    /// </summary>
    public interface NavigationGraph : Graph
    {
        /// <summary>
        /// Find the node containing the point.
        /// </summary>
        NavigationGraphNode Quantize(Vector3 pos);
    }
}
