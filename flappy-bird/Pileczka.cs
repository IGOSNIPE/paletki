using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace flappy_bird
{
    class Pileczka
    {
        Texture2D texture_ball;
        Rectangle sprite_ball;
        Vector2 velocity_ball;
        int window_height, window_width;
        
        // Nie chcemy edytować tekstury z zewnątrz - damy możliwość jej pobrania. Tak samo sprite
        public Texture2D getTexture() { return texture_ball; }
        public Rectangle getSprite() { return sprite_ball; }

        public Pileczka(GraphicsDevice device)
        {
            texture_ball = new Texture2D(device, 100, 100);
            Color[] colorData = new Color[100 * 100];

            for (int i = 0; i < 10000; i++)
                colorData[i] = new Color(Color.Red, 0.5f);
            texture_ball.SetData<Color>(colorData);

            sprite_ball = new Rectangle(new Point(0, 50), new Point(10, 10));
            velocity_ball = new Vector2(2, 2);

            window_height = device.Viewport.Height;
            window_width = device.Viewport.Width;
        }

        // Zwraca -1 gdy przegrana, 1 gdy punkt i 0 gdy nic z tego
        public int Update(int paletkaX, int paletkaSizeX, int paletkaY)
        {
            if (sprite_ball.Y + sprite_ball.Size.Y > window_height || sprite_ball.Y < 0)
                velocity_ball.Y *= -1;
            if (sprite_ball.X + sprite_ball.Size.X > window_width || sprite_ball.X < 0)
                velocity_ball.X *= -1;


            // Albo obiekt typu paletka do klasy - niezalecana opcja,
            // public int Update(int paletkaX, int paletkaY)
            if (sprite_ball.Y <= paletkaY && sprite_ball.X > paletkaX && sprite_ball.X < paletkaX + paletkaSizeX && velocity_ball.Y < 0.0)
            {
                velocity_ball.Y *= -1;
                return 1;
            }
            // Warunek odbicia od paletki: sprawdzamy czy piłka jest odpowiednio wysoko i czy jest w pasie obejmowanym przez paletkę. Polecam rozrysować w razie wątpliwości
            else if (sprite_ball.Y <= 0.0) // Warunek przegranej
            {
                sprite_ball.X += (int)velocity_ball.X;
                sprite_ball.Y += (int)velocity_ball.Y;
                return -1;
            }
            sprite_ball.X += (int)velocity_ball.X;
            sprite_ball.Y += (int)velocity_ball.Y;
            return 0;
        }

        public void SlowDown()
        {
            velocity_ball *= 1 / 1.01f;
        }

        public void SpeedUp()
        {
            if(velocity_ball.Length() < 10f) velocity_ball *= 1.01f;
        }
    }
}
