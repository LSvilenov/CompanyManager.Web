import { EmployeeListingServiceModel } from "./employee-listing-service.model";

export interface EmployeeListingViewModel {
  employees: EmployeeListingServiceModel;
  total: number;
  page: number;
}
