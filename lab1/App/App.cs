using Lab1.Main;
using Lab1.Main.Scene3D;
using Lab1.Resources;
using Lab1.Core;

namespace Lab1.App
{
    public class App
    {
        private Scene _scene;

        public App()
        {
            _scene = new Scene();

            _scene.AttachViewport();

            var myRectangle1 = new MeshInstance3D("Прямоугольник");
            myRectangle1.MeshData = new RectanglePrimitive();

            var cude = new MeshInstance3D("Куб");
            cude.MeshData = new CubePrimitive();

            var myTriangle = new MeshInstance3D("Треугольник");
            myTriangle.MeshData = new TrianglePrimitive();

            _scene.Root.AddChild(myRectangle1);
            myRectangle1.AddChild(cude);
            myRectangle1.AddChild(myTriangle);

            var matRectangle1 = new StandartMaterialResource();
            var matRectangle2 = new StandartMaterialResource();
            var matTriangle = new StandartMaterialResource();

            myRectangle1.Translate(new System.Numerics.Vector3(0.0f, 1.0f, -11.0f));
            cude.Translate(new System.Numerics.Vector3(0.0f, 0.0f, -10.5f));
            myTriangle.Translate(new System.Numerics.Vector3(0.0f, -1.0f, -10.0f));

            matRectangle1.Color = new Color(0.2f, 0.7f, 0.3f);
            matRectangle2.Color = new Color(0.2f, 0.3f, 0.8f);
            matTriangle.Color = new Color(0.6f, 0.2f, 0.3f);

            myRectangle1.MaterialResource = matRectangle1;
            cude.MaterialResource = matRectangle2;
            myTriangle.MaterialResource = matTriangle;

            _scene.Run();
        }
    }
}