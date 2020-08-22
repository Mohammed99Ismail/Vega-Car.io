import { SaveVehicle, Vehicle, Contact, KeyPair } from './../../Model/Vehicle';
import { Component, OnInit, DoCheck } from '@angular/core';
import { VehicleService } from './../../Services/vehicle.service';
import { Router, ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { features } from 'process';
import { identifierModuleUrl } from '@angular/compiler';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit,DoCheck {

  make:any[];
  models:any[];
  features;
  vehicle:SaveVehicle={
    id:0,
    makeId:0,
    modelId:0,
    isRegistered:"false",
    features:[],
    contact:{
      name:'',
      phone:'',
      email:''
    }
  };

  constructor(private vehicleService: VehicleService
    ,private router: Router,private route:ActivatedRoute) 
    {
      route.params.subscribe(p=>{
        this.vehicle.id=+p['id'];
      }) 
    }

  ngOnInit() {
    var sources=[
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];
    if(this.vehicle.id)
    sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    forkJoin(sources).subscribe(data=>{
      this.make=data[0] as any[];
      this.features=data[1];

      if(this.vehicle.id){
      this.setVehicle(data[2] as Vehicle)
      this.populateForm();
      }
    },err=>{
      this.router.navigate(['/vehicles/new']);
    });

    this.vehicleService.getFeatures().subscribe(f=>{
      this.features=f;
    });
    this.vehicleService.getMakes().subscribe(m=>{
      this.make=m
    });
    
  }
  private setVehicle(v:Vehicle){
    this.vehicle.id=v.id;
      this.vehicle.makeId=v.make.id;
      this.vehicle.modelId=v.model.id;
      this.vehicle.isRegistered=v.isRegistered;
      this.vehicle.contact=v.contact;
      this.vehicle.features=this.extractId(v.features);
  }
  private extractId(feature:KeyPair[]){
    let ids:number[]=[];
    feature.forEach(function(value){
      ids.push(value.id);
    })
    return ids;
  }

  onMakeChange(){
    this.populateForm();
    delete this.vehicle.modelId;
  }

  private populateForm(){
    var selectedMake=this.make.find(x=>x.id==this.vehicle.makeId);
    this.models=selectedMake ? selectedMake.models : [];
  }

  onSubmit(){
    if(this.vehicle.id){
    this.vehicleService.updateVehicle(this.vehicle).subscribe(val=>{
      this.router.navigate(['/vehicles']);},
      err=>console.log(err));
    }else{
    this.vehicle.id=0;
      this.vehicleService.create(this.vehicle).subscribe(v=>{
        this.router.navigate(['/vehicles']);
      });
      
    }
  }

  onChangeFeature(data,event){
    if(event.target.checked){
    this.vehicle.features.push(data);
    }else{
      var index=this.vehicle.features.indexOf(data);
      this.vehicle.features.splice(index,1);
    }

  }

  ngDoCheck() {
    if(this.vehicle.modelId)
    this.vehicle.modelId=+this.vehicle.modelId;
    if(this.vehicle.isRegistered)
    this.vehicle.isRegistered=JSON.parse(this.vehicle.isRegistered);
  }
  
  delete(){
    if(confirm("Are You Sure?")){
    return this.vehicleService.deleteVehicle(this.vehicle.id).subscribe(res=>{
      this.router.navigate(['/vehicles']);
    });
  }
  }
}

