using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkiaSharp;

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
    [SerializeField] private Renderer mTestCubeRenderer;

    private void Start()
    {

        Debug.LogWarning("Test start");

        var skImageInfo = new SKImageInfo(64, 64, SKColorType.Rgba8888);

        var bitmap = new SKBitmap(skImageInfo);
        Debug.LogWarning("bitmap was created");

        var skCanvas = new SKCanvas(bitmap);
        Debug.LogWarning("skCanvas was created");

        var skSolor = SKColors.DarkSeaGreen;
        Debug.LogWarning("skSolor was prepared");

        skCanvas.Clear(skSolor);
        Debug.LogWarning("skCanvas was cleared");

        var paint = new SKPaint();
        paint.Color = SKColors.Black;
        paint.StrokeWidth = 1;
        skCanvas.DrawCircle(32, 32, 16, paint);
        Debug.LogWarning("circle was drawn");

        skCanvas.Flush();
        Debug.LogWarning("skCanvas was Flushed");

        var texture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
        texture.LoadRawTextureData(bitmap.Bytes);
        Debug.LogWarning("bitmap was loaded to unity texture");

        texture.Apply();
        mTestCubeRenderer.material.mainTexture = texture;
        Debug.LogWarning("texture was applied and set to material");

        Debug.LogWarning("Test end");
    }
}
