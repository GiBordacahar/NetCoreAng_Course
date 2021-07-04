import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { SaveVehicle } from 'src/models/vehicle';

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

  create(vehicle) {
    return this.http.post('/api/vehicles', vehicle)
    .pipe(map(res => res || []));
  }

  getVehicle(id) {
    return this.http.get('/api/vehicles/' + id)
    .pipe(map(res => res || []));
  }

  update(vehicle: SaveVehicle) {
    return this.http.put('/api/vehicles/' + vehicle.id, vehicle)
    .pipe(map(res => res || []));
  }

  delete(id) {
    return this.http.delete('/api/vehicles/' + id)
    .pipe(map(res => res || []));
  }

}
