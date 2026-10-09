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
    private SpriteFont _font; // Pixel-fonten til teksten
    private Player _player; // vores spiller
    private List<Ball> _balls = new List<Ball>(); // alle boldene i spillet som starter som en tom liste
    private BallSpawner _spawner = new BallSpawner(); // sender nye bolde ind med tiden

    private int _lives = 3; // antal liv 
    private int _score; // antal point
    private HighscoreStore _highscoreStore = new HighscoreStore(); // gemmer og henter highscore
    private int _highscore; // bedste score indtil videre
    private bool _isNewHighscore; // sandt hvis sidste spil slår rekorden
    private GraphicsDeviceManager _graphics; // styrer vinduet og grafikkortet
    private SpriteBatch _spriteBatch; // tegner det 2D billeder på skærmen 
    private GameState _state = GameState.Menu; // spillet starter i menuen
    private KeyboardState _previousKeys; // tastaturet fra sidste frame

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this); // åbner vinduet for spillet
        _graphics.PreferredBackBufferWidth = GameWidth * Scale; // 1280 - bredden 
        _graphics.PreferredBackBufferHeight = GameHeight * Scale; // 720 - højden 
        Content.RootDirectory = "Content"; // lyden ligger i content mappen 
        IsMouseVisible = true;
    }

    private void SpawnBall()
    {
        _balls.Add(BallSpawner.CreateBall()); // ved hvordan en bold laves
    }

    private bool WasPressed(KeyboardState keys, Keys key) // kun sandt i den frame hvor tasten bliver trykket ned ikke mens den bliver holdt nede
    {
        return keys.IsKeyDown(key) && _previousKeys.IsKeyUp(key);
    }

    private void StartNewGame()
    {
        _score = 0;
        _lives = 3;
        _isNewHighscore = false;
        _balls.Clear(); // fjerner alle gamle bolde
        _spawner = new BallSpawner(); // ny spawner så tempoet starter forfra
        _player = new Player(new Vector2(GameWidth / 2 - Player.Width / 2, 140));
        SpawnBall(); // første bold
        _state = GameState.Playing;
    }

    protected override void Initialize()
    {
        _highscore = _highscoreStore.Load(); // henter det sidste highscore
        Window.Title = "Balls in the Air";  
        base.Initialize(); // kører egen opsætning og derfra kalder også LoadContent
    }

    protected override void LoadContent() // køre den kun en gang og indlæser grafikken, lyden og fonten 
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); // tegneværktøjet 
        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
        _font = Content.Load<SpriteFont>("font");

    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keys = Keyboard.GetState();

        if (keys.IsKeyDown(Keys.Escape)) // spillet lukker hvis man trykker 'back' på controlleren eller 'escape'
            Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // tiden fra sidste frame

        switch (_state) // kører den logik der hører til skærmen man er pā
        {
            case GameState.Menu:
            UpdateMenu(keys);
            break;

            case GameState.Playing:
            UpdatePlaying(dt, keys);
            break;

            case GameState.GameOver:
            UpdateGameOver(keys);
            break;
        }

        _previousKeys = keys; // husker tastaturet til næste frame
        base.Update(gameTime);
    }

    private void UpdateMenu(KeyboardState keys)
    {
        if (WasPressed(keys, Keys.Space))
        StartNewGame();
    }

    private void UpdatePlaying(float dt, KeyboardState keys)
    {
        _player.Update(dt, keys);
        _spawner.Update(dt, _balls);

        foreach (Ball ball in _balls)
        {
            ball.Update(dt);

            if (ball.Bounds.Intersects(_player.Bounds) && ball.Velocity.Y > 0)
            {
                
                ball.Velocity.Y = -BounceSpeed;
                _score++;

                float ballCenter = ball.Position.X + Ball.Size / 2f;
                float playerCenter = _player.Position.X + Player.Width / 2f;
                float offset = (ballCenter - playerCenter) / (Player.Width / 2f);
                ball.Velocity.X = offset * MaxSideSpeed;
            }
        }

        int missed = _balls.RemoveAll(b => b.Position.Y > GameHeight);
        _lives -= missed;

        if (_balls.Count == 0)
        SpawnBall();

        if (_lives <= 0) // skifter over til game over
        {
            _state = GameState.GameOver;

            if (_score > _highscore) // gemmer den nye rekord
            {
                _highscore = _score;
                _highscoreStore.Save(_highscore);
                _isNewHighscore = true;
            }
        }

    }

    private void UpdateGameOver(KeyboardState keys)
    {
        if (WasPressed(keys, Keys.Space))
        StartNewGame();
    }

    private void DrawCentered(string text, int y, Color color)
    {
        Vector2 size = _font.MeasureString(text); // bredden og højden på teksten
        int x = (int)((GameWidth - size.X) / 2); // teksten står i midten
        _spriteBatch.DrawString(_font, text, new Vector2(x, y), color);
    }

    private void DrawMenu()
    {
        DrawCentered("BALLS IN THE AIR", 50, Color.Gold);
        DrawCentered("Hold livets bolde i luften", 70, Color.White);
        DrawCentered("Piletaster eller A/D", 100, Color.LightGray);
        DrawCentered("Tryk MELLEMRUM", 125, Color.White);
        DrawCentered($"Highscore: {_highscore}", 155, Color.Gold);
    }

    private void DrawPlaying()
    {
        _player.Draw(_spriteBatch, _pixel);

        foreach (Ball ball in _balls)
        ball.Draw(_spriteBatch, _pixel);

        _spriteBatch.DrawString(_font, $"Point: {_score}", new Vector2(4, 4), Color.White); // laver point i højre side

        for (int i = 0; i < _lives; i++) // laver et lille firkantet hjerte pr liv i højre felt
        {
            int x = GameWidth - 12 - i * 12;
            _spriteBatch.Draw(_pixel, new Rectangle(x, 4, 8, 8), Color.HotPink);
        }

    }

    private void DrawGameOver()
    {
        _spriteBatch.Draw(_pixel, new Rectangle(0, 0, GameWidth, GameHeight), Color.Black * 0.6f);

        DrawCentered("GAME OVER", 55, Color.Red);
        DrawCentered($"Point: {_score}", 80, Color.White);

        if (_isNewHighscore)
        DrawCentered("NY HIGHSCORE!", 100, Color.Gold);

        DrawCentered("Tryk MELLEMRUM for at prøve igen", 130, Color.White);
    }

    protected override void Draw(GameTime gameTime) // her tegnes, ikke spil logik
    {
        GraphicsDevice.Clear(new Color(20, 24, 46));

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(Scale));

        switch (_state)
        {
            case GameState.Menu:
            DrawMenu();
            break;

            case GameState.Playing:
            DrawPlaying();
            break;

            case GameState.GameOver:
            DrawPlaying();
            DrawGameOver();
            break;
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}