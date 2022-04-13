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

        public Task<ICollection<Offer>> GetOfferByFilter(string? search, string? category, double? priceFrom, double? priceTo, string? sorting);
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
            return await GetAll().Where(x => x.Owner.Id == userId).ToListAsync();
        }

        public async Task<ICollection<Offer>> GetOfferByFilter(string? search,string? category,double? priceFrom,double? priceTo,string? sorting)
        {
            IQueryable<Offer> query = GetAll().Where(x=>x.OfferStatus == OfferStatus.Active);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Title.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(x => x.ItemCategory == Enum.Parse<ItemCategory>(category));

            if (priceFrom is not null)
                query = query.Where(x => x.Cost >= priceFrom);

            if (priceTo is not null)
                query = query.Where(x => x.Cost <= priceTo);


            return await query.ToListAsync();
        }
    }
}
