using Silk.NET.OpenGL;
using System.Numerics;

public class Shader : IDisposable
{
    private uint _shaderProgram;
    private GL _context;

    public Shader(GL context, string vertexShaderSource, string fragmentShaderSource)
    {
        _context = context;

        uint vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
        uint fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

        _shaderProgram = CreateShaderProgram(vertexShader, fragmentShader);

        context.DeleteShader(vertexShader);
        context.DeleteShader(fragmentShader);
    }

    static Shader FromFiles(GL context, string vertexShaderSourcePath, string fragmentShaderSourcePath)
    {
        string vertexShaderSource = File.ReadAllText(vertexShaderSourcePath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderSourcePath);

        return new Shader(context, vertexShaderSource, fragmentShaderSource);
    }

    private uint CompileShader(ShaderType type, string source)
    {
        uint shader = _context.CreateShader(type);
        _context.ShaderSource(shader, source);
        _context.CompileShader(shader);

        CompileShaderLog(shader);

        return shader;
    }

    private void CompileShaderLog(uint shader)
    {
        string infoLog = _context.GetShaderInfoLog(shader);

        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            throw new Exception($"Error compiling shader '{shader}': {infoLog}");
        }
    }

    private uint CreateShaderProgram(uint vertexShader, uint fragmentShader)
    {
        uint shaderProgram = _context.CreateProgram();
        _context.AttachShader(shaderProgram, vertexShader);
        _context.AttachShader(shaderProgram, fragmentShader);
        _context.LinkProgram(shaderProgram);

        return shaderProgram;
    }

    public void Use()
    {
        _context.UseProgram(_shaderProgram);
    }

    public void SetUniform(string name, int value)
    {
        int location = _context.GetUniformLocation(_shaderProgram, name);

        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }

        _context.Uniform1(location, value);
    }

    public void SetUniform(string name, float x, float y, float z)
    {
        int location = _context.GetUniformLocation(_shaderProgram, name);

        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }

        _context.Uniform3(location, x, y, z);
    }

    public void SetUniform(string name, float x, float y, float z, float w)
    {
        int location = _context.GetUniformLocation(_shaderProgram, name);

        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }

        _context.Uniform4(location, x, y, z, w);
    }

    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        int location = _context.GetUniformLocation(_shaderProgram, name);

        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }

        _context.UniformMatrix4(location, 1, false, (float*)&value);
    }

    public void Dispose()
    {
        _context.DeleteProgram(_shaderProgram);
    }
}