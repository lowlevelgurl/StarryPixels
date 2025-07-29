using Raylib_cs;

namespace StarryPixels;

class Program {
  const Int32 screenWidth = 980;
  const Int32 screenHeight = 680;

  static Int32 rCount = 0;
  static Int32 gCount = 0;
  static Int32 bCount = 0;
  static Int32 pixelateCount = 1;
  static UpDownButtons rCounter = new UpDownButtons();
  static UpDownButtons gCounter = new UpDownButtons();
  static UpDownButtons bCounter = new UpDownButtons();
  static UpDownButtons pixelateCounter = new UpDownButtons();
  static String picturePath = "";
  static String archivePicturePath = "";
  static Boolean isImageAlreadyOpened = false;
  static Image sourceImage;
  static RenderTexture2D target;
  static Image screenImage;
  static Texture2D screenTexture = Raylib.LoadTexture("");
  static Int32 screenImageWidth = 0;
  static Int32 screenImageHeight = 0;
  static float rectX = 0;
  static float rectY = 0;
  static Int32 updateCount = 30;
  static Font segoeUIFont = Raylib.LoadFont("Segoe UI.ttf");

  static void PictureViewer() {

    if (archivePicturePath != picturePath) {
      isImageAlreadyOpened = false;
    }

    if (isImageAlreadyOpened == false) {
      sourceImage = Raylib.LoadImage(picturePath);
      target = Raylib.LoadRenderTexture(sourceImage.Width, sourceImage.Height);

      screenImageWidth = sourceImage.Width;
      screenImageHeight = sourceImage.Height;

      screenImage = Raylib.LoadImage(picturePath);
      Raylib.ImageResize(ref screenImage, screenImageWidth, screenImageHeight);
      screenTexture = Raylib.LoadTextureFromImage(screenImage); 
      
      if (screenImageWidth > 800 || screenImageHeight > 660) {
        screenImageWidth = screenImageWidth / 2;
        screenImageHeight = screenImageHeight / 2;
      }
      rectX = (screenWidth - screenImageWidth) / 2f;
      rectY = (screenHeight - screenImageHeight) / 2f;

      archivePicturePath = picturePath;
      isImageAlreadyOpened = true;
    }

    Raylib.DrawTexturePro(
      target.Texture,
      new Rectangle (0, 0, sourceImage.Width, -sourceImage.Height),
      new Rectangle ((Int32) rectX, (Int32) rectY, screenImageWidth, screenImageHeight),
      new (0.0f, 0.0f),
      (Single) 0,
      Color.White
    ); 

    if (picturePath != "") {
      Raylib.DrawRectangleLinesEx(new Rectangle((Int32) rectX - 1, (Int32) rectY - 1, screenImageWidth + 2, screenImageHeight + 2), 1.0f, new Color (107, 107, 107, 255));
      
      Raylib.DrawRectangle(0, 0, (Int32) rectX - 1, screenHeight, new Color (240, 240, 240, 255));
      Raylib.DrawRectangle((Int32) rectX + screenImageWidth + 2, 0, screenWidth - screenImageWidth, screenHeight, new Color (240, 240, 240, 255));
      Raylib.DrawRectangle(0, 0, screenWidth, (Int32) rectY - 1, new Color (240, 240, 240, 255));
      Raylib.DrawRectangle(0, (Int32) rectY + 1 + screenImageHeight, screenWidth, (Int32) rectY, new Color (240, 240, 240, 255));
    }

    Raylib.DrawRectangle(9, 169, 252, 252, new Color (107, 107, 107, 255));
    Raylib.DrawRectangle(10, 170, 250, 250, new Color(240, 240, 240, 255));
    rCount = rCounter.Create("Red", 200, 180, -255, 255, segoeUIFont);
    gCount = gCounter.Create("Green", 200, 220, -255, 255, segoeUIFont);
    bCount = bCounter.Create("Blue", 200, 260, -255, 255, segoeUIFont);
    pixelateCount = pixelateCounter.Create("Pixelate", 200, 300, 1, 9, segoeUIFont);

    if (updateCount >= 30) {
      target = PixelScreen.Make(screenImage, pixelateCount + 1, rCount, gCount, bCount, target, screenTexture);
      updateCount = 0;
    } 

    updateCount++;
  }

  public static void Main() {
    Raylib.InitWindow(screenWidth, screenHeight, "StarryPixels");
    Raylib.SetTargetFPS(60);

    SavePictureButton savingPicture = new SavePictureButton();
    GetPicturePathButton gettingPicture = new GetPicturePathButton();

    while (!Raylib.WindowShouldClose()) {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(new Color (240, 240, 240, 255));

      PictureViewer();
      savingPicture.Create(40, 344, target, segoeUIFont, picturePath, sourceImage.Width, sourceImage.Height, sourceImage.Width, sourceImage.Height);
      picturePath = gettingPicture.Create(40, 380, segoeUIFont);
      

      Raylib.EndDrawing();
    }

    Raylib.UnloadImage(sourceImage);
    Raylib.UnloadRenderTexture(target);

    Raylib.CloseWindow();
  }
}
