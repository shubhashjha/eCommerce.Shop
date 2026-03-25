using AutoMapper;
using Azure;
using eCommerce.CouponService.Data;
using eCommerce.CouponService.Model;
using eCommerce.CouponService.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace eCommerce.CouponService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private ResponseDTO response;
        private IMapper mapper;
        public CouponController(AppDBContext _appDBContext, IMapper _mapper)
        {
            appDBContext = _appDBContext;
            response = new ResponseDTO();
            mapper = _mapper;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = appDBContext.Coupons.ToList();
                response.Result = mapper.Map<IEnumerable<CouponDTO>>(coupons);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                
            }
            return response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon coupons = appDBContext.Coupons.First(x => x.Id == id);
                response.Result = mapper.Map<CouponDTO>(coupons);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
