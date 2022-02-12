using System;

namespace BulletHell
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGame())
                game.Run();
            StageReader stage = new StageReader();
            stage.LoadJSON();
        }
    }
}
