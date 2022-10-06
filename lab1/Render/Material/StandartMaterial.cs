using System.Numerics;

using Lab1.Core;
using Lab1.Resources;
using ReMath = Lab1.Math;

namespace Lab1.Render
{
    public class StandartMaterial : Material
    {
        private ShaderProgram _shaderProgram;
        private uint _shaderDescriptor;

        public StandartMaterialResource Resource { get; set; } = new StandartMaterialResource();

        public StandartMaterial(ShaderContext context) : base(context)
        {
            _shaderProgram = ShaderLibrary.ColorShader(context);
            _shaderDescriptor = _shaderProgram.GetDescriptor();
        }

        internal override void Use(Viewport viewport, Matrix4x4 view)
        {
            _context.Use(_shaderDescriptor);

            _context.SetUniform(
                _shaderDescriptor,
                "material.diffuse",
                Resource.Diffuse.Red,
                Resource.Diffuse.Green,
                Resource.Diffuse.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                "material.specular",
                Resource.Specular.Red,
                Resource.Specular.Green,
                Resource.Specular.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                "material.shininess",
                Resource.Shininess
            );

            _context.SetUniform(_shaderDescriptor, "model", view);
            _context.SetUniform(_shaderDescriptor, "view", viewport.Camera.View);
            _context.SetUniform(_shaderDescriptor, "projection", viewport.GetProjection());
        }

        public override void LoadResource(MaterialResource resource)
        {
            Resource = (StandartMaterialResource)resource;
        }

        public override void Attach(IEnvironment environment)
        {
            base.Attach(environment);

            _context.SetUniform(
                _shaderDescriptor,
                "env.ambient",
                environment.Ambient.Red,
                environment.Ambient.Green,
                environment.Ambient.Blue
            );
        }

        public override void Attach(IDirectionalLight directionalLight)
        {
            base.Attach(directionalLight);

            _context.SetUniform(
                _shaderDescriptor,
                "dirLight.direction",
                directionalLight.Direction.X,
                directionalLight.Direction.Y,
                directionalLight.Direction.Z
            );

            _context.SetUniform(
                _shaderDescriptor,
                "dirLight.strength",
                directionalLight.Strength
            );

            _context.SetUniform(
                _shaderDescriptor,
                "dirLight.diffuse",
                directionalLight.Diffuse.Red,
                directionalLight.Diffuse.Green,
                directionalLight.Diffuse.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                "dirLight.specular",
                directionalLight.Specular.Red,
                directionalLight.Specular.Green,
                directionalLight.Specular.Blue
            );
        }

        public override void Attach(IPointLight[] pointLights)
        {
            int lightID = 0;

            for (; lightID < pointLights.Length && lightID < 16; lightID++)
            {
                addLight(pointLights[lightID], lightID);
            }

            for (; lightID < 16; lightID++)
            {
                addLight(new PointLightEmpty(), lightID);
            }
        }

        private void addLight(IPointLight light, int idx)
        {
            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].position",
                light.Position.X,
                light.Position.Y,
                light.Position.Z
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].diffuse",
                light.Diffuse.Red,
                light.Diffuse.Green,
                light.Diffuse.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].specular",
                light.Specular.Red,
                light.Specular.Green,
                light.Specular.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].constant",
                light.Constant
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].linear",
                light.Linear
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"pointLights[{idx}].quadratic",
                light.Quadratic
            );
        }


        public override void Attach(ICamera camera)
        {
            _context.SetUniform(
                _shaderDescriptor,
                "camera.position",
                camera.Position.X,
                camera.Position.Y,
                camera.Position.Z
            );
        }
    }
}