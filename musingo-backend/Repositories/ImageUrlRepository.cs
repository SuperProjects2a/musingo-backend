using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;

public interface IImageUrlRepository
{
    public Task<ImageUrl?> GetImageUrlById(int id);
    public Task<ImageUrl?> AddImageUrl(ImageUrl offer);
    public Task<ICollection<ImageUrl>> AddRangeImageUrl(ICollection<ImageUrl> imageUrls);
    public Task<ImageUrl?> UpdateImageUrl(ImageUrl offer);
    public IEnumerable<string> GetImageUrlsByOfferId(int offerId);
    public Task<ImageUrl?> GetFirstImageUrlByOfferId(int offerId);
    public ICollection<IGrouping<int, ImageUrl>> GetImageUrlsByOfferId();
}

public class ImageUrlRepository : Repository<ImageUrl>, IImageUrlRepository
{
    public ImageUrlRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<ImageUrl?> GetImageUrlById(int id)
    {
        return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ImageUrl?> AddImageUrl(ImageUrl imageUrl)
    {
        return await AddAsync(imageUrl);
    }

    public async Task<ICollection<ImageUrl>> AddRangeImageUrl(ICollection<ImageUrl> imageUrls)
    {
        return await AddRangeAsync(imageUrls);
    }

    public async Task<ImageUrl?> UpdateImageUrl(ImageUrl imageUrl)
    {
        return await UpdateAsync(imageUrl);
    }

    public IEnumerable<string> GetImageUrlsByOfferId(int offerId)
    { 
        return GetAll().Include(x => x.Offer).Where(x => x.Offer.Id == offerId).Select(x=>x.Url).AsEnumerable();
    }

    public async Task<ImageUrl?> GetFirstImageUrlByOfferId(int offerId)
    {
        return await GetAll().Include(x => x.Offer).FirstOrDefaultAsync(x => x.Offer.Id == offerId);
    }

    public ICollection<IGrouping<int, ImageUrl>> GetImageUrlsByOfferId()
    {
        var imageUrls = GetAll().Include(x=>x.Offer).Where(x => x.Offer.OfferStatus == OfferStatus.Active && x.Offer.IsBanned == false).ToList();

        return imageUrls.GroupBy(x => x.Offer.Id).ToList();
    }
}