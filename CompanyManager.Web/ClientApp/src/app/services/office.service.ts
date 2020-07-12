import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OfficeListingViewModel } from '../models/office-listing-view.model';
import { OfficeCreateModel } from '../models/office-create.model';
import { OfficeDetailsModel } from '../models/office-details.model';
import { EmployeeListingServiceModel } from '../models/employee-listing-service.model';
import { OfficeListingServiceModel } from '../models/office-listing-service.model';

@Injectable()
export class OfficeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getOfficesList(search: string, page: number, pageSize: number): Observable<OfficeListingViewModel> {
    let params = new HttpParams()
      .set('page', String(page))
      .set('pageSize', String(pageSize));

    if (search) {
      params = params.set('search', String(search));
    }

    const options = {
      params
    };

    return this.http
      .get<OfficeListingViewModel>(this.baseUrl + 'api/offices', options);
  }

  getOffice(id: number) {
    return this.http
      .get<OfficeDetailsModel>(this.baseUrl + `api/offices/${id}`);
  }

  createOffice(office: OfficeCreateModel) {
    return this.http
      .post<any>(this.baseUrl + `api/offices`, office)
  }

  editOffice(id: number, office: OfficeCreateModel) {
    return this.http
      .put<any>(this.baseUrl + `api/offices/${id}`, office);
  }

  getEmployees(id: any) {
    return this.http
      .get<EmployeeListingServiceModel>(this.baseUrl + `api/offices/${id}/employees`);
  }

  getCompanyOfficesList(companyId: number) {
    return this.http
      .get<OfficeListingServiceModel[]>(this.baseUrl + `api/companies/${companyId}/offices`);
  }

  getEmployeeOfficesList(employeeId: number) {
    return this.http
      .get<OfficeListingServiceModel[]>(this.baseUrl + `api/employees/${employeeId}/offices`);
  }
}
