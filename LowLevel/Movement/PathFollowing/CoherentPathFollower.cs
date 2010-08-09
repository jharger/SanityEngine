//
// Copyright (C) 2010 The Sanity Engine Development Team
//
// This source code is licensed under the terms of the
// MIT License.
//
// For more information, see the file LICENSE

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using SanityEngine.Structure.Path;
using SanityEngine.Structure.Graph;
using SanityEngine.Structure.Graph.Spatial;

namespace SanityEngine.Movement.PathFollowing
{
    /// <summary>
    /// A coherent path follower. This path follower ensures that the actor
    /// does not backtrack on the path, nor seek too far forward.
    /// </summary>
    /// <typeparam name="TID">The node ID type.</typeparam>
    public class CoherentPathFollower<TNode, TEdge>
        where TNode : SpatialNode<TNode, TEdge>
        where TEdge : Edge<TNode, TEdge>
    {
        Path<TNode, TEdge> path;
        float epsilon = 0.001f;
		float totalLength = 0.0f;
		float previousParam = 0.0f;
		float lookAhead = 2f;
		
        /// <summary>
        /// The maximum distance to follow along the path, in world units.
        /// </summary>
		public float LookAhead
		{
			get { return lookAhead; }
			set { lookAhead = value; }
		}

        /// <summary>
        /// The underlying path being followed.
        /// </summary>
        public Path<TNode, TEdge> Path
        {
            set
            {
                path = value;
                previousParam = 0.0f;
				totalLength = 0.0f;
				for(int i = 1;path != null && i < path.StepCount; i ++) {
                    TEdge edge = path.GetStep(i);
					Vector3 pos1 = edge.Source.Position;
                    Vector3 pos2 = edge.Target.Position;
                    totalLength += (pos2 - pos1).magnitude;
				}
            }
        }
		
        /// <summary>
        /// <code>true</code> if this path follower has a valid path.
        /// </summary>
		public bool Valid
		{
			get { return path != null; }
		}

        /// <summary>
        /// Create a path follower.
        /// </summary>
        public CoherentPathFollower()
        {
        }

        /// <summary>
        /// Reset the path follower to the beginning of the path.
        /// </summary>
        public void Reset()
        {
            previousParam = 0.0f;
        }

        /// <summary>
        /// Get the next parameter based on the agent's position.
        /// </summary>
        /// <param name="pos">The agent's position.</param>
        /// <returns>The next target parameter.</returns>
        /// <seealso cref="GetPosition"/>
        public float GetNextParameter(Vector3 pos)
        {
            float min = previousParam + epsilon;
			float max = min + lookAhead;

            float minDist = float.PositiveInfinity;
            float nearestParam = min;

			float totalDist = 0.0f;
            for (int i = 0; i < path.StepCount; i++)
            {
                TEdge edge = path.GetStep(i);
                Vector3 s = edge.Source.Position;
                Vector3 e = edge.Target.Position;
                Vector3 seg = e - s;
				float mag = seg.magnitude;
                Vector3 dir = pos - s;
                float proj = Vector3.Dot(seg, dir) / (mag * mag);
				float param = totalDist + mag * proj;
				totalDist += mag;
				
				//param = Math.Max(min, Math.Min(max, param));
				if(param < min || param > max) {
					continue;
				}

                Vector3 pt = Vector3.Lerp(s, e, proj);
                Vector3 vec = pos - (s + pt);
                float dist = vec.magnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestParam = param;
                }
            }
			
			previousParam = nearestParam;
            return nearestParam;
        }

        /// <summary>
        /// Get a world position based on the given parameter. This parameter
        /// is normally calculated using GetNextParameter().
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>The world position.</returns>
        /// <seealso cref="GetNextParameter"/>
        public Vector3 GetPosition(float param)
        {
			if(param <= 0.0f) {
				return path.GetStep(0).Source.Position;
			}
			if(param >= totalLength) {
				return path.GetStep(path.StepCount - 1).Target.Position;
			}
			
			float totalDist = 0.0f;
            for (int i = 0; i < path.StepCount; i++)
            {
                TEdge edge = path.GetStep(i);
                Vector3 s = edge.Source.Position;
                Vector3 e = edge.Target.Position;
                Vector3 seg = e - s;
				float mag = seg.magnitude;
				if(param >= totalDist && param <= totalDist + mag) {
					float segParam = (param - totalDist) / mag;
					Vector3 v = Vector3.Lerp(s, e, segParam);
					return v;
				}
				
				totalDist += mag;
            }
            return Vector3.zero;
        }
    }
}