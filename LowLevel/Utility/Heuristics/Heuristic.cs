using SanityEngine.Structure.Graph;

namespace SanityEngine.Utility.Heuristics
{
	public interface Heuristic {
		float Heuristic(Node n1, Node n2);
	}
}
