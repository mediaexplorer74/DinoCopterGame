// GameManager.Game1 (DinoCopterGame)


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SharpDX.DirectWrite;
using Windows.UI.Xaml.Controls;

namespace GameManager
{
    //  here is the next tutorial about making some controls to use in an xna game,
    //  this one is about making a button. Our button class will handle the events of being
    //  up, down, and clicked. We will have a enum to hold the current value of the button.
    //  And also will handle input in a method to get information from the touchpanel.
    public class Button : Control
    {
        private Vector2 position;
        private string text;
        private string text2;
        private int n1;
        private int n2;

        Texture2D texture;
        Texture2D touchOverlay;
        Rectangle bounds;
        ButtonStatus Status = ButtonStatus.Normal;
        private bool Enabled;
        internal bool isClicked;


        // Gets fired when the button is clicked or down

        public event EventHandler Clicked;

        public event EventHandler Down;

        public new Vector2 Position

        {

            get 
            {
                //return base.Position; 
                return default;
            }

            set

            {

                //base.Position = value;

                bounds = new Rectangle(10, 10, texture.Width, texture.Height);

            }

        }

        //            Texture2D texture, Texture2D touchedOverlay, Vector2 position, string text
        public Button(Texture2D texture, Vector2 position, string text, string text2, int n1, int n2)
        {
            this.texture = texture;
            this.position = position;
            this.text = text;
            this.text2 = text2;
            this.n1 = n1;
            this.n2 = n2;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

    
/*
    public override void UpdateInput()//(InputState input)

    {

        if (Enabled)

        {

            foreach (TouchLocation tl in input.TouchState)

            {

                if (ContainsPos(tl.Position))

                {

                    if (tl.State == TouchLocationState.Pressed)

                    {

                        Status = ButtonStatus.Clicked;

                        if (Clicked != null)

                        {

                            // Fire the clicked event.        

                            Clicked(this, EventArgs.Empty);

                        }

                    }

                    else

                    {

                        Status = ButtonStatus.Down;

                        if (Down != null)

                        {

                            // Fire the pressed down event.        

                            Down(this, EventArgs.Empty);

                        }

                    }

                }

            }

        }

    }
*/

    protected bool ContainsPos(Vector2 pos)
    {

        return bounds.Contains((int)pos.X, (int)pos.Y);

    }

       public void Draw(SpriteBatch spriteBatch)
       {
            if (true)//(Enabled)
            {

                spriteBatch.Draw(texture, new Vector2(this.position.X, this.position.Y), 
                    bounds, Color.GreenYellow);//, Color);

                if (Status == ButtonStatus.Down)
                {
                    spriteBatch.Draw(/*touchOverlay*/texture, new Vector2(this.position.X, this.position.Y), 
                        bounds, Color.AliceBlue);//, Color);
                }

                //if (Font != null)
                {

                    //Helper.DrawCenteredText(spriteBatch, Font, bounds, Text, Color);

                }

                Status = ButtonStatus.Up;
                isClicked = false;

            }
        }

        internal void Update(ContentManager content, MouseState state2)
        {
            //throw new NotImplementedException();

            if (state2.LeftButton == ButtonState.Pressed)
            {
                Status = ButtonStatus.Down;
                isClicked = true;
            }
        }
    }

    public enum ButtonStatus
    {
        Up, 
        Down, 
        Clicked,
        Normal
    }
}