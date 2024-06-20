using OpenTK.Graphics.OpenGL4;
namespace helloGraphics;

public class Shader
{
    public int program;
    public Shader(string vertexShaderPath, string fragmentShaderPath)
    {
        string vertexShaderCode = File.ReadAllText(vertexShaderPath);
        string fragmentShaderCode = File.ReadAllText(fragmentShaderPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(vertexShader, vertexShaderCode);
        GL.ShaderSource(fragmentShader, fragmentShaderCode);

        CompileAndCheckShaders(vertexShader, fragmentShader);

        // link them
        program = GL.CreateProgram();
        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(program);
            Console.WriteLine(infoLog);
        }

        // clean up
        GL.DetachShader(program, vertexShader);
        GL.DetachShader(program, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }
    public void Use()
    {
        GL.UseProgram(program);
    }
    public void SetInt(string name, int value)
    {
        int location = GL.GetUniformLocation(program, name);
        GL.Uniform1(location, value);
    }

    static void CompileAndCheckShaders(int vertexShader, int fragmentShader)
    {
        GL.CompileShader(vertexShader);
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(fragmentShader);
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(infoLog);
        }
    }
    // idk wtf the below code does
    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(program);
            disposedValue = true;
        }
    }
    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
