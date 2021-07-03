using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAng.Controllers.Resources;
using NetCoreAng.Core.Models;
using NetCoreAng.Persistence;

namespace NetCoreAng.Controllers
{
  public class MakesController : Controller
  {
    private readonly AppDbContext context;
    private readonly IMapper mapper;
    public MakesController(AppDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpGet("/api/makes")]
    public async Task<IEnumerable<MakeResource>> GetMakes()
    {
        var makes = await context.Makes.Include(m => m.Models).ToListAsync();
        return mapper.Map<List<Make>, List<MakeResource>>(makes);
    }
  }
}