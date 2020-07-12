import { OfficeListingServiceModel } from './office-listing-service.model';
export interface OfficeListingViewModel {
  offices: OfficeListingServiceModel[];
  total: number;
  page: number;
}
