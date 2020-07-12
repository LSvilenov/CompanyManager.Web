import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class ImageService {
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public baseUrl: string;

  uploadImage(image: FormData) {
    return this.http
      .post(this.baseUrl + `api/employees`, image, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }), reportProgress: true, observe: 'events' });
  }
}
