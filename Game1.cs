using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic; // giver en liste der kan vokse og skrumpe

namespace BoldeILuften;

public class Game1 : Game
{
    // tegnes og skaleres i denne størelse og skaleres derop.
    public const int GameWidth = 320;
    public const int GameHeight = 180;
    private const int Scale = 4; // hvor mnage gange det forstørres på skærmen 
    private const float BounceSpeed = 180f; // det her er hvor hårdt bolten bliver sendt op, når den rammer spilleren 

    private const float MaxSideSpeed = 100f; // det her er hvor hurtigt bolden flyvr til siden efter et hit
    private Texture2D _pixel; // bruges til firkanterne 
    private Player _player; // vores spiller
    private List<Ball> _balls = new List<Ball>(); // alle boldene i spillet som starter som en tom liste
    private BallSpawner _spawner = new BallSpawner(); // sender nye bolde ind med tiden

    private int _lives = 3; // antal liv 
    private int _score; // antal point
    private GraphicsDeviceManager _graphics; // styrer vinduet og grafikkortet
    private SpriteBatch _spriteBatch; // tegner det 2D billeder på skærmen 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this); // åbner vinduet for spillet
        _graphics.PreferredBackBufferWidth = GameWidth * Scale; // 1200 - bredden 
        _graphics.PreferredBackBufferHeight = GameWidth * Scale; // 720 - højden 
        Content.RootDirectory = "Content"; // lyden ligger i content mappen 
        IsMouseVisible = true;
    }

    private void SpawnBall()
    {
        _balls.Add(BallSpawner.CreateBall()); // ved hvordan en bold laves
    }

    protected override void Initialize()
    {
        _player = new Player(new Vector2(GameWidth / 2 - Player.Width / 2, 140));

        SpawnBall(); // opretter den første bold
        base.Initialize(); // kører egen opsætning og derfra kalder også LoadContent
    }

    protected override void LoadContent() // køre den kun en gang og indlæser grafikken, lyden og fonten 
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); // tegneværktøjet 
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) // spillet lukker hvis man trykker 'back' på controlleren eller 'escape'
            Exit();

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // tiden fra den sidste frame i sekunder 
        KeyboardState keys = Keyboard.GetState();
        _player.Update(dt, keys); 
        _spawner.Update(dt, _balls); // tilføjer en ny bold hvis der er tid

        foreach (Ball ball in _balls) // her går den igennem listen en af gangen
        {
            ball.Update(dt);

            if (ball.Bounds.Intersects(_player.Bounds) && ball.Velocity.Y > 0)
            {
                ball.Velocity.Y = -BounceSpeed;
                _score++; // 1 point for hvert hit

                float ballcenter = ball.Position.X + Ball.Size / 2f;
                float playerCenter = _player.Position.X + Player.Width / 2f;
                float offset = (ballcenter - playerCenter) / (Player.Width / 2f);

                ball.Velocity.X = offset * MaxSideSpeed;
            }
        }

        int missed = _balls.RemoveAll(b => b.Position.Y > GameHeight); // fjerne alle boldene, der faldet ud af bunden og trækker et liv pr. bold
        _lives -= missed;

        if (_balls.Count == 0)
        SpawnBall();

        if (_lives <= 0)
        Exit();

        Window.Title = $"Point: {_score} Liv: {_lives}";

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) // her tegnes, ikke spil logik
    {
        GraphicsDevice.Clear(new Color(20, 24, 46));

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp,
        transformMatrix: Matrix.CreateScale(Scale));

        _player.Draw(_spriteBatch, _pixel); // spilleren tegner sig selv

        foreach (Ball ball in _balls)
        ball.Draw(_spriteBatch, _pixel);

        _spriteBatch.End(); // afslutter og sender det til skærmen 

        base.Draw(gameTime); // køres egen
    }
}
