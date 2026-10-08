using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoldeILuften;

public class Ball // klassen her har ansvaret for en bold
{
    public const int Size = 16;
    private const float Gravity = 200f;

    public Vector2 Position; // hvor bolden er positioned 
    public Vector2 Velocity; // boldens fart

    public Ball(Vector2 startPosition, Vector2 startVelocity) // opretter bolden med en startposition og startfart
    {
        Position = startPosition;
        Velocity = startVelocity;
    }

    public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Size, Size);

    public void Update(float dt)
    {
        Velocity.Y += Gravity * dt;

        Position += Velocity * dt;

        if (Position.X < 0)
        {
            Position.X = 0;
            Velocity.X = -Velocity.X;
        }
        else if (Position.X > Game1.GameWidth - Size)
        {
            Position.X = Game1.GameWidth - Size;
            Velocity.X = -Velocity.X;
        }
        if (Position.Y < 0)
        {
            Position.Y = 0;
            Velocity.Y = -Velocity.Y;
        }
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D pixel) // tegner bolden (ændres senere)
    {
        spriteBatch.Draw(pixel, Bounds, Color.Orange);
    }
}