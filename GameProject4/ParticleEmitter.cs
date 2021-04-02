using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProject4
{
    public class ParticleEmitter : IParticleEmitter
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
    }
}
