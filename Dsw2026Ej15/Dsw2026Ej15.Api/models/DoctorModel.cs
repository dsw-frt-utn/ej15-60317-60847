namespace Dsw2026Ej15.Api.models
{
    public class DoctorModel
    {
        public record Request(string Name, string LicenceNumber, Guid SpecialityId);
    }
}
