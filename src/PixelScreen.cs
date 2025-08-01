using Raylib_cs;
using System.Numerics;

class PixelScreen {
  static Int32[] RGBAFilter(Color originalColor, Int32 rSize, Int32 gSize, Int32 bSize) {
    Int32 r, g, b;
    if ((originalColor.R + rSize) > 255) {
      r = 255;
    } else if ((originalColor.R + rSize) < 0) {
      r = 0;
    } else {
      r = originalColor.R + rSize;
    }

    if ((originalColor.G + gSize) > 255) {
      g = 255;
    } else if ((originalColor.G + gSize) < 0) {
      g = 0;
    } else {
      g = originalColor.G + gSize;
    }

    if ((originalColor.B + bSize) > 255) {
      b = 255;
    } else if ((originalColor.B + bSize) < 0) {
      b = 0;
    } else {
      b = originalColor.B + bSize;
    }

    Int32[] rgb = {r, g, b};

    return rgb;
  }

  public static RenderTexture2D Make(
    Image sourceImage,
    Int32 pixelable,
    Int32 rSize,
    Int32 gSize,
    Int32 bSize,
    RenderTexture2D target,
    Texture2D screenTexture
  ) {
    Raylib.BeginTextureMode(target);

    for (Int32 y = 0; y < sourceImage.Height; y += pixelable) {
      for (Int32 x = 0; x < sourceImage.Width; x += pixelable) {
        Int32[] pixel = RGBAFilter(Raylib.GetImageColor(sourceImage, x, y), rSize, gSize, bSize);
        Color blockColor = new Color(pixel[0], pixel[1], pixel[2], 255);
        Raylib.DrawRectangle(x, y, pixelable, pixelable, blockColor);
      }
    }

    /* Raylib.DrawTextureRec(
      screenTexture,
      new Rectangle (0, 0, screenTexture.Width, screenTexture.Height),
      new Vector2 (
        0.0f,
        0.0f
      ),
      Color.White
    ); */

    Raylib.EndTextureMode();

    return target;
  }
}