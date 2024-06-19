using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
namespace helloGraphics;
class Renderer
{
    int count;
    int vertexBufferObject;
    int elementBufferObject;
    int vertexArrayObject;
    Shader shader;

    public Renderer()
    {
        vertexBufferObject = GL.GenBuffer();
        vertexArrayObject = GL.GenVertexArray();
        elementBufferObject = GL.GenBuffer();
        shader = new Shader("resources/basic.vert", "resources/basic.frag");
    }
    public void Load(float[] vertices, uint[] indices)
    {
        count = indices.Length;

        GL.BindVertexArray(vertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

        // position
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        // color
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
        // texture corrds
        GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        GL.EnableVertexAttribArray(2);

        // setting texture params
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        // loading texture
        var image = (Image<Rgba32>)SixLabors.ImageSharp.Image.Load(Configuration.Default, "resources/icon.png");
        byte[] pixels = new byte[4 * image.Width * image.Height];
        image.CopyPixelDataTo(pixels);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        // generating mipmaps (only do it after loading texture)
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

    }
    public void Draw()
    {
        shader.Use();
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, 0);
    }
    public void Unload()
    {
        shader.Dispose();
    }
}