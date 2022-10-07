using Silk.NET.OpenGL;
using System.Numerics;

using GlShaderType = Silk.NET.OpenGL.ShaderType;

public enum ShaderType
{
    VertexShader = GlShaderType.VertexShader,
    FragmentShader = GlShaderType.FragmentShader,
}

namespace Lab1.Render
{
    public class ShaderContext
    {
        private GL _gl;
        public ShaderContext(GL gl)
        {
            _gl = gl;
        }

        public uint CreateProgram()
        {
            return _gl.CreateProgram();
        }

        public uint CreateShader(ShaderType type, string source)
        {
            uint shader = _gl.CreateShader(((GlShaderType)type));
            _gl.ShaderSource(shader, source);
            _gl.CompileShader(shader);

            string infoLog = _gl.GetShaderInfoLog(shader);

            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader '{shader}': {infoLog}");
            }

            return shader;
        }

        public void LinkShader(uint program, uint shader)
        {
            _gl.AttachShader(program, shader);
            _gl.LinkProgram(program);
        }

        public void DeleteProgram(uint program)
        {
            _gl.DeleteProgram(program);
        }

        public void DeleteShader(uint shader)
        {
            _gl.DeleteProgram(shader);
        }

        public void Use(uint program)
        {
            _gl.UseProgram(program);
        }

        public void SetUniform(uint program, string name, int value)
        {
            int location = _gl.GetUniformLocation(program, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            _gl.Uniform1(location, value);
        }

        public void SetUniform(uint program, string name, float value)
        {
            int location = _gl.GetUniformLocation(program, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            _gl.Uniform1(location, value);
        }

        public void SetUniform(uint program, string name, float x, float y, float z)
        {
            int location = _gl.GetUniformLocation(program, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            _gl.Uniform3(location, x, y, z);
        }

        public void SetUniform(uint program, string name, float x, float y, float z, float w)
        {
            int location = _gl.GetUniformLocation(program, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            _gl.Uniform4(location, x, y, z, w);
        }

        public unsafe void SetUniform(uint program, string name, Matrix4x4 value)
        {
            int location = _gl.GetUniformLocation(program, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }
    }
}