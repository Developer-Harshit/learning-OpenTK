
using OpenTK.Windowing.Common.Input;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
namespace helloGraphics;
class Program
{
    public static void Main(string[] args)
    {

        using (Game game = new Game(500, 500, "Hello Motherfu****", CreateIcon())) game.Run();

    }
    static WindowIcon CreateIcon()
    {
        var image = (Image<Rgba32>)SixLabors.ImageSharp.Image.Load(Configuration.Default, "resources/icon.png");
        image.Mutate(x => x.Flip(FlipMode.Vertical));
        var pixels = new byte[4 * image.Width * image.Height];
        image.CopyPixelDataTo(pixels);
        WindowIcon icon = new(new OpenTK.Windowing.Common.Input.Image(image.Width, image.Height, pixels));
        return icon;
    }
}
