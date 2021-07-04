import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class PhotoService {
    constructor(private http : HttpClient) { }

    upload(vehicleId, photo) {
        var formData = new FormData();
        formData.append('file', photo);
        return this.http.post(`/api/vehicles/${vehicleId}/photos`, formData)
            .pipe(map(res => res || []));;
    } 

    getPhotos(vehicleId) {
        return this.http.get(`/api/vehicles/${vehicleId}/photos`)
        .pipe(map(res => res || []));;
    } 
}