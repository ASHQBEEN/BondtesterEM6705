namespace DutyCycle.Logic
{
    public interface IGlobalKeyMessageFilter
    {
        bool OnGlobalKeyDown(Keys key);
        bool OnGlobalKeyUp(Keys key);
    }
}
