﻿using System;
using System.Collections.Generic;
using System.Text;
using AIEngine.Search.PathFinding;
using AIEngine.Structure.Graph;

namespace AIEngine.Structure.Path
{
    /// <summary>
    /// A path through a graph.
    /// </summary>
    /// <typeparam name="TID">The node ID type.</typeparam>
    public class Path<TNode, TEdge>
        where TNode : Node<TNode, TEdge>
        where TEdge : Edge<TNode, TEdge>
    {
        readonly Graph.Graph<TNode, TEdge> graph;
        TEdge[] steps;

        /// <summary>
        /// The graph this path refers to.
        /// </summary>
        public Graph.Graph<TNode, TEdge> Graph
        {
            get { return graph; }
        }

        /// <summary>
        /// The number of steps in this path.
        /// </summary>
        public int StepCount
        {
            get { return steps.Length; }
        }

        /// <summary>
        /// Get the node index for the given step.
        /// </summary>
        /// <param name="step">The 0-based step number</param>
        /// <returns>The node index for the step.</returns>
        public TEdge GetStep(int step)
        {
            return steps[step];
        }

        /// <summary>
        /// Create a path.
        /// </summary>
        /// <param name="graph">The graph this path is through.</param>
        /// <param name="steps">The edges.</param>
        public Path(Graph.Graph<TNode, TEdge> graph, TEdge[] steps)
        {
            this.graph = graph;
            this.steps = steps;
        }

        /// <summary>
        /// String representation of a path for debugging purposes.
        /// </summary>
        /// <returns>A string representation of the path.</returns>
        public override string ToString()
        {
            string str = "[";
            foreach (TEdge step in steps)
            {
                str += " " + step.ToString() + " ";
            }
            return str + "]";
        }
    }
}