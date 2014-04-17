using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticalProject
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        private float _zoom;
        private Matrix _transform;
        private Vector2 _position;
        private float _rotation;
        private Viewport viewport;

        public Camera(Game game)
            : base(game)
        {
            viewport = Game.GraphicsDevice.Viewport;
            _zoom = 1.0f;
            _rotation = 0.0f;
            _position = new Vector2(viewport.Width / 2, viewport.Height / 2);
        }

        public float zoom
        {
            get { return _zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f;
                if (zoom > 2.0f)
                    zoom = 2.0f;
            }
        }

        public Matrix transform { get { return _transform; } }
        public float rotation { get { return _rotation; } set { _rotation = value; } }
        public void Move(Vector2 amount)
        {
            _position += amount;
        }
        public Vector2 position { get { return _position; } set { _position = value; } }

        public Matrix GetTransformation()
        {
            _transform =
                Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0))
                * Matrix.CreateRotationZ(_rotation)
                * Matrix.CreateScale(new Vector3(_zoom, _zoom, 1))
                * Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return _transform;
        }

    }
}
