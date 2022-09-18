﻿using DesafioToro.Application.Dtos;
using DesafioToro.Domain.User;

namespace DesafioToro.Application.Helpers
{
    public static class UserHelper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Cpf = user.Cpf,
                Account = user.Account,
                Balance = user.Balance
            };
        }
    }
}
