import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NomenclatureItemModel } from '../models/nomenclature-item.model';

@Injectable()
export class GeoService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getCities() {
    return this.http
      .get<NomenclatureItemModel[]>(this.baseUrl + `api/geo/cities`);
  }
}
