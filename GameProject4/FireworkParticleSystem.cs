﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject4
{
    public class FireworkParticleSystem : ParticleSystem
    {
        public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25) { }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";
            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);
            var accel = -velocity / lifetime;
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var scale = RandomHelper.NextFloat(4, 6);

            p.Initialize(where, velocity, accel, Color.Goldenrod, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);
            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            particle.Scale = 0.1f + 0.25f * normalizedLifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            AddParticles(where);
        }
    }
}
