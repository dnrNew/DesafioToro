using DesafioToro.Application.Dtos;
using DesafioToro.Domain.UserAssets;

namespace DesafioToro.Application.Helpers
{
    public static class UserAssetHelper
    {
        public static UserAssetDto ToDto(UserAsset userAsset)
        {
            return new UserAssetDto
            {
                Quantity = userAsset.Quantity,
                Symbol = userAsset.Stock.Symbol,
            };
        }
    }
}
