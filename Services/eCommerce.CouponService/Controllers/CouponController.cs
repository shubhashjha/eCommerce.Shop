using AutoMapper;
using eCommerce.CouponService.Data;
using eCommerce.CouponService.Model;
using eCommerce.CouponService.Model.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public ResponseDTO Get()
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
        [Route("GetByCode/{Code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon coupons = appDBContext.Coupons.First(x => x.Code == code);
                response.Result = mapper.Map<CouponDTO>(coupons);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO coupon)
        {
            try
            {
                Coupon requestBody = mapper.Map<Coupon>(coupon);
                appDBContext.Coupons.Add(requestBody);
                appDBContext.SaveChanges();
                response.Result = mapper.Map<CouponDTO>(requestBody);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }


        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO coupon)
        {
            try
            {
                Coupon requestBody = mapper.Map<Coupon>(coupon);
                appDBContext.Coupons.Update(requestBody);
                appDBContext.SaveChanges();
                response.Result = mapper.Map<CouponDTO>(requestBody);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon coupons = appDBContext.Coupons.First(x => x.Id == id);
                appDBContext.Coupons.Remove(coupons);
                appDBContext.SaveChanges();
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
