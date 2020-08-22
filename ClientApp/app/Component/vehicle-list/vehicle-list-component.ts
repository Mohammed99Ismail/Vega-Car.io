import { KeyPair } from './../../Model/Vehicle';
import { VehicleService } from './../../Services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/Model/Vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list-component.html',
  styleUrls: ['./vehicle-list-component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles:Vehicle[];
  makes:KeyPair[];
  filter:any={
    PageSize:3
  };
  item:any;
  constructor(private vehicleService:VehicleService) { }

  ngOnInit() {
    this.vehicleService.getVehiclesTwo().subscribe((vehicle:Vehicle[])=>{
      this.item=vehicle.length;
    });
    this.vehicleService.getMakes().subscribe(makes=>this.makes=makes);
    this.populateVehicle();
  }

  populateVehicle(){
    this.vehicleService.getVehicles(this.filter).subscribe((vehicle:Vehicle[])=>{
      this.vehicles=vehicle;
    });
  }
  onFilterChange(){
    this.populateVehicle();
    console.log(this.item)    
  }

  resetFilter(){
    this.filter={};
    this.onFilterChange();
  }

  sortBy(value){
    if(this.filter.SortBy===value){
      this.filter.IsSortAsending=false
    }
    else{
      this.filter.SortBy=value;
      this.filter.IsSortAsending=true
    }
    this.populateVehicle();
  }

  onPageChange(page){
    this.filter.page=page;
    this.populateVehicle();
  }

}
