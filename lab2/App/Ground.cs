using Lab1.Resources;
using Lab1.Main.Scene3D;
using Lab1.Core;
using System.Numerics;

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
                _block.Transform.Scale = new Vector3(2.0f, 0.1f, 3.0f);
                ((StandartMaterialResource)_block.MaterialResource).Diffuse = new Color(0.1f, 0.1f, 0.1f);
                ((StandartMaterialResource)_block.MaterialResource).Specular = new Color(0.0f, 0.0f, 0.0f);

                _stripe.MeshData = new CubePrimitive();
                _stripe.Transform.Scale = new Vector3(1.4f, 0.01f, 0.05f);
                _stripe.Translate(0.0f, 0.1f, 0.05f);

                AddChild(_block);
                AddChild(_stripe);
            }
        }

        public Ground(string name) : base(name)
        {
            _material.Diffuse = new Color(0.5f, 0.9f, 0.3f);
            _material.Specular = new Color(0.1f, 0.1f, 0.1f);

            _base.MeshData = new CubePrimitive();
            _base.MaterialResource = _material;
            _base.Transform.Scale = new Vector3(16.0f, 0.5f, 16.0f);

            _hill1.MeshData = new CubePrimitive();
            _hill1.MaterialResource = _material;
            _hill1.Transform.Scale = new Vector3(11.0f, 0.2f, 9.0f);
            _hill1.Translate(-5.0f, 0.5f, 7.0f);

            _hill2.MeshData = new CubePrimitive();
            _hill2.MaterialResource = _material;
            _hill2.Transform.Scale = new Vector3(9.0f, 0.2f, 7.0f);
            _hill2.Translate(-7.0f, 0.7f, 9.0f);

            for (int idx = 0; idx < 8; idx++)
            {
                _road[idx] = new RoadBlock($"Дорожный блок {idx}");
                _road[idx].Translate(-14.0f + 4.0f * idx, 0.5f, -10.0f);
                AddChild(_road[idx]);
            }

            AddChild(_base);
            AddChild(_hill1);
            AddChild(_hill2);
        }
    }
}