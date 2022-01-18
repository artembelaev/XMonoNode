using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XMonoNodeEditor
{
    public static class FlowNodeEditorResources
    {
        // Textures

        public static Texture2D flow
        {
            get
            {
                return _flow != null ? _flow : _flow = Resources.Load<Texture2D>("xnode_flow");
            }
        }
        private static Texture2D _flow;

        public static Texture2D flowOuter
        {
            get
            {
                return _flowOuter != null ? _flowOuter : _flowOuter = Resources.Load<Texture2D>("xnode_flow_outer");
            }
        }
        private static Texture2D _flowOuter;

        public static Texture2D IconPlay16
        {
            get
            {
                return iconPlay16 != null ? iconPlay16 : iconPlay16 = Resources.Load<Texture2D>("xnode_icon_play_16");
            }
        }
        private static Texture2D iconPlay16 = null;

        public static Texture2D IconPlay16Hover
        {
            get
            {
                return iconPlay16Hover != null ? iconPlay16Hover : iconPlay16Hover = Resources.Load<Texture2D>("xnode_icon_play_16_hover");
            }
        }
        private static Texture2D iconPlay16Hover = null;

        public static Texture2D IconPlay16Active
        {
            get 
            {
                return iconPlay16Active != null ? iconPlay16Active : iconPlay16Active = Resources.Load<Texture2D>("xnode_icon_play_16_active");
            }
        }
        private static Texture2D iconPlay16Active = null;

        public static Texture2D IconError16
        {
            get
            {
                return iconError16 != null ? iconError16 : iconError16 = Resources.Load<Texture2D>("xnode_icon_error_16");
            }
        }
        private static Texture2D iconError16 = null;

        public static Texture2D EaseTextureClamped01(XMonoNode.EasingMode mode, bool reverse = false)
        {
            Texture2D tex;
            if (reverse)
            {
                if (!easeTexturesClamped01Reverse.TryGetValue(mode, out tex) || tex == null)
                {
                    tex = GenerateEaseTexture(mode, true, reverse);
                    easeTexturesClamped01Reverse[mode] = tex;
                }
            }
            else
            {
                if (!easeTexturesClamped01.TryGetValue(mode, out tex) || tex == null)
                {
                    tex = GenerateEaseTexture(mode, true, reverse);
                    easeTexturesClamped01[mode] = tex;
                }
            }
            return tex;
        }

        private static Dictionary<XMonoNode.EasingMode, Texture2D> easeTexturesClamped01 = new Dictionary<XMonoNode.EasingMode, Texture2D>();
        private static Dictionary<XMonoNode.EasingMode, Texture2D> easeTexturesClamped01Reverse = new Dictionary<XMonoNode.EasingMode, Texture2D>();

        public static Texture2D EaseTexture(XMonoNode.EasingMode mode)
        {
            Texture2D tex;
            if (!easeTextures.TryGetValue(mode, out tex) || tex == null)
            {
                tex = GenerateEaseTexture(mode, false);
                easeTextures[mode] = tex;
            }
            return tex;
        }

        private static Dictionary<XMonoNode.EasingMode, Texture2D> easeTextures = new Dictionary<XMonoNode.EasingMode, Texture2D>();

        // Styles
        public static Styles styles { get { return _styles != null ? _styles : _styles = new Styles(); } }
        public static Styles _styles = null;
        public static GUIStyle OutputPort { get { return new GUIStyle(EditorStyles.label) { alignment = TextAnchor.UpperRight }; } }
        public class Styles
        {
            public GUIStyle inputPortFlow;
            public GUIStyle outputPortFlow;
            public GUIStyle playButton;
            public GUIStyle errorButton;

            public Styles()
            {
                GUIStyle baseStyle = new GUIStyle("Label");
                baseStyle.fixedHeight = 18;


                inputPortFlow = new GUIStyle(baseStyle);
                inputPortFlow.alignment = TextAnchor.UpperLeft;
                inputPortFlow.padding.left = 0;
                inputPortFlow.active.background = flow;
                inputPortFlow.normal.background = flowOuter;

                outputPortFlow = new GUIStyle(baseStyle);
                outputPortFlow.alignment = TextAnchor.UpperRight;
                outputPortFlow.padding.right = 0;
                outputPortFlow.active.background = flow;
                outputPortFlow.normal.background = flowOuter;

                playButton = new GUIStyle(baseStyle);
                playButton.alignment = TextAnchor.MiddleCenter;
                playButton.active.background = IconPlay16Active;
                playButton.normal.background = IconPlay16;
                playButton.hover.background = IconPlay16Hover;

                errorButton = new GUIStyle(baseStyle);
                errorButton.alignment = TextAnchor.MiddleCenter;
                errorButton.active.background = IconError16;
                errorButton.normal.background = IconError16;
            }
        }

        public static void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
        {
            int dy = (int)(y1-y0);
            int dx = (int)(x1-x0);
            int stepx, stepy;

            if (dy < 0)
            {
                dy = -dy;
                stepy = -1;
            }
            else
            {
                stepy = 1;
            }
            if (dx < 0)
            {
                dx = -dx;
                stepx = -1;
            }
            else
            {
                stepx = 1;
            }
            dy <<= 1;
            dx <<= 1;

            float fraction = 0;

            tex.SetPixel(x0, y0, col);
            if (dx > dy)
            {
                fraction = dy - (dx >> 1);
                while (Mathf.Abs(x0 - x1) > 1)
                {
                    if (fraction >= 0)
                    {
                        y0 += stepy;
                        fraction -= dx;
                    }
                    x0 += stepx;
                    fraction += dy;
                    tex.SetPixel(x0, y0, col);
                }
            }
            else
            {
                fraction = dx - (dy >> 1);
                while (Mathf.Abs(y0 - y1) > 1)
                {
                    if (fraction >= 0)
                    {
                        x0 += stepx;
                        fraction -= dy;
                    }
                    y0 += stepy;
                    fraction += dx;
                    tex.SetPixel(x0, y0, col);
                }
            }
        }

        private static bool InRange(float value, float min, float max)
        {
            return min <= value && value <= max;
        }

        private static Texture2D GenerateEaseTexture(XMonoNode.EasingMode mode, bool clamped01, bool reverse = false)
        {
            Color backColor = new Color(0.15f, 0.15f, 0.15f, 0.5f);
            Color borderColor = Color.black;
            Color axesColor = new Color(0f, 0f, 0f, 0.1f);
            Color curveColor = Color.blue;
            int width = 90;
            int padding = 20;
            Texture2D tex = new Texture2D(width, width);
            Color[] cols = new Color[width * width];
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    
                    cols[(y * width) + x] = backColor;
                }
            }
            tex.SetPixels(cols);
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.name = mode.ToString();

            // axes
            DrawLine(tex, padding, 0, padding, width, axesColor);
            DrawLine(tex, 0, padding, width, padding, axesColor);

            // 1,1 lines
            DrawLine(tex, width - padding, width - padding, padding, width - padding, new Color(0.5f, 0.5f, 0.5f, 0.5f));
            DrawLine(tex, width - padding, width - padding, width - padding, padding, new Color(0.5f, 0.5f, 0.5f, 0.5f));

            // curve
            int x0 = 0;
            int y0 = 0;
            int areaWidth = width - 2*padding;
            for (int x_ = -padding; x_ < width; ++x_)
            {
                float t = x_ / (float)areaWidth;
                if (reverse)
                {
                    t = 1.0f - t;
                }

                if (clamped01)
                {
                    t = Mathf.Clamp01(t);
                }
                int y = Mathf.RoundToInt(XMonoNode.FloatEase.Ease(t, mode) * areaWidth);
                y += padding;
                int x = x_ + padding;

                if (x_ != -padding
                    && InRange(x0, 0, width - 1)
                    && InRange(y0, 0, width - 1)
                    && InRange(x, 0, width - 1)
                    && InRange(y, 0, width - 1))
                {
                    DrawLine(tex, x0, y0, x, y, curveColor);
                }
                x0 = x;
                y0 = y;
            }

            DrawLine(tex, 0, 0, 0, width - 1, borderColor);
            DrawLine(tex, width - 1, 0, width - 1, width - 1, borderColor);
            DrawLine(tex, 0, 0, width, 0, borderColor);
            DrawLine(tex, 0, width-1, width, width-1, borderColor);
            // DrawLine(tex, width - padding, width - padding, width - padding, padding, new Color(0.5f, 0.5f, 0.5f, 0.5f));

            tex.Apply();
            return tex;
        }
    }
}