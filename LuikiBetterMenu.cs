using UnityEngine;
using UnityEngine.XR;

public class LuikiBetterMenu : MonoBehaviour
{
    // =========================
    // MENU STATE
    // =========================

    private bool menuOpen = false;
    private bool settingsOpen = false;

    private MenuPage[] pages;
    private int currentPage = 0;

    private Rect windowRect = new Rect(100, 100, 620, 680);

    // =========================
    // MOVEMENT
    // =========================

    private bool platforms = false;
    private bool ghostMonkey = false;
    private bool invisibleMonkey = false;

    // =========================
    // SAFETY
    // =========================

    private bool antiKick = false;
    private bool antiBan = false;

    // =========================
    // CONTROLLER STATES
    // =========================

    private bool previousYPressed = false;
    private bool previousAPressed = false;
    private bool previousBPressed = false;

    // =========================
    // UI
    // =========================

    private GUIStyle titleStyle;
    private GUIStyle versionStyle;
    private GUIStyle pageStyle;
    private GUIStyle buttonStyle;
    private GUIStyle tabStyle;
    private GUIStyle statusStyle;
    private GUIStyle settingsStyle;

    private Texture2D cyanTexture;
    private Texture2D darkTexture;
    private Texture2D panelTexture;

    private float animationTime;

    private void Start()
    {
        CreatePages();
        CreateTextures();
        CreateStyles();
    }

    // =========================
    // PAGES
    // =========================

    private void CreatePages()
    {
        pages = new MenuPage[]
        {
            new MenuPage(
                "Movement",
                "Platforms",
                "Ghost Monkey",
                "Invisible Monkey",
                "Long Arms"
            ),

            new MenuPage(
                "Overpowered",
                "Kick Gun",
                "Kick All",
                "Crash Gun",
                "Crash All",
                "Reverse Card"
            ),

            new MenuPage(
                "Safety",
                "Anti-Kick",
                "Anti-Ban",
                "Accept ToS"
            )
        };
    }

    // =========================
    // TEXTURES
    // =========================

    private void CreateTextures()
    {
        cyanTexture = MakeTexture(new Color(0f, 0.85f, 1f));
        darkTexture = MakeTexture(new Color(0.025f, 0.035f, 0.055f));
        panelTexture = MakeTexture(new Color(0.055f, 0.075f, 0.105f));
    }

    private Texture2D MakeTexture(Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }

    // =========================
    // STYLES
    // =========================

    private void CreateStyles()
    {
        titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 34,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        versionStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleCenter
        };

        pageStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 24,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        tabStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 15,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        statusStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 13,
            alignment = TextAnchor.MiddleCenter
        };

        settingsStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold
        };
    }

    // =========================
    // UPDATE
    // =========================

    private void Update()
    {
        animationTime += Time.deltaTime;

        InputDevice leftController =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        InputDevice rightController =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Y = menu
        if (leftController.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out bool yPressed))
        {
            if (yPressed && !previousYPressed)
                menuOpen = !menuOpen;

            previousYPressed = yPressed;
        }

        // A = Ghost Monkey
        if (rightController.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out bool aPressed))
        {
            if (aPressed && !previousAPressed)
                ghostMonkey = !ghostMonkey;

            previousAPressed = aPressed;
        }

        // B = Invisible Monkey
        if (rightController.TryGetFeatureValue(
            CommonUsages.secondaryButton,
            out bool bPressed))
        {
            if (bPressed && !previousBPressed)
                invisibleMonkey = !invisibleMonkey;

            previousBPressed = bPressed;
        }

        if (platforms)
            ApplyPlatforms();

        if (ghostMonkey)
            ApplyGhostMonkey();

        if (invisibleMonkey)
            ApplyInvisibleMonkey();
    }

    // =========================
    // MOVEMENT HOOKS
    // =========================

    private void ApplyPlatforms()
    {
        // Add platform implementation here.
    }

    private void ApplyGhostMonkey()
    {
        // Add Ghost Monkey implementation here.
    }

    private void ApplyInvisibleMonkey()
    {
        // Add Invisible Monkey implementation here.
    }

    private void ActivateLongArms()
    {
        Debug.Log("Luiki Better: Long Arms activated.");
    }

    // =========================
    // GUI
    // =========================

    private void OnGUI()
    {
        if (!menuOpen)
            return;

        GUI.backgroundColor = Color.white;

        windowRect = GUI.Window(
            999,
            windowRect,
            DrawMenu,
            ""
        );
    }

    private void DrawMenu(int windowID)
    {
        // Dark background
        GUI.DrawTexture(
            new Rect(0, 0, windowRect.width, windowRect.height),
            darkTexture
        );

        GUILayout.BeginVertical();

        GUILayout.Space(12);

        // =========================
        // LOGO / HEADER
        // =========================

        float pulse =
            0.75f + Mathf.Sin(animationTime * 3f) * 0.25f;

        Color oldColor = GUI.color;

        GUI.color = new Color(
            0.3f,
            pulse,
            1f
        );

        GUILayout.Label(
            "LUIKI BETTER",
            titleStyle,
            GUILayout.Height(45)
        );

        GUI.color = Color.white;

        GUILayout.Label(
            "V1.0",
            versionStyle,
            GUILayout.Height(22)
        );

        GUILayout.Space(8);

        // Cyan divider
        GUI.DrawTexture(
            new Rect(
                25,
                82,
                windowRect.width - 50,
                3
            ),
            cyanTexture
        );

        GUILayout.Space(18);

        // =========================
        // PAGE TABS
        // =========================

        GUILayout.BeginHorizontal();

        for (int i = 0; i < pages.Length; i++)
        {
            bool selected = i == currentPage;

            GUI.backgroundColor = selected
                ? Color.cyan
                : new Color(0.12f, 0.15f, 0.2f);

            if (GUILayout.Button(
                pages[i].Name,
                tabStyle,
                GUILayout.Height(38)))
            {
                currentPage = i;
                settingsOpen = false;
            }
        }

        GUI.backgroundColor = Color.white;

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        if (settingsOpen)
        {
            DrawSettings();
        }
        else
        {
            DrawCurrentPage();
        }

        GUILayout.FlexibleSpace();

        // =========================
        // FOOTER
        // =========================

        GUILayout.BeginHorizontal();

        GUI.backgroundColor = new Color(
            0.1f,
            0.15f,
            0.2f
        );

        if (GUILayout.Button(
            "⚙ SETTINGS",
            settingsStyle,
            GUILayout.Height(38)))
        {
            settingsOpen = !settingsOpen;
        }

        if (GUILayout.Button(
            "EXIT",
            settingsStyle,
            GUILayout.Height(38)))
        {
            menuOpen = false;
            settingsOpen = false;
        }

        GUI.backgroundColor = Color.white;

        GUILayout.EndHorizontal();

        GUILayout.Space(6);

        GUILayout.Label(
            "PAGE " +
            (currentPage + 1) +
            " / " +
            pages.Length,
            statusStyle
        );

        GUILayout.Space(8);

        GUILayout.EndVertical();

        GUI.DragWindow();
    }

    // =========================
    // PAGE
    // =========================

    private void DrawCurrentPage()
    {
        MenuPage page = pages[currentPage];

        GUI.DrawTexture(
            new Rect(
                20,
                145,
                windowRect.width - 40,
                430
            ),
            panelTexture
        );

        GUILayout.BeginVertical();

        GUILayout.Space(15);

        GUILayout.Label(
            page.Name.ToUpper(),
            pageStyle,
            GUILayout.Height(35)
        );

        GUILayout.Space(12);

        foreach (string mod in page.Mods)
        {
            DrawModButton(mod);
            GUILayout.Space(7);
        }

        GUILayout.EndVertical();
    }

    // =========================
    // MOD BUTTON
    // =========================

    private void DrawModButton(string mod)
    {
        string text = mod;

        if (mod == "Platforms")
            text = platforms
                ? "●  Platforms       ON"
                : "○  Platforms       OFF";

        if (mod == "Ghost Monkey")
            text = ghostMonkey
                ? "●  Ghost Monkey    ON"
                : "○  Ghost Monkey    OFF";

        if (mod == "Invisible Monkey")
            text = invisibleMonkey
                ? "●  Invisible Monkey ON"
                : "○  Invisible Monkey OFF";

        if (mod == "Anti-Kick")
            text = antiKick
                ? "●  Anti-Kick       ON"
                : "○  Anti-Kick       OFF";

        if (mod == "Anti-Ban")
            text = antiBan
                ? "●  Anti-Ban        ON"
                : "○  Anti-Ban        OFF";

        GUI.backgroundColor =
            new Color(0.08f, 0.12f, 0.17f);

        if (GUILayout.Button(
            text,
            buttonStyle,
            GUILayout.Height(48)))
        {
            HandleMod(mod);
        }

        GUI.backgroundColor = Color.white;
    }

    // =========================
    // SETTINGS
    // =========================

    private void DrawSettings()
    {
        GUILayout.Space(20);

        GUILayout.Label(
            "SETTINGS",
            pageStyle,
            GUILayout.Height(40)
        );

        GUILayout.Space(20);

        GUI.backgroundColor =
            new Color(0.08f, 0.12f, 0.17f);

        GUILayout.Label(
            "Luiki Better",
            buttonStyle,
            GUILayout.Height(45)
        );

        GUILayout.Label(
            "Version: V1.0",
            versionStyle
        );

        GUILayout.Space(10);

        if (GUILayout.Button(
            "Back to Menu",
            buttonStyle,
            GUILayout.Height(45)))
        {
            settingsOpen = false;
        }

        GUI.backgroundColor = Color.white;
    }

    // =========================
    // HANDLER
    // =========================

    private void HandleMod(string mod)
    {
        if (mod == "Platforms")
        {
            platforms = !platforms;
            return;
        }

        if (mod == "Ghost Monkey")
        {
            ghostMonkey = !ghostMonkey;
            return;
        }

        if (mod == "Invisible Monkey")
        {
            invisibleMonkey = !invisibleMonkey;
            return;
        }

        if (mod == "Long Arms")
        {
            ActivateLongArms();
            return;
        }

        if (mod == "Anti-Kick")
        {
            antiKick = !antiKick;
            return;
        }

        if (mod == "Anti-Ban")
        {
            antiBan = !antiBan;
            return;
        }

        if (mod == "Accept ToS")
        {
            Debug.Log(
                "Luiki Better: Accept ToS selected."
            );
            return;
        }

        // Safe UI placeholders for the disruptive entries.
        if (mod == "Kick Gun" ||
            mod == "Kick All" ||
            mod == "Crash Gun" ||
            mod == "Crash All" ||
            mod == "Reverse Card")
        {
            Debug.Log(
                "Luiki Better: " +
                mod +
                " selected."
            );
        }
    }
}
