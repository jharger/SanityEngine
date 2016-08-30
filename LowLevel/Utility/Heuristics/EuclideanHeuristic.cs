using UnityEngine;
using System.Collections;
using SanityEngine.Utility;
using SanityEngine.Utility.Heuristics;
using SanityEngine.Structure.Graph;
using SanityEngine.Structure.Graph.NavigationGraph;

public class EuclideanHeuristic : Heuristic {
	public float Heuristic(Node n1, Node n2)
	{
        NavigationGraphNode node1 = (NavigationGraphNode)n1;
        NavigationGraphNode node2 = (NavigationGraphNode)n2;
		return DistanceMetrics.Euclidean(node1.Position, node2.Position);
	}
}
