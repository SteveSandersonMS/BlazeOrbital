namespace SatelliteSimulator.Rendering;

public struct Color
{
    public float R;
    public float G;
    public float B;
    public float A;

    public Color(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static readonly Color[] Presets = new[] { new Color(0.678f, 0.847f, 0.902f, 1), new Color(0.941f, 0.502f, 0.502f, 1), new Color(0.878f, 1.000f, 1.000f, 1), new Color(0.980f, 0.980f, 0.824f, 1), new Color(0.827f, 0.827f, 0.827f, 1), new Color(0.565f, 0.933f, 0.565f, 1), new Color(1.000f, 0.714f, 0.757f, 1), new Color(1.000f, 0.714f, 0.757f, 1), new Color(1.000f, 0.627f, 0.478f, 1), new Color(0.125f, 0.698f, 0.667f, 1), new Color(0.529f, 0.808f, 0.980f, 1), new Color(0.467f, 0.533f, 0.600f, 1), new Color(0.690f, 0.769f, 0.871f, 1), new Color(1.000f, 1.000f, 0.878f, 1), };
}
