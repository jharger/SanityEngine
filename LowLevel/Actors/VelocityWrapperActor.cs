﻿//
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

namespace SanityEngine.Actors
{
    class VelocityWrapperActor : Actor
    {
        Actor actor;

        public Vector3 Velocity
        {
            get { return actor.Velocity; }
        }

        public Vector3 Position
        {
            get { return actor.Position + actor.Velocity; }
        }

        public Vector3 Facing
        {
            get { return actor.Facing; }
        }

        public float MaxForce
        {
            get { return actor.MaxForce; }
        }

        public VelocityWrapperActor(Actor actor)
        {
            this.actor = actor;
        }

        public void SetProperty(string name, string value)
        {
            actor.SetProperty(name, value);
        }

        public void SetBoolProperty(string name)
        {
            actor.SetBoolProperty(name);
        }

        public void ClearBoolProperty(string name)
        {
            actor.ClearBoolProperty(name);
        }

        public string GetProperty(string name)
        {
            return actor.GetProperty(name);
        }

        public bool HasBoolProperty(string name)
        {
            return actor.HasBoolProperty(name);
        }
    }
}
