using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Order.BankSrv.Dto;
using Application.Services.Order.BankSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.BankSrv
{
    public class BankService : CommonSrv<Bank, BankDto>, IBankService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public BankService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public BaseSearchDto<BankVDto> Search(BaseInputDto baseSearchDto)
        {
            var query = _context.Banks.Include(s => s.Picture).AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(baseSearchDto.Q));
            }
            if (baseSearchDto.Available.HasValue)
            {
                query = query.Where(s => s.Active == baseSearchDto.Available);
            }
            return new BaseSearchDto<Bank, BankVDto>(baseSearchDto, query, mapper);
        }
    }
}
