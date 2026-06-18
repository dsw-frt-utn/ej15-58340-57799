namespace Dsw2026Ej15.Api.Models
{
    public record DoctorModel
    {
        public record Request(string Name, string LicenseNumber, Guid SpecialityId);

        public record Response(string Name, string LicenseNumber, string specialityName);
        //xq records anidados? xq el request es para recibir datos con cierta estructura y luego devolverlos con otra estructura
    }
}
