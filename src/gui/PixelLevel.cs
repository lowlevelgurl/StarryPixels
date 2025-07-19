using Raylib_cs;

public class PixelLevel {
  public Int32 level = 1;

  void PixelLevelTexture(Int32 x, Int32 y) {
    Raylib.DrawText($"Pixelable: {level}", x, y, 30, Color.Black);
  }

  void PixelLevelButton(Int32 x, Int32 y) {
    Raylib.DrawRectangle(x + 180, y, 20, 28, Color.Gray);
    Raylib.DrawText($"+", x + 183, y, 30, Color.Black);
    Raylib.DrawRectangle(x + 202, y, 20, 28, Color.Gray);
    Raylib.DrawText($"-", x + 205, y, 30, Color.Black);

    Rectangle mouseCollision = new (Raylib.GetMousePosition()[0], Raylib.GetMousePosition()[1], 15, 15);
    Rectangle PlusButtonCollision = new (x + 180, y, 20, 28);
    Rectangle MinusButtonCollision = new (x + 202, y, 20, 28);
    Boolean collisionPlus = Raylib.CheckCollisionRecs(mouseCollision, PlusButtonCollision);
    Boolean collisionMinus = Raylib.CheckCollisionRecs(mouseCollision, MinusButtonCollision);
    if (collisionPlus && Raylib.IsMouseButtonDown(MouseButton.Left) && level <= 9) {
      level += 1;
    } else if (collisionMinus && Raylib.IsMouseButtonDown(MouseButton.Left) && level >= 1) {
      level -= 1;
    }

    if (level <= 1) {
      level = 1;
    }

    if (level >= 9) {
      level = 9;
    }
  }

  public Int32 Drawing(Int32 x, Int32 y) {
    PixelLevelTexture(x, y);
    PixelLevelButton(x, y);
    
    return level;
  }
}