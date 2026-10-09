using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoldeILuften;

public class Ball // klassen her har ansvaret for en bold
{
    public const int Size = 16;
    private const float Gravity = 200f;

    public Vector2 Position; // hvor bolden er positioned 
    public Vector2 Velocity; // boldens fart
    public BallType Type; // hvilke en af de bolde det er

    public Ball(Vector2 startPosition, Vector2 startVelocity, BallType type) // opretter bolden med en startposition og startfart
    {
        Position = startPosition;
        Velocity = startVelocity;
        Type = type;
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
    private Color GetColor() => Type switch
    {
        BallType.Eksamen => Color.Red,
        BallType.Venner => Color.CornflowerBlue,
        BallType.Soevn => Color.Orange,
        BallType.Arbejde => Color.MediumPurple,
        BallType.Foedselsdage => Color.Gold,
        _ => Color.White // betyder alt andet
    };
    public void Draw(SpriteBatch spriteBatch, Texture2D pixel) // tegner bolden (ændres senere)
    {
        spriteBatch.Draw(pixel, Bounds, GetColor());
    }

    private string GetLetter() => Type switch
    {
        BallType.Eksamen => "E",
        BallType.Arbejde => "A",
        BallType.Foedselsdage => "F",
        BallType.Soevn => "Z",
        BallType.Venner => "V",
        _ => "?"
    };
}