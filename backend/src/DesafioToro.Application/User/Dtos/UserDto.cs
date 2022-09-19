namespace DesafioToro.Application.Dtos
{
    public class UserDto
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }
        public List<UserAssetDto> UserAssets { get; set; } = new List<UserAssetDto>();
    }
}
