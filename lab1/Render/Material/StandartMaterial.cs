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

        // TODO: Maybe light generator is needed

        public override void Attach(IDirectionalLight[] directionalLights)
        {
            base.Attach(directionalLights);

            int lightID = 0;

            for (; lightID < directionalLights.Length && lightID < RenderServer.MaxDirectionalLightCount; lightID++)
            {
                AddDirectionalLight(directionalLights[lightID], lightID);
            }

            for (; lightID < RenderServer.MaxDirectionalLightCount; lightID++)
            {
                AddDirectionalLight(new DirectionalLightEmpty(), lightID);
            }
        }

        public override void Attach(IPointLight[] pointLights)
        {
            base.Attach(pointLights);

            int lightID = 0;

            for (; lightID < pointLights.Length && lightID < RenderServer.MaxPointLightCount; lightID++)
            {
                AddPointLight(pointLights[lightID], lightID);
            }

            for (; lightID < RenderServer.MaxPointLightCount; lightID++)
            {
                AddPointLight(new PointLightEmpty(), lightID);
            }
        }

        public override void Attach(ISpotLight[] spotLights)
        {
            base.Attach(spotLights);

            int lightID = 0;

            for (; lightID < spotLights.Length && lightID < RenderServer.MaxSpotLightCount; lightID++)
            {
                AddSpotLight(spotLights[lightID], lightID);
            }

            for (; lightID < RenderServer.MaxSpotLightCount; lightID++)
            {
                AddSpotLight(new SpotLightEmpty(), lightID);
            }
        }

        private void AddDirectionalLight(IDirectionalLight light, int idx)
        {
            _context.SetUniform(
                _shaderDescriptor,
                $"dirLights[{idx}].direction",
                light.Direction.X,
                light.Direction.Y,
                light.Direction.Z
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"dirLights[{idx}].strength",
                light.Strength
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"dirLights[{idx}].diffuse",
                light.Diffuse.Red,
                light.Diffuse.Green,
                light.Diffuse.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"dirLights[{idx}].specular",
                light.Specular.Red,
                light.Specular.Green,
                light.Specular.Blue
            );
        }

        private void AddPointLight(IPointLight light, int idx)
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

        private void AddSpotLight(ISpotLight light, int idx)
        {
            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].position",
                light.Position.X,
                light.Position.Y,
                light.Position.Z
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].direction",
                light.Direction.X,
                light.Direction.Y,
                light.Direction.Z
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].cutOff",
                light.CutOff
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].outerCutOff",
                light.OuterCutOff
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].diffuse",
                light.Diffuse.Red,
                light.Diffuse.Green,
                light.Diffuse.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].specular",
                light.Specular.Red,
                light.Specular.Green,
                light.Specular.Blue
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].constant",
                light.Constant
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].linear",
                light.Linear
            );

            _context.SetUniform(
                _shaderDescriptor,
                $"spotLights[{idx}].quadratic",
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