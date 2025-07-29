using Raylib_cs;
using System.Numerics;

public class SavePictureButton {
  public Color WindowsButtonGray = new Color (225, 225, 225, 255);
  public Color WindowsBorderGray = new Color (173, 173, 173, 255);
  public Color WindowsButtonActive = new Color (229, 241, 251, 255);
  public Color WindowsButtonClick = new Color (204, 228, 247, 255);
  public Color WindowsBorderActive = new Color (0, 120, 215, 255);

  public Color buttonColor;
  public Color borderButtonColor;

  void Drawing(Int32 x, Int32 y, Font font) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle buttonCollision = new (x, y, 166, 30);
    Boolean collision = Raylib.CheckCollisionRecs(mouseCollision, buttonCollision);
    
    Raylib.DrawRectangle(x - 1, y - 1, 166, 30, borderButtonColor);
    Raylib.DrawRectangle(x, y, 164, 28, buttonColor);
    Raylib.DrawTextEx(font, $"Save Picture", new Vector2((Single) x + 20.0f, (Single) y - 2.0f), font.BaseSize, 0, Color.Black);

    if (collision && Raylib.IsMouseButtonDown(MouseButton.Left)) {
      buttonColor = WindowsButtonClick;
      borderButtonColor = WindowsBorderActive;
    } else if (collision) {
      buttonColor = WindowsButtonActive;
      borderButtonColor = WindowsBorderActive;
    } else {
      buttonColor = WindowsButtonGray;
      borderButtonColor = WindowsBorderGray;
    }
  }

  void OnClick(Int32 x, Int32 y, RenderTexture2D target, String imagePath, Int32 width, Int32 height, Int32 widthOriginal, Int32 heightOriginal) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle buttonCollision = new (x, y, 166, 30);
    Boolean collision = Raylib.CheckCollisionRecs(mouseCollision, buttonCollision);

    if (collision && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
      Image resultImage = Raylib.LoadImageFromTexture(target.Texture);
      Raylib.ImageFlipVertical(ref resultImage);
      Raylib.ImageResizeCanvas(ref resultImage, width, height, 0, 0, new Color ());
      Raylib.ImageResize(ref resultImage, widthOriginal, heightOriginal);
      Raylib.ExportImage(resultImage, $"{Path.GetFileNameWithoutExtension(imagePath)}_pixelate.jpg");
    }
  }

  public void Create(Int32 x, Int32 y, RenderTexture2D target, Font font, String imagePath, Int32 width, Int32 height, Int32 widthOriginal, Int32 heightOriginal) {
    Drawing(x, y, font);
    OnClick(x, y, target, imagePath, width, height, widthOriginal, heightOriginal);
  }
}