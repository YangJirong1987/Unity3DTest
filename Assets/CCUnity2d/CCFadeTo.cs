public class CCFadeTo : CCFadeIn
{
    public static CCFadeTo Create(float duartion, float opacity)
    {
        return new CCFadeTo {_tarOpacity = opacity, _duration = duartion};
    }
}