using Application.Common.Interface;
using Application.Services.CategorySrv.Dto;
using Application.Services.CodeSrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Setting.CodeSrv.Iface
{
    public interface ICodeService : ICommonSrv<Code, CodeDto>
    {
        CodeSearchDto Search(CodeInputDto baseSearchDto);
        Task<CodeDto> GetByLabelAsync(string label);
        Task<CodeDto> GetByIdAsync(long id);
        Task<long> GetIdByLabelAsync(string label);
    }
}
