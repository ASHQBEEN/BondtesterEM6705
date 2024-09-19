namespace DutyCycle.Logic
{
    public class GlobalKeyMessageFilter(IGlobalKeyMessageFilter form) : IMessageFilter
    {
        private readonly IGlobalKeyMessageFilter form = form;

        public static bool BlockControls { get; set; } = false;

        public bool PreFilterMessage(ref Message m)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;

            if (m.Msg == WM_KEYDOWN)
            {
                return form.OnGlobalKeyDown((Keys)m.WParam.ToInt32());  // Call a method in Form to handle the key event
                // Optionally handle the key press and stop it from propagating further
            }

            if (m.Msg == WM_KEYUP)
            {
                return form.OnGlobalKeyUp((Keys)m.WParam.ToInt32());  // Call a method in Form to handle the key event
                // Optionally handle the key press and stop it from propagating further
            }
            return false;
        }
    }
}
