using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    private Ball _ball; // vores bold, kun en lige nu

    private int _lives = 3; // antal liv 
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

    private void ResetBall()
    {
        _ball = new Ball(new Vector2(GameWidth / 2 - Ball.Size / 2, 20), new Vector2(40, 0));
    }

    protected override void Initialize()
    {
        _player = new Player(new Vector2(GameWidth / 2 - Player.Width / 2, 140));

        ResetBall(); // opretter den første bold
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

        _ball.Update(dt); // bolden falder

        if (_ball.Bounds.Intersects(_player.Bounds) && _ball.Velocity.Y > 0) // hvis bolden rammer spilleren reagere den kun hvis bolden er på vej ned
        {
            _ball.Velocity.Y = -BounceSpeed; // her sender den bolden op igen (negativ y - opad)

            float ballCenter = _ball.Position.X + Ball.Size / 2f; // finder midten af bolden og midten af spilleren 
            float playerCenter = _player.Position.X + Player.Width / 2f;

            float offset = (ballCenter - playerCenter) / (Player.Width / 2f); // fra spillerens midt punkt, ramte bolden. -1 er venstre, 0 er midten og 1 er højre

            _ball.Velocity.X = offset * MaxSideSpeed; // nu længere ude på siden, nu hurtigere flyver bolden til siden 
        }

        if (_ball.Position.Y > GameHeight) // hvis bolden falder ud af skærmen 
        {
            _lives --; // trækker et af livene 
            ResetBall(); // starter en ny bold

            if (_lives <= 0) // game over midlertidig
            Exit();
        }
        Window.Title = $"Liv: {_lives}"; // viser livene i vinduets titel

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) // her tegnes, ikke spil logik
    {
        GraphicsDevice.Clear(new Color(20, 24, 46));

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp,
        transformMatrix: Matrix.CreateScale(Scale));

        _player.Draw(_spriteBatch, _pixel); // spilleren tegner sig selv

        _ball.Draw(_spriteBatch, _pixel);

        _spriteBatch.End(); // afslutter og sender det til skærmen 

        base.Draw(gameTime); // køres egen
    }
}
