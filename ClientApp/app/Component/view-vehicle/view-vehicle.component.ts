import { PhtotoService } from './../../Services/Photo.service';
import { VehicleService } from './../../Services/vehicle.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Vehicle } from 'src/app/Model/Vehicle';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  vehicle:any={
    features:[],
    make:{
      name:''
    },
    model:{
      name:''
    },
    isRegistered:false,
    contact:{}
  };
  @ViewChild('fileInput', {static: false}) fileInput: ElementRef;
  vehicleId:number;
  photos:any[]=[{fileName:''}];
  constructor(private vehicleService:VehicleService
    ,private route:ActivatedRoute,private router:Router,private photoService:PhtotoService) { }

  ngOnInit() {
    
    this.route.params.subscribe(p=>this.vehicleId=+p['id']);
    this.vehicleService.getVehicle(this.vehicleId).subscribe((v:Vehicle)=>{
      this.vehicle=v;
    });
    
    this.photoService.getPhotos(this.vehicleId).subscribe((p:any[])=>{
      this.photos=p;
    });
  }
  delete(){
    confirm("Are You Sure to delete the vehicle !")
    this.vehicleService.deleteVehicle(this.vehicleId).subscribe(v=>v);
    this.router.navigate(['/vehicles']);
  }

  uploadPhoto(){
    var element:HTMLInputElement=this.fileInput.nativeElement;
    this.photoService.upload(this.vehicleId,element.files[0]).subscribe(x=>console.log(x));
  }

}
