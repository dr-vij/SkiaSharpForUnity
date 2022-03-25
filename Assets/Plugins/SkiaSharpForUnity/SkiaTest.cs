using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkiaSharp;
using System.IO;

#if UNITY_EDITOR

using UnityEditor;

static class UsefulShortcuts
{
    [MenuItem("Tools/Update emscripten")]
    static void UpdateEmscripten()
    {
        PlayerSettings.WebGL.emscriptenArgs = "";
    }
}
#endif

public class SkiaTest : MonoBehaviour
{
    [SerializeField] private Renderer mTestCubeRenderer = default;
    [SerializeField] private TextAsset mFontAsset = default;

    private void Start()
    {
        Debug.LogWarning("Test start");

        var w = 128;
        var h = 128;

        var skImageInfo = new SKImageInfo(w, h, SKColorType.Rgba8888);

        var bitmap = new SKBitmap(skImageInfo);
        Debug.LogWarning("bitmap was created");

        var skCanvas = new SKCanvas(bitmap);
        Debug.LogWarning("skCanvas was created");

        var skSolor = SKColors.DarkSeaGreen;
        Debug.LogWarning("skSolor was prepared");

        skCanvas.Clear(skSolor);
        Debug.LogWarning("skCanvas was cleared");

        var paint = new SKPaint();
        paint.Color = SKColors.White;
        paint.StrokeWidth = 1;
        skCanvas.DrawCircle(w / 2, h / 2, w / 2, paint);
        Debug.LogWarning("circle was drawn");

        var bytes = mFontAsset.bytes;
        var stream = new MemoryStream(bytes);
        stream.Flush();
        var typeFace = SKTypeface.FromStream(stream);
        Debug.LogWarning($"Font loaded, glyphcount: {typeFace.GlyphCount}");

        paint.Typeface = typeFace;
        paint.TextSize = 24;
        paint.TextAlign = SKTextAlign.Center;
        paint.Color = SKColors.Black;
        skCanvas.DrawText("TEXT", w / 2, h / 2, paint);
        Debug.LogWarning("Text printed");

        skCanvas.Flush();
        Debug.LogWarning("skCanvas was Flushed");

        var texture = new Texture2D(w, h, TextureFormat.RGBA32, false);
        texture.LoadRawTextureData(bitmap.Bytes);
        Debug.LogWarning("bitmap was loaded to unity texture");

        texture.Apply();
        mTestCubeRenderer.material.mainTexture = texture;
        Debug.LogWarning("texture was applied and set to material");

        Debug.LogWarning("Test end");
    }
}
