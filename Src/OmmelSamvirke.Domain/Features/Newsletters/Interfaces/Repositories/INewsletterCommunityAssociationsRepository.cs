﻿using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Repositories;

public interface INewsletterCommunityAssociationsRepository : IGenericRepository<NewsletterCommunityAssociations>
{
    Task<NewsletterCommunity> AddNewsletterCommunity(NewsletterCommunity newsletterCommunity, CancellationToken cancellationToken = default);
}
