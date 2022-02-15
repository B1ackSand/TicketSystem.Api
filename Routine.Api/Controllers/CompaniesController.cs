using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DtoParameters;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    //只做 web api只需要继承base这个类，如果带有mvc架构则继承Controller
    //注解非强制，但建议添加会开启以下功能：要求使用属性路由；自动 HTTP 400响应；推断参数的绑定源；错误代码问题的详细信息等
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        //数据库操作注入到Controller
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ??
                                 throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        //获取所有Company资源
        //ActionResult 类型表示多种 HTTP 状态代码，是控制器的返回类型
        [HttpGet]
        [HttpHead] // 只请求页面的头部
        public async Task<ActionResult<IEnumerable<CompanyDto>>>
            GetCompanies([FromQuery] CompanyDtoParameters? parameters)
        {
            //返回一个集合
            var companies = await _companyRepository.GetCompaniesAsync(parameters);

            //面向外部的models输出
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);


            //集合转化为json格式
            //return new JsonResult(companies);
            //return Ok(companies);
            return Ok(companyDtos);
        }

        [HttpGet("{companyId}",Name = nameof(GetCompany))]
        //[Route("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            //返回一个集合
            var company = await _companyRepository.GetCompaniesAsync(companyId);
            //集合转化为json格式
            //return new JsonResult(companies);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CompanyDto>(company));
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CompanyAddDto company)
        {
            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync();

            var returnDto = _mapper.Map<CompanyDto>(entity);

            return CreatedAtRoute(nameof(GetCompany), new {companyId = returnDto.Id}, returnDto);
        }
    }
}
