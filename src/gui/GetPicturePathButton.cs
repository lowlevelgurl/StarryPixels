using Raylib_cs;
using System.Numerics;
using NativeFileDialogSharp;

public class GetPicturePathButton {
  public String picturePath = "";

  public Color WindowsButtonGray = new Color (225, 225, 225, 255);
  public Color WindowsBorderGray = new Color (173, 173, 173, 255);
  public Color WindowsButtonActive = new Color (229, 241, 251, 255);
  public Color WindowsButtonClick = new Color (204, 228, 247, 255);
  public Color WindowsBorderActive = new Color (0, 120, 215, 255);

  public Color buttonColor;
  public Color borderButtonColor;

  String OpenDialog(DialogResult result) {
    if (result.IsOk == true) {
      return String.Join("\n", result.Paths);
    } else {
      return "";
    }
  }

  void Drawing(Int32 x, Int32 y, Font font) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle buttonCollision = new (x, y, 166, 30);
    Boolean collision = Raylib.CheckCollisionRecs(mouseCollision, buttonCollision);
    
    Raylib.DrawRectangle(x - 1, y - 1, 166, 30, borderButtonColor);
    Raylib.DrawRectangle(x, y, 164, 28, buttonColor);
    Raylib.DrawTextEx(font, $"Find Picture", new Vector2((Single) x + 20.0f, (Single) y - 2.0f), font.BaseSize, 0, Color.Black);

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

  void OnClick(Int32 x, Int32 y) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle buttonCollision = new (x, y, 166, 30);
    Boolean collision = Raylib.CheckCollisionRecs(mouseCollision, buttonCollision);

    if (collision && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
      picturePath = OpenDialog(Dialog.FileOpenMultiple("jpg", null));
      Console.WriteLine(picturePath);
    }
  }

  public String Create(Int32 x, Int32 y, Font font) {
    Drawing(x, y, font);
    OnClick(x, y);

    return picturePath;
  }
}