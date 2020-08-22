import { PaginationComponent } from './Component/shared/pagination.component';
import { VehicleService } from './Services/vehicle.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Component/nav-menu/nav-menu.component';
import { HomeComponent } from './Component/home/home.component';
import { CounterComponent } from './Component/counter/counter.component';
import { FetchDataComponent } from './Component/fetch-data/fetch-data.component';
import { VehicleFormComponent } from './Component/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './Component/vehicle-list/vehicle-list-component';
import { ViewVehicleComponent } from './Component/view-vehicle/view-vehicle.component';
import { PhtotoService } from './Services/Photo.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles', component: VehicleListComponent },
      { path: 'vehicles/:id', component: ViewVehicleComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: '**', redirectTo: 'home' },
    ])
  ],
  providers: [VehicleService,PhtotoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
