using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ParticalProject
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ParticleGenerator : Microsoft.Xna.Framework.GameComponent
    {
        #region Private variables
        private SpriteBatch _sprBtch;
        private Texture2D _t2D;
        private Random _rand;
        private Vector2 _emiter;
        private List<OBJ> _BUCKET;
        private int _intensity;
        private int _density;
        private int _count;
        private Vector2 _drift;
        private Vector2 _wind;
        private Color _color;
        #endregion

        /// <summary> Spawn position of particals </summary>
        /// <example> new Vector2(x, y)</example>
        public Vector2 emiter { get { return _emiter; } set { _emiter = value; } }
        /// <summary> Effects the speed of the particals </summary>
        public int intensity { get { return _intensity; } set { _intensity = value; } }
        /// <summary> Sets the spawn rate of particals </summary>
        public int density { get { return _density; } set { _density = value; } }
        /// <summary> Cap the amount of particals this generate can have at one time </summary>
        public int count { get { return _count; } set { _count = value; } }
        /// <summary> Set the drift vector of the partical </summary>
        public Vector2 drift { get { return _drift; } set { _drift = value; } }
        /// <summary> Use for enviromental infuences </summary>
        public Vector2 wind { get { return _wind; } set { _wind = value; } }
        /// <summary> Set the partical color </summary>
        public Color color { get { return _color; } set { _color = value; } }


        /// <summary> !!!NOTE!!! Must be instantiated in Game1.LoadContent or equivelent. </summary>
        /// <param name="game"> Game1 or this</param>
        /// <param name="spriteBatch"> used by the Draw function</param>
        /// <param name="texture"> Pass it a loaded texture to ellimenate the need to pass a ContentManager</param>
        /// <example> ParticalGenerator(this, spriteBatch, Content.Load[texture2D]("filename"))</example>
        public ParticleGenerator(Game game, SpriteBatch spriteBatch, Texture2D texture)
            : base(game)
        {
            // TODO: Construct any child components here
            _color = Color.White;
            _sprBtch = spriteBatch;
            _t2D = texture;
            _emiter = Vector2.Zero;
            _rand = new Random();
            _BUCKET = new List<OBJ>();
            _density = 3;
            _count = 100;
            _intensity = 5;
            _wind = Vector2.Zero;
            _drift = Vector2.Zero;
        }

        public void Update(GameTime gameTime, ParticalEffect Effect)
        {
            // Default Effect
            if (Effect.effect == 0)
            {
                for (int i = 0; i < _density; i++)
                {
                    int x = _rand.Next(-1000, 1000);
                    int y = _rand.Next(-1000, 1000);
                    Vector2 t_Vel = new Vector2(x, y);

                    _BUCKET.Add(new OBJ(_emiter, t_Vel));
                }
            }

            // Rain Effect
            if (Effect.effect == 1)
            {
                int ceiling;
                ceiling = Game.GraphicsDevice.Viewport.Width;
                int offset = 0;
                if (_wind != Vector2.Zero)
                {
                    offset = Math.Abs((int)_wind.X) * Game.GraphicsDevice.Viewport.Height;
                    offset = offset / 2;
                }
                _count = 10000;
                for (int i = 0; i < _density; i++)
                {
                    _emiter = new Vector2(_rand.Next(-offset, ceiling + offset), -10);
                    int x = _rand.Next(-10, 10);
                    int y = _rand.Next(100, 1000);
                    Vector2 t_Vel = new Vector2(x, y);
                    _BUCKET.Add(new OBJ(_emiter, t_Vel));

                }
            }

            // WaterFall
            if (Effect.effect == 2)
            {
                Vector2 left = new Vector2(2000, -50);
                int length = 500;
                for (int j = 0; j < length; j += 10)
                {
                    float z = j;
                    _emiter = new Vector2(z += left.X, left.Y);
                    for (int i = 0; i < _density; i++)
                    {
                        int x = _rand.Next(-50, 50);
                        int y = _rand.Next(10, 1000);
                        Vector2 t_Vel = new Vector2(x, y);
                        _BUCKET.Add(new OBJ(_emiter, t_Vel));

                    }
                }
            }

            // Fog
            if (Effect.effect == 3)
            {
                Viewport viewport = Game.GraphicsDevice.Viewport;

                for (int i = _count; i >= _count; i--)
                {
                    int x = _rand.Next(viewport.X, viewport.Width);
                    int y = _rand.Next(viewport.Y, viewport.Height);
                    //Vector2 t_Vel = new Vector2(x, y);
                    _emiter = new Vector2(x, y);
                    Vector2 t_Vel = Vector2.Zero;
                    _BUCKET.Add(new OBJ(_emiter, t_Vel));

                }


            }

            foreach (OBJ obj in _BUCKET)
            {
                //obj.vel += new Vector2(0, 1);
                //obj.vel += _drift;
                //obj.pos += _wind;
                //obj.pos += Vector2.Normalize(obj.vel) * _intensity;
            }

            if (_BUCKET.Count > _count)
            {
                int tCount = _BUCKET.Count - _count;
                _BUCKET.RemoveRange(0,tCount);
            }

            
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (OBJ obj in _BUCKET)
                _sprBtch.Draw(_t2D, obj.pos, _color);
        }

        public void DrawIntoCamera(Camera cam)
        {
            _sprBtch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null,
                cam.GetTransformation());

            foreach (OBJ obj in _BUCKET)
                _sprBtch.Draw(_t2D, obj.pos, _color);

            _sprBtch.End();

        }


        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        private class OBJ
        {
            public Vector2 pos;
            public Vector2 vel;
            public OBJ()
            {
                pos = Vector2.Zero;
                vel = Vector2.Zero;
            }
            public OBJ(Vector2 position)
            {
                pos = position;
                vel = Vector2.Zero;
            }
            public OBJ(Vector2 position, Vector2 velocity)
            {
                pos = position;
                vel = velocity;
            }
        }

    }

    public class ParticalEffect
    {
        public int effect;
        private ParticalEffect() { effect = 0; }
        private ParticalEffect(int e) { effect = e; }
        public static ParticalEffect Default { get { return new ParticalEffect(0); } }
        public static ParticalEffect Rain { get { return new ParticalEffect(1); } }
        public static ParticalEffect WaterFall { get { return new ParticalEffect(2); } }
        public static ParticalEffect Fog { get { return new ParticalEffect(3); } }
    }

}
