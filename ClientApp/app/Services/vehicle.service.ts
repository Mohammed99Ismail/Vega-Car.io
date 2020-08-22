import { SaveVehicle } from './../Model/Vehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly vehicleEndPoint='/api/vehicles';

  constructor(private http: HttpClient) { }

  getMakes(){
    return this.http.get('/api/makes').pipe(map((x:any[])=>x));
  }

  getFeatures(){
    return this.http.get('/api/features').pipe(map(x=>x));
  }

  create(vehicle){
    return this.http.post('/api/vehicles',vehicle).pipe(map(v=>v));
  }
  
  getVehicle(id){
    return this.http.get('/api/vehicles/'+id);
  }

  updateVehicle(Vehicle:SaveVehicle){
    return this.http.put('/api/vehicles/'+Vehicle.id,Vehicle).pipe(map(res=>res));
  }

  deleteVehicle(id){
    return this.http.delete('/api/vehicles/'+id).pipe(map(res=>res));
  }

  getVehicles(filter) {
    return this.http.get(this.vehicleEndPoint + '?' + this.toQueryString(filter)).pipe(
      map(res => res));
  }
  
  getVehiclesTwo() {
    return this.http.get(this.vehicleEndPoint).pipe(
      map(res => res));
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }

}
