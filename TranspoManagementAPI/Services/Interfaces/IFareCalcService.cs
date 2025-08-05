namespace TranspoManagementAPI.IServices
{
public interface IFareCalcService
    {
    Task<double> CalculateFare(double distance, double multiplier = 1.0);
    }
}

