using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Repositories
{
    public interface IOfferRepository
    {
        public Task<ICollection<Offer>> GetAllOffers();
        public Task<Offer?> GetOfferById(int id);
        public Task<Offer?> AddOffer(Offer offer);
        public Task<Offer?> UpdateOffer(Offer offer);
        public Task<ICollection<Offer>> GetUserOffers(int userId);
        public IQueryable<Offer> GetAllActiveOffers();
        public Task<ICollection<Offer>> GetReportedOffers();
        public Task<ICollection<Offer>> GetPromotedOffers();
        public Task<ICollection<Offer>> GetUserOtherOffers(string email,int offerId);
    }

    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Offer?> AddOffer(Offer offer)
        {
            return await AddAsync(offer);
        }

        public async Task<ICollection<Offer>> GetAllOffers()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Offer?> GetOfferById(int id)
        {
            return await GetAll().Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Offer?> UpdateOffer(Offer offer)
        {
            return await UpdateAsync(offer);
        }

        public async Task<ICollection<Offer>> GetUserOffers(int userId)
        {
            return await GetAll().Where(x => x.Owner.Id == userId && x.OfferStatus != OfferStatus.Cancelled).ToListAsync();
        }

        public IQueryable<Offer> GetAllActiveOffers()
        {
            return  GetAll().Include(x => x.Owner).Where(x => x.OfferStatus == OfferStatus.Active && x.IsBanned == false);
        }

        public async Task<ICollection<Offer>> GetReportedOffers()
        {
            var reported = await repositoryContext.Reports.Select(x=>x.Offer.Id).ToListAsync();
            var offers =await GetAll().Include(x=>x.Owner).Where(x => reported.Contains(x.Id)).ToListAsync();
            return offers;
        }

        public async Task<ICollection<Offer>> GetPromotedOffers()
        {
            return await GetAll().Where(x => x.isPromoted).ToListAsync();
        }

        public async Task<ICollection<Offer>> GetUserOtherOffers(string email, int offerId)
        {
            return await GetAll().Include(x => x.Owner).Where(x => x.Owner.Email == email && x.Id != offerId)
                .ToListAsync();
        }
    }
}
