using Raylib_cs;

public class ColorLevel {
  public Int32 level = 0;

  void ColorLevelTexture(String colorName, Color backgroundColor, Int32 x, Int32 y) {
    Raylib.DrawText($"{colorName}: {level}", x, y, 30, Color.Black);
  }

  void ColorLevelButton(String colorName, Int32 x, Int32 y) {
    Raylib.DrawRectangle(x + 180, y, 20, 28, Color.Gray);
    Raylib.DrawText($"+", x + 183, y, 30, Color.Black);
    Raylib.DrawRectangle(x + 202, y, 20, 28, Color.Gray);
    Raylib.DrawText($"-", x + 205, y, 30, Color.Black);

    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle PlusButtonCollision = new (x + 180, y, 20, 28);
    Rectangle MinusButtonCollision = new (x + 202, y, 20, 28);
    Boolean collisionPlus = Raylib.CheckCollisionRecs(mouseCollision, PlusButtonCollision);
    Boolean collisionMinus = Raylib.CheckCollisionRecs(mouseCollision, MinusButtonCollision);
    if (collisionPlus && Raylib.IsMouseButtonDown(MouseButton.Left) && level <= 255) {
      level += 1;
    } else if (collisionMinus && Raylib.IsMouseButtonDown(MouseButton.Left) && level >= -254) {
      level -= 1;
    }

    if (colorName == "A" && level <= -255) {
      level = -255;
    }

    if (colorName == "A" && level >= 0) {
      level = 0;
    }

    if (level >= 255) {
      level = 255;
    }
  }

  public Int32 Drawing(String colorName, Color backgroundColor, Int32 x, Int32 y) {
    ColorLevelTexture(colorName, backgroundColor, x, y);
    ColorLevelButton(colorName, x, y);
    
    return level;
  }
}