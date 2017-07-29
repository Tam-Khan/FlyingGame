namespace FlyingGame.Model.Shared
{
    public class KeyboardInput
    {
        public int KeyDirectionVertical { get; set; }
        public int KeyDirectionHorizontal { get; set; }
        public bool KeyFire { get; set; }
        public bool KeyBomb { get; set; }
        public bool RestartGame { get; set; }

        public KeyboardInput()
        {
            KeyDirectionHorizontal = 0;
            KeyDirectionVertical = 0;
            KeyFire = false;
            KeyBomb = false;
            RestartGame = false;
        }
    }
}
