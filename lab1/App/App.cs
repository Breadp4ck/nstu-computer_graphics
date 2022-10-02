using Lab1.Main;
using Lab1.Main.Scene3D;
using Lab1.Resources;

namespace Lab1.App
{
    public class App
    {
        private Scene _scene;

        public App()
        {
            _scene = new Scene();

            _scene.AttachViewport();

            var myMesh = new MeshInstance3D(_scene, "Меш");
            myMesh.MeshData = new RectanglePrimitive();

            _scene.AddNode(myMesh);
        }
    }
}