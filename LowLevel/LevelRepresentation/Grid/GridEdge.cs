﻿//
// Copyright (C) 2010 The Sanity Engine Development Team
//
// This source code is licensed under the terms of the
// MIT License.
//
// For more information, see the file LICENSE

using System;
using System.Collections.Generic;
using SanityEngine.Structure.Graph;

namespace SanityEngine.LevelRepresentation.Grid
{
    public class GridEdge : Edge<GridNode, GridEdge>
    {
        public GridNode Source
        {
            get { return source; }
        }

        public GridNode Target
        {
            get { return target; }
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        GridNode source;
        GridNode target;
        float cost;

        public GridEdge(GridNode source, GridNode target, float cost)
        {
            this.source = source;
            this.target = target;
            this.cost = cost;
        }
    }
}