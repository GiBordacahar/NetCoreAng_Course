import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  getFeatures() {
    return this.http.get('/api/features')
    .pipe(map(res => res || []));
  }

  getMakes() {
    return this.http.get('/api/makes')
    .pipe(map(res => res || []));
  }

}