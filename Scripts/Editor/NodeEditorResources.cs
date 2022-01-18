using UnityEditor;
using UnityEngine;

namespace XMonoNodeEditor {
    public static class NodeEditorResources {
        // Textures
        public static Texture2D dot { get { return _dot != null ? _dot : _dot = Resources.Load<Texture2D>("xnode_dot"); } }
        private static Texture2D _dot;

        public static Texture2D dotOuter { get { return _dotOuter != null ? _dotOuter : _dotOuter = Resources.Load<Texture2D>("xnode_dot_outer"); } }
        private static Texture2D _dotOuter;

        public static Texture2D nodeBody { get { return _nodeBody != null ? _nodeBody : _nodeBody = Resources.Load<Texture2D>("xnode_node"); } }
        private static Texture2D _nodeBody;

        public static Texture2D nodeBodySmall
        {
            get
            {
                return _nodeBodySmall != null ? _nodeBodySmall : _nodeBodySmall = Resources.Load<Texture2D>("xnode_node_small");
            }
        }
        private static Texture2D _nodeBodySmall;

        public static Texture2D nodeHighlight { get { return _nodeHighlight != null ? _nodeHighlight : _nodeHighlight = Resources.Load<Texture2D>("xnode_node_highlight"); } }
        private static Texture2D _nodeHighlight;


        public static Texture2D graph
        {
            get
            {
                return _graph != null ? _graph : _graph = Resources.Load<Texture2D>("xnode_graph");
            }
        }
        static Texture2D _graph = null;

        public static Texture2D home
        {
            get
            {
                return _home != null ? _home : _home = Resources.Load<Texture2D>("xnode_home");
            }
        }
        static Texture2D _home = null;


        public static Texture2D hiddenMark
        {
            get
            {
                return _hiddenMark != null ? _hiddenMark : _hiddenMark = Resources.Load<Texture2D>("hidden_mark");
            }
        }
        static Texture2D _hiddenMark = null;

        // Styles
        public static Styles styles { get { return _styles != null ? _styles : _styles = new Styles(); } }
        public static Styles _styles = null;
        public static GUIStyle OutputPort { get { return new GUIStyle(EditorStyles.label) { alignment = TextAnchor.UpperRight }; } }
        public class Styles
        {
            public GUIStyle inputPort;
            public GUIStyle outputPort;
            public GUIStyle nodeHeader;
            public GUIStyle nodeBody;
            public GUIStyle tooltip;
            public GUIStyle nodeHighlight;
            public GUIStyle nodePaletteFoldout;
           // public GUIStyle minimizeButton;
            public GUIStyle minimizeButtonSimple;
            public GUIStyle hiddenAttributeMark;

            public Styles()
            {
                GUIStyle baseStyle = new GUIStyle("Label");
                baseStyle.fixedHeight = 18;

                inputPort = new GUIStyle(baseStyle);
                inputPort.alignment = TextAnchor.UpperLeft;
                inputPort.padding.left = 0;
                inputPort.active.background = dot;
                inputPort.normal.background = dotOuter;

                outputPort = new GUIStyle(baseStyle);
                outputPort.alignment = TextAnchor.UpperRight;
                outputPort.padding.right = 0;
                outputPort.active.background = dot;
                outputPort.normal.background = dotOuter;

                nodeHeader = new GUIStyle();
                nodeHeader.alignment = TextAnchor.MiddleLeft;//MiddleCenter;
                nodeHeader.fontStyle = FontStyle.Bold;
                nodeHeader.normal.textColor = Color.white;
                nodeHeader.margin = new RectOffset(0, 0, 3, 0);
                nodeHeader.padding = new RectOffset(0, 0, 0, 0);
                nodeHeader.clipping = TextClipping.Clip;

                nodeBody = new GUIStyle();
                nodeBody.normal.background = NodeEditorResources.nodeBody;
                nodeBody.border = new RectOffset(32, 32, 32, 32);
                nodeBody.padding = new RectOffset(16, 16, 4, 12);
                

                nodeHighlight = new GUIStyle();
                nodeHighlight.normal.background = NodeEditorResources.nodeHighlight;
                nodeHighlight.border = new RectOffset(16, 16, 16, 16);

                tooltip = new GUIStyle("helpBox");
                tooltip.alignment = TextAnchor.MiddleCenter;

                nodePaletteFoldout = new GUIStyle(EditorStyles.foldout);
                nodePaletteFoldout.fontStyle = FontStyle.Bold;

                minimizeButtonSimple = new GUIStyle(EditorStyles.miniButton);
                minimizeButtonSimple.margin = new RectOffset(0, 0, 5, 0);
                minimizeButtonSimple.padding = new RectOffset(2, 0, 0, 0);
                minimizeButtonSimple.alignment = TextAnchor.MiddleCenter;

                hiddenAttributeMark = new GUIStyle(EditorStyles.label);
                hiddenAttributeMark.padding = new RectOffset(0, 0, 0, 0);
                hiddenAttributeMark.margin = new RectOffset(0, 0, 0, 0);
                hiddenAttributeMark.alignment = TextAnchor.LowerCenter;

            }
        }

        public static Texture2D GenerateGridTexture(Color line, Color bg) {
            Texture2D tex = new Texture2D(64, 64);
            Color[] cols = new Color[64 * 64];
            for (int y = 0; y < 64; y++) {
                for (int x = 0; x < 64; x++) {
                    Color col = bg;
                    if (y % 16 == 0 || x % 16 == 0) col = Color.Lerp(line, bg, 0.65f);
                    if (y == 63 || x == 63) col = Color.Lerp(line, bg, 0.35f);
                    cols[(y * 64) + x] = col;
                }
            }
            tex.SetPixels(cols);
            tex.wrapMode = TextureWrapMode.Repeat;
            tex.filterMode = FilterMode.Bilinear;
            tex.name = "Grid";
            tex.Apply();
            return tex;
        }

        public static Texture2D GenerateCrossTexture(Color line) {
            Texture2D tex = new Texture2D(64, 64);
            Color[] cols = new Color[64 * 64];
            for (int y = 0; y < 64; y++) {
                for (int x = 0; x < 64; x++) {
                    Color col = line;
                    if (y != 31 && x != 31) col.a = 0;
                    cols[(y * 64) + x] = col;
                }
            }
            tex.SetPixels(cols);
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.name = "Grid";
            tex.Apply();
            return tex;
        }
    }
}