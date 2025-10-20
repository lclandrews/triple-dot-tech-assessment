using System;

using UnityEngine;

namespace TripleDot.Blur
{
    public static class Blur
    {
        public static Texture2D CaptureCamera(Camera camera)
        {
            if(camera == null) throw new ArgumentException(nameof(camera));
            
            RenderTextureDescriptor descriptor = new RenderTextureDescriptor(camera.scaledPixelWidth, camera.scaledPixelHeight, 
                RenderTextureFormat.Default, 24)
            {
                msaaSamples = 1,
                sRGB = (QualitySettings.activeColorSpace == ColorSpace.Linear)
            };
            
            RenderTexture renderTexture = RenderTexture.GetTemporary(descriptor);
            
            RenderTexture targetRT = camera.targetTexture;
            camera.targetTexture = renderTexture;
            camera.Render();
            
            RenderTexture activeRT = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Texture2D newTexture = new Texture2D(
                renderTexture.width, renderTexture.height, TextureFormat.RGB24, false, !descriptor.sRGB
            );
            newTexture.ReadPixels(new Rect(0, 0,  renderTexture.width, renderTexture.height), 0, 0);
            newTexture.Apply();
            
            camera.targetTexture = targetRT;
            RenderTexture.active = activeRT;
            RenderTexture.ReleaseTemporary(renderTexture);
            return newTexture;
        }
    }
}
