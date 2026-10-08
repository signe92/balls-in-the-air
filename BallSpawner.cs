using System;
using System.Collections.Generic; // list
using Microsoft.Xna.Framework; // vector2

namespace BoldeILuften;

public class BallSpawner // klassen har ansvaret for hvornår og hvordan nye bolde kommer ind i spillet
{
    private const float StartInterval = 10f; // sekunder mellem bolde i starten
    private const float minInterval = 4f; // tempoet spillet kan nå
    private const float IntervalStep = 0.5f; // ventetiden for hver ny bold

    private float _timer; // tæller sekunder siden den sidste bold
    private float _interval = StartInterval; // nuværende ventetid mellem boldene

    public void Update(float dt, List<Ball> balls) // kalder hver frame fra Game1 og tilføjer en ny bold når tiden er gået
    {
        _timer += dt; // lægger tiden siden sidste frame til

        if (_timer >= _interval)
        {
            _timer = 0f; // starter forfra med at tælle
            balls.Add(CreateBall()); // sender en ny bold ind

            _interval = MathF.Max(minInterval, _interval - IntervalStep);
        }
    }

    public static Ball CreateBall()
    {
        int x = Random.Shared.Next(0, Game1.GameWidth - Ball.Size);
        BallType[] types = Enum.GetValues<BallType>();
        BallType type = types[Random.Shared.Next(types.Length)];
        return new Ball(new Vector2(x, 20), new Vector2(40, 0), type);
    }
}