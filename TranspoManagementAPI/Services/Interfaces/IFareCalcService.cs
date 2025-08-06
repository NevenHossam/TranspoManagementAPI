namespace TranspoManagementAPI.Services.Interfaces
{
    public interface IFareCalcService
    {
        Task<double> CalculateFare(double distance, double multiplier = 1.0);
    }
}

