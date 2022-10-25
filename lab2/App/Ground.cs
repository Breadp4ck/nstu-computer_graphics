using Lab1.Resources;
using Lab1.Main.Scene3D;

namespace Lab1.App
{
    public class Ground : MeshInstance3D
    {
        private StandartMaterialResource _material = new StandartMaterialResource();
        private MeshInstance3D _base = new MeshInstance3D("Основа");
        private MeshInstance3D _hill1 = new MeshInstance3D("Возвышенность 1");
        private MeshInstance3D _hill2 = new MeshInstance3D("Возвышенность 2");
        private RoadBlock[] _road = new RoadBlock[20];

        public class RoadBlock : VisualInstance3D
        {
            private MeshInstance3D _block = new MeshInstance3D("Дорожный блок");
            private MeshInstance3D _stripe = new MeshInstance3D("Разделительная полоса 1");

            public RoadBlock(string name) : base(name)
            {
                _block.MeshData = new CubePrimitive();
                _block.Transform.Scale = new System.Numerics.Vector3(2.0f, 0.1f, 3.0f);
                ((StandartMaterialResource)_block.MaterialResource).Diffuse = new Core.Color(0.1f, 0.1f, 0.1f);

                _stripe.MeshData = new CubePrimitive();
                _stripe.Transform.Scale = new System.Numerics.Vector3(1.4f, 0.01f, 0.05f);
                _stripe.Translate(0.0f, 0.1f, 0.05f);

                AddChild(_block);
                AddChild(_stripe);
            }
        }

        public Ground(string name) : base(name)
        {
            _material.Diffuse = new Core.Color(0.5f, 0.9f, 0.3f);
            _material.Specular = new Core.Color(0.1f, 0.1f, 0.1f);

            _base.MeshData = new CubePrimitive();
            _base.MaterialResource = _material;
            _base.Transform.Scale = new System.Numerics.Vector3(40.0f, 0.5f, 30.0f);

            _hill1.MeshData = new CubePrimitive();
            _hill1.MaterialResource = _material;
            _hill1.Transform.Scale = new System.Numerics.Vector3(15.0f, 0.2f, 15.0f);
            _hill1.Translate(-20.0f, 0.5f, 15.0f);

            _hill2.MeshData = new CubePrimitive();
            _hill2.MaterialResource = _material;
            _hill2.Transform.Scale = new System.Numerics.Vector3(10.0f, 0.2f, 11.0f);
            _hill2.Translate(-22.0f, 0.7f, 17.0f);

            for (int idx = 0; idx < 20; idx++)
            {
                _road[idx] = new RoadBlock($"Дорожный блок {idx}");
                _road[idx].Translate(-38.0f + 4.0f * idx, 0.5f, -14.0f);
                AddChild(_road[idx]);
            }

            AddChild(_base);
            AddChild(_hill1);
            AddChild(_hill2);
        }
    }
}