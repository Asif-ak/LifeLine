using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LifeLine_WebApi.DBConfiguration;
using System.Linq;

namespace LifeLine_WebApi.Models
{
    public static class Seed
    {
        public static void Initialize(IServiceProvider provider)
        {
            using(var context=new LifeLineContext(
                provider.GetRequiredService<DbContextOptions<LifeLineContext>>()))
                {
                    if(context.Set<Donor>().Any()) return;

                    context.Set<Donor>().Add(
                        new Donor
                        {
                            DonorName="John Doe",
                            DonorBloodtype= BloodType.ABNeg,
                            DonorCellNumber="923001234567",
                            City="Karachi"
                        });
                        context.SaveChangesAsync();

                }
        }
    }
}