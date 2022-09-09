using Silk.NET.OpenGL;

class Shader : IDisposable {
    private uint _shaderProgram;
    private GL _gl;

    public Shader(GL gl, string vertexShaderSourcePath, string fragmentShaderSourcePath) {
        _gl = gl;

        string vertexShaderSource = File.ReadAllText(vertexShaderSourcePath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderSourcePath);

        uint vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
        uint fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

        _shaderProgram = CreateShaderProgram(vertexShader, fragmentShader);

        gl.DeleteShader(vertexShader);
        gl.DeleteShader(fragmentShader);
    }

    private uint CompileShader(ShaderType type, string source) {
        uint shader = _gl.CreateShader(type);
        _gl.ShaderSource(shader, source);
        _gl.CompileShader(shader);

        CompileShaderLog(shader);

        return shader;
    }

    private void CompileShaderLog(uint shader) {
        string infoLog = _gl.GetShaderInfoLog(shader);

        if (!string.IsNullOrWhiteSpace(infoLog)) {
            throw new Exception($"Error compiling shader from file: {infoLog}");
        }
    }

    private uint CreateShaderProgram(uint vertexShader, uint fragmentShader) {
        uint shaderProgram = _gl.CreateProgram();
        _gl.AttachShader(shaderProgram, vertexShader);
        _gl.AttachShader(shaderProgram, fragmentShader);
        _gl.LinkProgram(shaderProgram);

        return shaderProgram;
    }

    public void Use() {
        _gl.UseProgram(_shaderProgram);
    }

    public void SetUniform(string name, int value ) {
        int location = _gl.GetUniformLocation(_shaderProgram, name);

        if (location == -1) {
            throw new Exception($"{name} uniform not found on shader.");
        }

        _gl.Uniform1(location, value);
    }

    public void Dispose() {
        _gl.DeleteProgram(_shaderProgram);
    }
}