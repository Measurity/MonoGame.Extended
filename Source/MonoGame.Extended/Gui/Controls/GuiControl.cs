﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.InputListeners;

namespace MonoGame.Extended.Gui.Controls
{
    public abstract class GuiControl : IUpdate
    {
        protected GuiControl()
        {
            IsHovered = false;
        }

        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMoved;

        protected abstract IGuiControlTemplate GetCurrentTemplate();
        public virtual void Update(GameTime gameTime) { }

        public virtual void LayoutChildren(Rectangle boundingRectangle)
        {
        }
        
        public virtual void OnMouseMoved(object sender, MouseEventArgs args)
        {
            MouseMoved.Raise(this, args);
        }

        public virtual void OnMouseDown(object sender, MouseEventArgs args)
        {
            MouseDown.Raise(this, args);
        }

        public virtual void OnMouseUp(object sender, MouseEventArgs args)
        {
            MouseUp.Raise(this, args);
        }

        public GuiHorizontalAlignment HorizontalAlignment { get; set; }
        public GuiVerticalAlignment VerticalAlignment { get; set; }

        public Point Location { get; set; }
        public Size Size { get; set; }
        public bool IsHovered { get; private set; }
        public int Left => Location.X;
        public int Top => Location.Y;
        public int Right => Location.X + Width;
        public int Bottom => Location.Y + Height;
        public int Width => DesiredSize.Width;
        public int Height => DesiredSize.Height;
        public Vector2 Center => new Vector2(Location.X + Width*0.5f, Location.Y + Height*0.5f);
        public Rectangle Margin { get; set; }
        public Rectangle BoundingRectangle => new Rectangle(Location, Size);
        
        public virtual Size DesiredSize
        {
            get
            {
                var currentDrawable = GetCurrentTemplate();
                return currentDrawable.CalculateDesiredSize(this);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            //var actualWidth = DesiredSize.Width > rectangle.Width ? rectangle.Width : DesiredSize.Width;
            //var actualHeight = DesiredSize.Height > rectangle.Height ? rectangle.Height : DesiredSize.Height;
            var currentDrawable = GetCurrentTemplate();

            //ActualSize = new Size(actualWidth, actualHeight);
            currentDrawable.Draw(spriteBatch, this);
        }
        
        public bool Contains(Vector2 point)
        {
            return BoundingRectangle.Contains(point);
        }

        public bool Contains(Point point)
        {
            return BoundingRectangle.Contains(point.ToVector2());
        }

        public bool Contains(int x, int y)
        {
            return BoundingRectangle.Contains(x, y);
        }

        public virtual void OnMouseEnter(object sender, MouseEventArgs args)
        {
            IsHovered = true;
        }

        public virtual void OnMouseLeave(object sender, MouseEventArgs args)
        {
            IsHovered = false;
        }
    }
}