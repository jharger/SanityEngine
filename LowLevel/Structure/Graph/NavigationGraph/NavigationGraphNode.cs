using System;
using UnityEngine;

namespace SanityEngine.Structure.Graph.NavigationGraph
{
    /// <summary>
    /// A node object in 3D space.
    /// </summary>
    public interface NavigationGraphNode : Node
    {
		Vector3 Position
		{
			get;
		}
	}
}