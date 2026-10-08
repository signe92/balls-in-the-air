using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoldeILuften; // bruger den samme som fra Game1
public class Player // her holder klassen alt ansvaret om spilleren
{
    public const int Width = 32;
    public const int height = 32;
    private const float Speed = 120f; // hvor hurtigt det kører pr sekund i pixels 

    public Vector2 Positions;

    public Player(Vector2 startPosition) // konstruktøren kører når der bliver skrevet 'new player'
    {
        Positions = startPosition;
    }

    public Rectangle Bounds => new Rectangle((int)Positions.X, (int)Positions.Y, Width, height);
    // bounds er den rektangel for spilleren (positionen og størrelsen)

    public void Update(float dt, KeyboardState keys) 
    {
        if (keys.IsKeyDown(Keys.Left) || keys.IsKeyDown(Keys.A))
        Positions.X -= Speed * dt;

        if (keys.IsKeyDown(Keys.Right) || keys.IsKeyDown(Keys.A))
        Positions.X += Speed * dt;

        Positions.X = MathHelper.Clamp(Positions.X, 0, Game1.GameWidth - Width);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel) // kalder fra Game1.draw som tegner spilleren som en hvid firkant
    {
        spriteBatch.Draw(pixel, Bounds, Color.White);
    }
}