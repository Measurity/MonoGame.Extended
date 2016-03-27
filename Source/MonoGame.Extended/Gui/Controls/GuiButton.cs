using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Gui.Drawables;
using MonoGame.Extended.InputListeners;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiButton : GuiControl, IGuiTextControl
    {
        private readonly GuiButtonStyle _style;

        public GuiButton(GuiButtonStyle style)
        {
            _style = style;
            IsPressed = false;
            Text = string.Empty;
            TextColor = Color.White;
        }

        public string Text { get; set; }
        public Color TextColor { get; set; }
        public bool IsPressed { get; private set; }

        public event EventHandler<MouseEventArgs> Clicked;

        protected override IGuiControlTemplate GetCurrentTemplate()
        {
            if (IsPressed)
                return _style.Pressed;

            if (IsHovered)
                return _style.Hovered;

            return _style.Normal;
        }

        public override void OnMouseDown(object sender, MouseEventArgs args)
        {
            IsPressed = true;
            base.OnMouseDown(sender, args);
        }

        public override void OnMouseUp(object sender, MouseEventArgs args)
        {
            if(IsPressed)
                Clicked.Raise(this, args);

            IsPressed = false;
            base.OnMouseUp(sender, args);
        }
    }
}