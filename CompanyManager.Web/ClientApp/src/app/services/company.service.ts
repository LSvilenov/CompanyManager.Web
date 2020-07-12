import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CompanyListingViewModel } from '../models/company-listing-view.model';
import { CompanyDetailsModel } from '../models/company-details.model';
import { CompanyCreateModel } from '../models/company-create.model';
import { OfficeListingServiceModel } from '../models/office-listing-service.model';
import { NomenclatureItemModel } from '../models/nomenclature-item.model';

@Injectable()
export class CompanyService {
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public baseUrl: string;

  getCompanyList(name: string, page: number, pageSize: number): Observable<CompanyListingViewModel> {
    let params = new HttpParams()
      .set('page', String(page))
      .set('pageSize', String(pageSize));

    if (name) {
      params = params.set('name', String(name));
    }

    const options = {
      params
    };

    return this.http
      .get<CompanyListingViewModel>(this.baseUrl + 'api/companies', options);
  }

  getCompany(id: number) {
    return this.http
      .get<CompanyDetailsModel>(this.baseUrl + `api/companies/${id}`);
  }

  createCompany(company: CompanyCreateModel) {
    return this.http
      .post<any>(this.baseUrl + `api/companies`, company);
  }

  editCompany(id: number, company: CompanyCreateModel) {
    return this.http
      .put<any>(this.baseUrl + `api/companies/${id}`, company);
  }

  getOffices(id: number) {
    return this.http
      .get<OfficeListingServiceModel>(this.baseUrl + `api/companies/${id}/offices`);
  }

  getComanySelectList() {
    const result = this.getCompanyList(null, 1, 100)
      .pipe(map(x => x.companies.map(c => <NomenclatureItemModel>{
        id: c.id,
        name: c.name
      })));

    return result;
  }
}
