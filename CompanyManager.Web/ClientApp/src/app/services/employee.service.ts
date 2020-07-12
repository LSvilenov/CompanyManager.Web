import { Injectable, Inject } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeListingViewModel } from '../models/employee-listing-view.model';
import { EmployeeDetailsModel } from '../models/employee-details.model';
import { EmployeeCreateModel } from '../models/employee-create.model';

@Injectable()
export class EmployeeService {
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public baseUrl: string;

  getEmployeesList(search: string, page: number, pageSize: number): Observable<EmployeeListingViewModel> {
    let params = new HttpParams()
      .set('page', String(page))
      .set('pageSize', String(pageSize));

    if (search) {
      params = params.set('search', String(search))
    }

    const options = {
      params
    };

    return this.http
      .get<EmployeeListingViewModel>(this.baseUrl + 'api/employees', options);
  }

  getEmployee(id: number) {
    return this.http
      .get<EmployeeDetailsModel>(this.baseUrl + `api/employees/${id}`);
  }

  createEmployee(employee: EmployeeCreateModel) {
    return this.http
      .post<any>(this.baseUrl + `api/employees`, employee);
  }

  editEmployee(id: number, employee: EmployeeCreateModel) {
    return this.http
      .put<any>(this.baseUrl + `api/employees/${id}`, employee);
  }
}
