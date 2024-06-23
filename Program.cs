
using OpenTK.Windowing.Common.Input;
using StbImageSharp;
namespace helloGraphics;
class Program
{
    public static void Main(string[] args)
    {

        using (Game game = new Game(500, 500, "Hello Motherfu****", CreateIcon())) game.Run();

    }
    static WindowIcon CreateIcon()
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        var image = ImageResult.FromStream(File.OpenRead("resources/icon.png"), ColorComponents.RedGreenBlueAlpha);
        WindowIcon icon = new(new OpenTK.Windowing.Common.Input.Image(image.Width, image.Height, image.Data));
        return icon;
    }
}
