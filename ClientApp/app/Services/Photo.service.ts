import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';

@Injectable()
export class PhtotoService{
    constructor(private http:HttpClient) {}

    upload(vehicleId,photo){
        var file=new FormData();
        file.append('file',photo);
        return this.http.post(`/api/vehicles/${vehicleId}/photos`,file).pipe(map(res=>res));
    }

    getPhotos(vehicleId){
        return this.http.get(`/api/vehicles/${vehicleId}/photos`).pipe(map(res=>res));
    }
}