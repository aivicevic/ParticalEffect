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
    public class BackGround : Microsoft.Xna.Framework.GameComponent
    {
        private List<Texture2D> _GALLARY;
        private List<Vector2> _ANCHORS;
        SpriteBatch spriteBatch;
        

        public BackGround(Game game)
            : base(game)
        {
            _GALLARY = new List<Texture2D>();
            _ANCHORS = new List<Vector2>();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
        }

        public void LoadTexture(Texture2D t2D)
        {
            int width = t2D.Width;
            int count = _ANCHORS.Count;
            Vector2 anchor = new Vector2(width * count, 0);

            _GALLARY.Add(t2D);
            _ANCHORS.Add(anchor);
        }

        public void Draw(GameTime gameTime, Camera cam)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null,
                cam.GetTransformation());
            for (int i = 0; i < _GALLARY.Count; i++ )
            {
                spriteBatch.Draw(_GALLARY[i], _ANCHORS[i], Color.White);
            }
            spriteBatch.End();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
