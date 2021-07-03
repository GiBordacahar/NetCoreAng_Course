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
  public class FeaturesController : Controller
  {
    private readonly AppDbContext context;
    private readonly IMapper mapper;
    public FeaturesController(AppDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpGet("/api/features")]
    public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
    {
      var features = await context.Features.ToListAsync();
      return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features); 
    }
  }
}