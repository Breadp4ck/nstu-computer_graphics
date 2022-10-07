using Lab1.Resources;
using Lab1.Main.Scene3D;

namespace Lab1.App
{
    public class Wall : MeshInstance3D
    {
        MeshInstance3D[] _bricks = new MeshInstance3D[9];

        public Wall(string name) : base(name)
        {
            for (int idx = 0; idx < 9; idx++)
            {
                _bricks[idx] = new MeshInstance3D($"Кирпич {idx + 1}");
                _bricks[idx].MeshData = new CubePrimitive();
                _bricks[idx].MaterialResource = new StandartMaterialResource();
            }

            _bricks[0].Translate(2.0f, 0.0f, 0.0f);
            _bricks[1].Translate(2.0f, 2.0f, 0.0f);
            _bricks[2].Translate(0.0f, 2.0f, 0.0f);
            _bricks[3].Translate(-2.0f, 2.0f, 0.0f);
            _bricks[4].Translate(-2.0f, 0.0f, 0.0f);
            _bricks[5].Translate(-2.0f, -2.0f, 0.0f);
            _bricks[6].Translate(0.0f, -2.0f, 0.0f);
            _bricks[7].Translate(2.0f, -2.0f, 0.0f);

            foreach (var brick in _bricks)
            {
                AddChild(brick);
            }
        }
    }
}