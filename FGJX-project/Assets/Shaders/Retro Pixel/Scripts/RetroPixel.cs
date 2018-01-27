using UnityEngine;
using System.Collections;

namespace AlpacaSound
{
	[ExecuteInEditMode]
	[RequireComponent (typeof(Camera))]
	[AddComponentMenu("Image Effects/Custom/Retro Pixel")]
	public class RetroPixel : MonoBehaviour
	{
		public static readonly int MAX_NUM_COLORS = 256;

		public int horizontalResolution = 160;
		public int verticalResolution = 200;

		public int numColors = MAX_NUM_COLORS;
		int oldNumColors = 0;

        Shader customSizeShader;

        public Color[] colors = new Color[256];

		Material m_material;
		Material material
		{
			get
			{
				if (m_material == null)
				{


                    customSizeShader = Shader.Find("AlpacaSound/RetroPixelUnlimited");

                    m_material = new Material (customSizeShader);
					m_material.hideFlags = HideFlags.DontSave;
				}
				return m_material;
			} 
		}

		void Start ()
		{
            if (!SystemInfo.supportsImageEffects)
			{
				enabled = false;
				return;
			}

		}
		
		public void OnRenderImage (RenderTexture src, RenderTexture dest)
		{
			horizontalResolution = Mathf.Clamp(horizontalResolution, 1, 2048);
			verticalResolution = Mathf.Clamp(verticalResolution, 1, 2048);
			numColors = Mathf.Clamp(numColors, 2, 256);

			if (material)
			{
                material.SetInt("_ColorCount", numColors);
                material.SetColorArray("_Colors", colors);
				
				RenderTexture scaled = RenderTexture.GetTemporary (horizontalResolution, verticalResolution);
				scaled.filterMode = FilterMode.Point;
				Graphics.Blit (src, scaled, material);
				Graphics.Blit (scaled, dest);
				RenderTexture.ReleaseTemporary (scaled);
				
			}
			else
			{
				Graphics.Blit (src, dest);
			}
		}

        public void ResetColors()
        {
            for (byte i = 0; i < numColors; i++)
            {
                colors[i] = new Color32((byte)((48 + i * 16) % 255),
                                        (byte)((32 + i * 32) % 255),
                                        (byte)((i * 7) % 255),
                                        255);
            }
        }

		void OnDisable ()
		{
			if (m_material)
			{
				Material.DestroyImmediate (m_material);
			}
		}
	}
}



