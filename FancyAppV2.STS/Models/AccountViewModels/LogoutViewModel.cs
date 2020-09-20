namespace FancyAppV2.STS.Models.AccountViewModels
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}