import { CompanyListingServiceModel } from './company-listing-service.model';

export interface CompanyListingViewModel {
    companies: CompanyListingServiceModel[];
    total: number;
    page: number;
}
