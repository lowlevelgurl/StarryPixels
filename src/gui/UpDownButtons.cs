using Raylib_cs;
using System.Numerics;

public class UpDownButtons {
  public Int32 level = 0;

  public Color WindowsButtonGray = new Color (225, 225, 225, 255);
  public Color WindowsBorderGray = new Color (173, 173, 173, 255);
  public Color WindowsButtonActive = new Color (229, 241, 251, 255);
  public Color WindowsButtonClick = new Color (204, 228, 247, 255);
  public Color WindowsBorderActive = new Color (0, 120, 215, 255);

  public Color buttonUpColor;
  public Color borderButtonUpColor;
  public Color buttonDownColor;
  public Color borderButtonDownColor;

  void Drawing(String buttonName, Int32 x, Int32 y, Font font) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 9, 9);
    Rectangle upButtonCollision = new (x - 1, y - 1, 22, 30);
    Rectangle downButtonCollision = new (x + 24, y - 1, 20, 28);
    Boolean collisionUp = Raylib.CheckCollisionRecs(mouseCollision, upButtonCollision);
    Boolean collisionDown = Raylib.CheckCollisionRecs(mouseCollision, downButtonCollision);

    Raylib.DrawRectangle(x - 1, y - 1, 22, 30, borderButtonUpColor);
    Raylib.DrawRectangle(x, y, 20, 28, buttonUpColor);
    Raylib.DrawTextEx(font, $"+", new Vector2((Single) x + 2.0f, (Single) y - 4.0f), font.BaseSize, 0, Color.Black);
    Raylib.DrawRectangle(x + 24, y - 1, 22, 30, borderButtonDownColor);
    Raylib.DrawRectangle(x + 25, y, 20, 28, buttonDownColor);
    Raylib.DrawTextEx(font, $"-", new Vector2((Single) x + 30.0f, (Single) y - 4.0f), font.BaseSize, 0, Color.Black);

    if (level > 0 && buttonName != "Pixelate") {
      Raylib.DrawTextEx(font, $"{buttonName}: +{level}", new Vector2((Single) x - 180.0f, (Single) y), font.BaseSize, 0, Color.Black);
    } else {
      Raylib.DrawTextEx(font, $"{buttonName}: {level}", new Vector2((Single) x - 180.0f, (Single) y), font.BaseSize, 0, Color.Black);
    }

    if (collisionUp && Raylib.IsMouseButtonDown(MouseButton.Left)) {
      buttonUpColor = WindowsButtonClick;
      borderButtonUpColor = WindowsBorderActive;
    } else if (collisionUp) {
      buttonUpColor = WindowsButtonActive;
      borderButtonUpColor = WindowsBorderActive;
    } else {
      buttonUpColor = WindowsButtonGray;
      borderButtonUpColor = WindowsBorderGray;
    }

    if (collisionDown && Raylib.IsMouseButtonDown(MouseButton.Left)) {
      buttonDownColor = WindowsButtonClick;
      borderButtonDownColor = WindowsBorderActive;
    } else if (collisionDown) {
      buttonDownColor = WindowsButtonActive;
      borderButtonDownColor = WindowsBorderActive;
    } else {
      buttonDownColor = WindowsButtonGray;
      borderButtonDownColor = WindowsBorderGray;
    }
  }

  void OnClick(String buttonName, Int32 x, Int32 y, Int32 minValue, Int32 maxValue) {
    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 9, 9);
    Rectangle upButtonCollision = new (x - 1, y - 1, 22, 30);
    Rectangle downButtonCollision = new (x + 24, y - 1, 20, 28);

    Boolean collisionUp = Raylib.CheckCollisionRecs(mouseCollision, upButtonCollision);
    Boolean collisionDown = Raylib.CheckCollisionRecs(mouseCollision, downButtonCollision);
    
    if (buttonName != "Pixelate") {
      if (collisionUp && Raylib.IsMouseButtonDown(MouseButton.Left) && level <= maxValue) {
        level += 1;
      } else if (collisionDown && Raylib.IsMouseButtonDown(MouseButton.Left) && level >= minValue) {
        level -= 1;
      }
    } else {
      if (collisionUp && Raylib.IsMouseButtonPressed(MouseButton.Left) && level <= maxValue) {
        level += 1;
      } else if (collisionDown && Raylib.IsMouseButtonPressed(MouseButton.Left) && level >= minValue) {
        level -= 1;
      }
    }

    if (level >= maxValue) {
      level = maxValue;
    }

    if (level <= minValue) {
      level = minValue;
    }
  }

  public Int32 Create(String buttonName, Int32 x, Int32 y, Int32 minValue, Int32 maxValue, Font font) {
    Drawing(buttonName, x, y, font);
    OnClick(buttonName, x, y, minValue, maxValue);

    return level;
  }
}