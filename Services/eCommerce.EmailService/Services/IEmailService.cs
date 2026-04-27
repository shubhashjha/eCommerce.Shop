using eCommerce.EmailService.Message;
using eCommerce.EmailService.Models.Dto;

namespace eCommerce.EmailService.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}

