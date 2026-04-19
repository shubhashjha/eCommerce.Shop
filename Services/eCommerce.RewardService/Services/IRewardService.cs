using eCommerce.RewardService.Message;

namespace eCommerce.RewardService.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}

