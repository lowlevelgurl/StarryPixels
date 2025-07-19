using Raylib_cs;

namespace StarryPixels;

class Program {
  static Int32 rLevelInt = 0;
  static Int32 gLevelInt = 0;
  static Int32 bLevelInt = 0;
  static Int32 aLevelInt = 0;
  static Int32 pixelLevelInt = 1;

  public static void Main() {
    Raylib.InitWindow(980, 680, "StarryPixels");

    Image sourceImage = Raylib.LoadImage("image.jpg");
    RenderTexture2D target = Raylib.LoadRenderTexture(sourceImage.Width, sourceImage.Height);

    Int32 screenImageWidth = sourceImage.Width;
    Int32 screenImageHeight = sourceImage.Height;

    while (screenImageWidth > 800 || screenImageHeight > 660) {
      screenImageWidth = screenImageWidth / 2;
      screenImageHeight = screenImageHeight / 2;
    }

    Image screenImage = Raylib.LoadImage("image.jpg");
    Raylib.ImageResize(ref screenImage, screenImageWidth, screenImageHeight);

    float rectX = (980 - screenImageWidth) / 2f;
    float rectY = (680 - screenImageHeight) / 2f;



    ColorLevel rLevel = new ColorLevel();
    ColorLevel gLevel = new ColorLevel();
    ColorLevel bLevel = new ColorLevel();
    ColorLevel aLevel = new ColorLevel();

    PixelLevel pixelLevel = new PixelLevel();

    Raylib.SetTargetFPS(60);

    while (!Raylib.WindowShouldClose()) {
      target = PixelScreen.Make(screenImage, pixelLevelInt + 1, rLevelInt, gLevelInt, bLevelInt, aLevelInt * -1, target);
      
      Raylib.BeginDrawing();
      Raylib.ClearBackground(new Color(240, 240, 240, 255));

      Raylib.DrawRectangle((int) rectX - 1, (int) rectY -1, screenImageWidth + 2, screenImageHeight + 2, Color.Black);
      Raylib.DrawTexturePro(
        target.Texture,
        new Rectangle (0, 0, sourceImage.Width, -sourceImage.Height),
        new Rectangle ((int) rectX, (int) rectY, sourceImage.Width, sourceImage.Height),
        new (0.0f, 0.0f),
        (Single) 0,
        Color.White
      );

      Raylib.DrawRectangle(10, 170, 250, 250, Color.White);
      Raylib.DrawRectangleLines(10, 170, 250, 250, Color.Black);
      rLevelInt = rLevel.Drawing("R", Color.Red, 20, 180);
      gLevelInt = gLevel.Drawing("G", Color.Green, 20, 220);
      bLevelInt = bLevel.Drawing("B", Color.Blue, 20, 260);
      aLevelInt = aLevel.Drawing("A", Color.White, 20, 300);
      pixelLevelInt = pixelLevel.Drawing(20, 340);

      Raylib.EndDrawing();
    }
    Image resultImage = Raylib.LoadImageFromTexture(target.Texture);
    Raylib.ImageFlipVertical(ref resultImage);
    Raylib.ImageResize(ref resultImage, Raylib.LoadImage("image.jpg").Width, Raylib.LoadImage("image.jpg").Height);
    Raylib.ExportImage(resultImage, "boobs.jpg");

    Raylib.UnloadImage(sourceImage);
    Raylib.UnloadRenderTexture(target);

    Raylib.CloseWindow();
  }
}
